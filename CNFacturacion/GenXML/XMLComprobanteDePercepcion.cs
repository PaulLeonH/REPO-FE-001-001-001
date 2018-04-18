using CDFacturacion.ComprobanteDePercepcion;
using CNFacturacion.CommonDTO.Exchange;
using CNFacturacion.Services;
using CNFacturacion.Signed;
using CNFacturacion.Structure.CommonAggregateComponents;
using CNFacturacion.Structure.CommonBasicComponents;
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
    public class XMLComprobanteDePercepcion
    {
        private static readonly ISerializador _serializador = new Serializador();
        private static readonly ICertificador _certificador = new Certificador();
        private static readonly IServicioSunatDocumentos _servicioDocumentoSunat = new ServicioSunatDocumentos();

        public static async Task<EnviarResumenResponse> RutaXMLAsync(ComprobanteDePercepcion percep)
        {
            try
            {
                Perception perception = FillXML(percep);
                var TramaXMLSinFirma = await _serializador.GenerarXml(perception);

                var firmadoRequest = new FirmadoRequest
                {
                    TramaXmlSinFirma = TramaXMLSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(@"C:\Users\Diego-pc\Desktop\FacturacionSUNAT\CNFacturacion\TemporaryFacturacion.pfx")),
                    PasswordCertificado = "123456789",
                    UnSoloNodoExtension = true
                };

                var respuestaFirmado = await _certificador.FirmarXml(firmadoRequest);

                var response = new EnviarResumenResponse();
                var nombreArchivo = $"{percep.Emisor.DocumentoNumero}-40-{percep.Detalles.ID}";
                var tramaZip = await _serializador.GenerarZip(respuestaFirmado.TramaXmlFirmado, nombreArchivo);

                _servicioDocumentoSunat.Inicializar(new ParametrosConexion
                {
                    Ruc = percep.Emisor.DocumentoNumero,
                    UserName = "MODDATOS",
                    Password = "moddatos",
                    EndPointUrl = @"https://e-beta.sunat.gob.pe:443/ol-ti-itcpfegem-beta/billService",
                });

                var resultado = _servicioDocumentoSunat.EnviarResumen(new DocumentoSunat
                {
                    NombreArchivo = $"{nombreArchivo}.zip",
                    TramaXml = tramaZip
                });

                if (resultado.Exito)
                {
                    response.NroTicket = resultado.NumeroTicket;
                    response.Exito = true;
                    response.NombreArchivo = nombreArchivo;
                }
                else
                {
                    response.MensajeError = resultado.MensajeError;
                    response.Exito = false;
                }

                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Perception FillXML(ComprobanteDePercepcion compPercep)
        {
            Emisor emisor = compPercep.Emisor;
            DomicilioFiscal emisorDomicilioFiscal = emisor.DomicilioFiscal;
            Cliente proveedor = compPercep.Cliente;
            DomicilioFiscal proveedorDomicilioFiscal = proveedor.DomicilioFiscal;
            Detalles detalles = compPercep.Detalles;
            ComprobanteRelacionado comprobanteRelacionado = detalles.ComprobanteRelacionado;

            Perception perception = new Perception();

            perception.UBLVersionID = "2.0";
            perception.CustomizationID = "1.0";
            perception.ID = detalles.ID;
            perception.IssueDate = detalles.FechaEmision;

            #region Firma
            SignatureCac signature = new SignatureCac();
            signature.ID = emisor.FirmaDigital;
            signature.SignatoryParty.PartyIdentification.ID.Value = emisor.DocumentoNumero;
            signature.SignatoryParty.PartyName.Name = emisor.Nom_RazonSoc;
            signature.DigitalSignatureAttachment.ExternalReference.URI = "#SignatureSP";
            perception.Signature = signature;
            #endregion


            // Emisor
            perception.AgentParty.PartyIdentification.ID.SchemeId = emisor.DocumentoTipo;
            perception.AgentParty.PartyIdentification.ID.Value = emisor.DocumentoNumero;
            perception.AgentParty.PartyLegalEntity.RegistrationName = emisor.Nom_RazonSoc;
            perception.AgentParty.PartyName.Name = emisor.NombreComercial;

            perception.AgentParty.PostalAddress.ID = emisorDomicilioFiscal.Ubigeo;
            perception.AgentParty.PostalAddress.StreetName = emisorDomicilioFiscal.Direccion;
            perception.AgentParty.PostalAddress.CitySubdivisionName = emisorDomicilioFiscal.Urbanizacion;
            perception.AgentParty.PostalAddress.CityName = emisorDomicilioFiscal.Departamento;
            perception.AgentParty.PostalAddress.CountrySubentity = emisorDomicilioFiscal.Provincia;
            perception.AgentParty.PostalAddress.District = emisorDomicilioFiscal.Distrito;
            perception.AgentParty.PostalAddress.Country.IdentificationCode = emisorDomicilioFiscal.PaisCodigo;


            // Cliente
            perception.ReceiverParty.PartyIdentification.ID.SchemeId = proveedor.DocumentoTipo;
            perception.ReceiverParty.PartyIdentification.ID.Value = proveedor.DocumentoNumero;
            perception.ReceiverParty.PartyLegalEntity.RegistrationName = proveedor.Nom_RazonSoc;
            perception.ReceiverParty.PartyName.Name = proveedor.NombreComercial;

            perception.ReceiverParty.PostalAddress.ID = proveedorDomicilioFiscal.Ubigeo;
            perception.ReceiverParty.PostalAddress.StreetName = proveedorDomicilioFiscal.Direccion;
            perception.ReceiverParty.PostalAddress.CitySubdivisionName = proveedorDomicilioFiscal.Urbanizacion;
            perception.ReceiverParty.PostalAddress.CityName = proveedorDomicilioFiscal.Departamento;
            perception.ReceiverParty.PostalAddress.CountrySubentity = proveedorDomicilioFiscal.Provincia;
            perception.ReceiverParty.PostalAddress.District = proveedorDomicilioFiscal.Distrito;
            perception.ReceiverParty.PostalAddress.Country.IdentificationCode = proveedorDomicilioFiscal.PaisCodigo;


            // Regimen de Retencion
            perception.SunatPerceptionSystemCode = detalles.PercepcionRegimen;
            // Tasa de Retencion
            perception.SunatPerceptionPercent = detalles.PercepcionTasa;
            // Observaciones
            perception.Note = detalles.Notas_Observaciones;
            // Importe Total Retenido
            perception.TotalInvoiceAmount.CurrencyID = detalles.ImporteTotalPercibidoMoneda;
            perception.TotalInvoiceAmount.Value = detalles.ImporteTotalPercibido;
            // Importe Total Pagado
            perception.TotalPaid.CurrencyID = detalles.ImporteTotalPercibidoMoneda;
            perception.TotalPaid.Value = detalles.ImporteTotalPercibido;


            // Tipo de Documento Relacionado
            perception.SunatPerceptionDocumentReference.Add(new SunatRetentionDocumentReference
            {
                Id = new PartyIdentificationId
                {
                    SchemeId = comprobanteRelacionado.DocumentoTipo,
                    Value = comprobanteRelacionado.DocumentoNumero
                },
                IssueDate = comprobanteRelacionado.FechaEmision,
                TotalInvoiceAmount = new PayableAmount
                {
                    CurrencyID = comprobanteRelacionado.Moneda,
                    Value = comprobanteRelacionado.ImporteTotal
                },
                Payment = new Payment
                {
                    PaidDate = comprobanteRelacionado.CobroFecha,
                    IdPayment = comprobanteRelacionado.CobroNumero,
                    PaidAmount = new PayableAmount
                    {
                        CurrencyID = comprobanteRelacionado.CobroMoneda,
                        Value = comprobanteRelacionado.CobroImporteSinRetencion
                    }
                },
                SunatRetentionInformation = new SunatRetentionInformation
                {
                    SunatRetentionAmount = new PayableAmount
                    {
                        CurrencyID = comprobanteRelacionado.PercepcionMoneda,
                        Value = comprobanteRelacionado.PercepcionImporte
                    },
                    SunatRetentionDate = comprobanteRelacionado.PercepcionFecha,
                    SunatNetTotalPaid = new PayableAmount
                    {
                        CurrencyID = comprobanteRelacionado.TotalNetoMoneda,
                        Value = comprobanteRelacionado.TotalNetoMonto
                    },
                    ExchangeRate = new ExchangeRate
                    {
                        SourceCurrencyCode = comprobanteRelacionado.TipoCambioMonedaReferencia,
                        TargetCurrencyCode = comprobanteRelacionado.TipoCambioMonedaObjetivo,
                        CalculationRate = comprobanteRelacionado.TipoCambioFactorAplicado,
                        Date = comprobanteRelacionado.TipoCambioFecha
                    }
                }
            });
            
            return perception;
        }
    }
}
