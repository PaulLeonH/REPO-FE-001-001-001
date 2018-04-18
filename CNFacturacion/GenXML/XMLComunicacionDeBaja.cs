using CDFacturacion.ComunicacionDeBaja;
using CNFacturacion.CommonDTO.Exchange;
using CNFacturacion.Services;
using CNFacturacion.Signed;
using CNFacturacion.Structure.CommonAggregateComponents;
using CNFacturacion.Structure.EstandarUBL;
using CNFacturacion.Structure.SunatAggregateComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.GenXML
{
    public class XMLComunicacionDeBaja
    {
        private static readonly ISerializador _serializador = new Serializador();
        private static readonly ICertificador _certificador = new Certificador();
        private static readonly IServicioSunatDocumentos _servicioDocumentoSunat = new ServicioSunatDocumentos();

        public static async Task<EnviarDocumentoResponse> RutaXMLAsync(ComunicacionDeBaja docBaja)
        {
            try
            {
                VoidedDocuments voidedDocument = FillXML(docBaja);
                var TramaXMLSinFirma = await _serializador.GenerarXml(voidedDocument);

                var firmadoRequest = new FirmadoRequest
                {
                    TramaXmlSinFirma = TramaXMLSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(@"C:\Users\Diego-pc\Desktop\FacturacionSUNAT\CNFacturacion\TemporaryFacturacion.pfx")),
                    PasswordCertificado = "123456789",
                    UnSoloNodoExtension = true
                };

                var respuestaFirmado = await _certificador.FirmarXml(firmadoRequest);

                var response = new EnviarDocumentoResponse();
                var nombreArchivo = $"{docBaja.Emisor.DocumentoNumero}-{docBaja.Detalles.ID}";
                var tramaZip = await _serializador.GenerarZip(respuestaFirmado.TramaXmlFirmado, nombreArchivo);

                _servicioDocumentoSunat.Inicializar(new ParametrosConexion
                {
                    Ruc = docBaja.Emisor.DocumentoNumero,
                    UserName = "MODDATOS",
                    Password = "moddatos",
                    EndPointUrl = @"https://e-beta.sunat.gob.pe:443/ol-ti-itcpfegem-beta/billService",
                });

                var resultado = _servicioDocumentoSunat.EnviarResumen(new DocumentoSunat {
                    NombreArchivo = $"{nombreArchivo}.zip",
                    TramaXml = tramaZip
                });

                var consultaTicketRequest = new ConsultaTicketRequest
                {
                    Ruc = docBaja.Emisor.DocumentoNumero,
                    UsuarioSol = $"{docBaja.Emisor.DocumentoNumero}MODDATOS",
                    ClaveSol = "moddatos",
                    EndPointUrl = @"https://e-beta.sunat.gob.pe:443/ol-ti-itcpfegem-beta/billService",
                    IdDocumento = docBaja.Detalles.ID,
                    NroTicket = resultado.NumeroTicket
                };

                response = await ConsultaTicket.ConsultaTicketAsync(consultaTicketRequest);


                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static VoidedDocuments FillXML(ComunicacionDeBaja docBaja)
        {
            #region Instanciacion
            Emisor emisor = docBaja.Emisor;
            Detalles detalles = docBaja.Detalles;
            List<DocumentoDeBaja> listaItems = detalles.ListaDocumentos;
            #endregion

            VoidedDocuments voidedDocuments = new VoidedDocuments();

            voidedDocuments.UBLVersionID = "2.0";
            voidedDocuments.CustomizationID = "1.0";
            voidedDocuments.ID = detalles.ID;
            voidedDocuments.ReferenceDate = detalles.DocumentoDeBajaFechaGeneracion;
            voidedDocuments.IssueDate = detalles.FechaGeneracion;

            #region Firma
            SignatureCac signature = new SignatureCac();
            signature.ID = emisor.FirmaDigital;
            signature.SignatoryParty.PartyIdentification.ID.Value = emisor.DocumentoNumero;
            signature.SignatoryParty.PartyName.Name = emisor.Nom_RazonSoc;
            signature.DigitalSignatureAttachment.ExternalReference.URI = "#SignatureSP";
            voidedDocuments.Signature = signature;
            #endregion

            #region Emisor
            voidedDocuments.AccountingSupplierParty.CustomerAssignedAccountId = emisor.DocumentoNumero;
            voidedDocuments.AccountingSupplierParty.AdditionalAccountId = emisor.DocumentoTipo;

            Party partyEmisor = new Party();
            partyEmisor.PartyLegalEntity.RegistrationName = emisor.Nom_RazonSoc;
            voidedDocuments.AccountingSupplierParty.Party = partyEmisor;
            #endregion
            

            #region Items
            foreach (DocumentoDeBaja item in listaItems)
            {
                VoidedDocumentsLine voidedLine = new VoidedDocumentsLine();
                voidedLine.LineId = item.ID;
                voidedLine.DocumentTypeCode = item.TipoDocumento;
                voidedLine.DocumentSerialId = item.SerieDocumento;
                voidedLine.DocumentNumberId = item.NumeroDocumento;
                voidedLine.VoidReasonDescription = item.Descripcion;

                voidedDocuments.VoidedDocumentsLines.Add(voidedLine);
            }
            #endregion

            return voidedDocuments;
        }
    }
}
