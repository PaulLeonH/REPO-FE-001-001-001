using CDFacturacion.ComprobanteDeRetencion;
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
    public class XMLComprobanteDeRetencion
    {
        private static readonly ISerializador _serializador = new Serializador();
        private static readonly ICertificador _certificador = new Certificador();
        private static readonly IServicioSunatDocumentos _servicioDocumentoSunat = new ServicioSunatDocumentos();

        public static async Task<EnviarResumenResponse> RutaXMLAsync(ComprobanteDeRetencion retent)
        {
            try
            {
                Retention retention = FillXML(retent);
                var TramaXMLSinFirma = await _serializador.GenerarXml(retention);

                var firmadoRequest = new FirmadoRequest
                {
                    TramaXmlSinFirma = TramaXMLSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(@"C:\Users\Diego-pc\Desktop\FacturacionSUNAT\CNFacturacion\TemporaryFacturacion.pfx")),
                    PasswordCertificado = "123456789",
                    UnSoloNodoExtension = true
                };

                var respuestaFirmado = await _certificador.FirmarXml(firmadoRequest);

                var response = new EnviarResumenResponse();
                var nombreArchivo = $"{retent.Emisor.DocumentoNumero}-20-{retent.Detalles.ID}";
                var tramaZip = await _serializador.GenerarZip(respuestaFirmado.TramaXmlFirmado, nombreArchivo);

                _servicioDocumentoSunat.Inicializar(new ParametrosConexion
                {
                    Ruc = retent.Emisor.DocumentoNumero,
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

        public static Retention FillXML(ComprobanteDeRetencion compReten)
        {
            Emisor emisor = compReten.Emisor;
            DomicilioFiscal emisorDomicilioFiscal = emisor.DomicilioFiscal;
            Proveedor proveedor = compReten.Proveedor;
            DomicilioFiscal proveedorDomicilioFiscal = proveedor.DomicilioFiscal;
            Detalles detalles = compReten.Detalles;
            ComprobanteRelacionado comprobanteRelacionado = detalles.ComprobanteRelacionado;

            Retention retention = new Retention();

            retention.UBLVersionID = "2.0";
            retention.CustomizationID = "1.0";
            retention.ID = detalles.ID;
            retention.IssueDate = detalles.FechaEmision;

            #region Firma
            SignatureCac signature = new SignatureCac();
            signature.ID = emisor.FirmaDigital;
            signature.SignatoryParty.PartyIdentification.ID.Value = emisor.DocumentoNumero;
            signature.SignatoryParty.PartyName.Name = emisor.Nom_RazonSoc;
            signature.DigitalSignatureAttachment.ExternalReference.URI = "#SignatureSP";
            retention.Signature = signature;
            #endregion


            // Emisor
            retention.AgentParty.PartyIdentification.ID.SchemeId = emisor.DocumentoTipo;
            retention.AgentParty.PartyIdentification.ID.Value = emisor.DocumentoNumero;
            retention.AgentParty.PartyLegalEntity.RegistrationName = emisor.Nom_RazonSoc;
            retention.AgentParty.PartyName.Name = emisor.NombreComercial;

            retention.AgentParty.PostalAddress.ID = emisorDomicilioFiscal.Ubigeo;
            retention.AgentParty.PostalAddress.StreetName = emisorDomicilioFiscal.Direccion;
            retention.AgentParty.PostalAddress.CitySubdivisionName = emisorDomicilioFiscal.Urbanizacion;
            retention.AgentParty.PostalAddress.CityName = emisorDomicilioFiscal.Departamento;
            retention.AgentParty.PostalAddress.CountrySubentity = emisorDomicilioFiscal.Provincia;
            retention.AgentParty.PostalAddress.District = emisorDomicilioFiscal.Distrito;
            retention.AgentParty.PostalAddress.Country.IdentificationCode = emisorDomicilioFiscal.PaisCodigo;


            // Proveedor
            retention.ReceiverParty.PartyIdentification.ID.SchemeId = proveedor.DocumentoTipo;
            retention.ReceiverParty.PartyIdentification.ID.Value = proveedor.DocumentoNumero;
            retention.ReceiverParty.PartyLegalEntity.RegistrationName = proveedor.Nom_RazonSoc;
            retention.ReceiverParty.PartyName.Name = proveedor.NombreComercial;

            retention.ReceiverParty.PostalAddress.ID = proveedorDomicilioFiscal.Ubigeo;
            retention.ReceiverParty.PostalAddress.StreetName = proveedorDomicilioFiscal.Direccion;
            retention.ReceiverParty.PostalAddress.CitySubdivisionName = proveedorDomicilioFiscal.Urbanizacion;
            retention.ReceiverParty.PostalAddress.CityName = proveedorDomicilioFiscal.Departamento;
            retention.ReceiverParty.PostalAddress.CountrySubentity = proveedorDomicilioFiscal.Provincia;
            retention.ReceiverParty.PostalAddress.District = proveedorDomicilioFiscal.Distrito;
            retention.ReceiverParty.PostalAddress.Country.IdentificationCode = proveedorDomicilioFiscal.PaisCodigo;


            // Regimen de Retencion
            retention.SunatRetentionSystemCode = detalles.RetencionRegimen;
            // Tasa de Retencion
            retention.SunatRetentionPercent = detalles.RetencionTasa;
            // Observaciones
            retention.Note = detalles.Notas_Observaciones;
            // Importe Total Retenido
            retention.TotalInvoiceAmount.CurrencyID = detalles.ImporteTotalRetenidoMoneda;
            retention.TotalInvoiceAmount.Value = detalles.ImporteTotalRetenido;
            // Importe Total Pagado
            retention.TotalPaid.CurrencyID = detalles.ImporteTotalPagadoMoneda;
            retention.TotalPaid.Value = detalles.ImporteTotalPagado;


            // Tipo de Documento Relacionado
            retention.SunatRetentionDocumentReference.Add(new SunatRetentionDocumentReference {
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
                Payment = new Payment {
                    PaidDate = comprobanteRelacionado.PagoFecha,
                    IdPayment = comprobanteRelacionado.PagoNumero,
                    PaidAmount = new PayableAmount
                    {
                        CurrencyID = comprobanteRelacionado.PagoMoneda,
                        Value = comprobanteRelacionado.PagoImporteSinRetencion
                    }
                },
                SunatRetentionInformation = new SunatRetentionInformation
                {
                    SunatRetentionAmount = new PayableAmount
                    {
                        CurrencyID = comprobanteRelacionado.RetencionMoneda,
                        Value = comprobanteRelacionado.RetencionImporte
                    },
                    SunatRetentionDate = comprobanteRelacionado.RetencionFecha,
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




            return retention;
        }
    }
}
