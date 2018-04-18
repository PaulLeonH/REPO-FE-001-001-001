using CDFacturacion.NotaDeCredito;
using CNFacturacion.CommonDTO.Exchange;
using CNFacturacion.Services;
using CNFacturacion.Signed;
using CNFacturacion.Structure.CommonAggregateComponents;
using CNFacturacion.Structure.CommonExtensionComponents;
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
    public class XMLNotaDeCredito
    {

        private static readonly ISerializador _serializador = new Serializador();
        private static readonly ICertificador _certificador = new Certificador();
        private static readonly IServicioSunatDocumentos _servicioDocumentoSunat = new ServicioSunatDocumentos();

        public static async Task<EnviarDocumentoResponse> RutaXMLAsync(NotaDeCredito notaCred)
        {
            try
            {
                CreditNote creditNote = FillXML(notaCred);
                var TramaXMLSinFirma = await _serializador.GenerarXml(creditNote);

                var firmadoRequest = new FirmadoRequest
                {
                    TramaXmlSinFirma = TramaXMLSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(@"C:\Users\Diego-pc\Desktop\FacturacionSUNAT\CNFacturacion\TemporaryFacturacion.pfx")),
                    PasswordCertificado = "123456789",
                    UnSoloNodoExtension = false
                };

                var respuestaFirmado = await _certificador.FirmarXml(firmadoRequest);

                var enviarDocumentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = notaCred.Emisor.DocumentoNumero,
                    UsuarioSol = $"{notaCred.Emisor.DocumentoNumero}MODDATOS",
                    ClaveSol = "moddatos",
                    EndPointUrl = @"https://e-beta.sunat.gob.pe:443/ol-ti-itcpfegem-beta/billService",
                    IdDocumento = notaCred.Detalles.ID ,
                    TipoDocumento = "07",
                    TramaXmlFirmado = respuestaFirmado.TramaXmlFirmado
                };

                var response = new EnviarDocumentoResponse();
                var nombreArchivo = $"{enviarDocumentoRequest.Ruc}-{enviarDocumentoRequest.TipoDocumento}-{enviarDocumentoRequest.IdDocumento}";
                var tramaZip = await _serializador.GenerarZip(enviarDocumentoRequest.TramaXmlFirmado, nombreArchivo);

                _servicioDocumentoSunat.Inicializar(new ParametrosConexion
                {
                    Ruc = notaCred.Emisor.DocumentoNumero,
                    UserName = $"{notaCred.Emisor.DocumentoNumero}MODDATOS",
                    Password = "moddatos",
                    EndPointUrl = @"https://e-beta.sunat.gob.pe:443/ol-ti-itcpfegem-beta/billService",
                });

                var resultado = _servicioDocumentoSunat.EnviarDocumento(new DocumentoSunat
                {
                    TramaXml = tramaZip,
                    NombreArchivo = $"{nombreArchivo}.zip"
                });

                if (!resultado.Exito)
                {
                    response.Exito = false;
                    response.MensajeError = resultado.MensajeError;
                }
                else
                {
                    response = await _serializador.GenerarDocumentoRespuesta(resultado.ConstanciaDeRecepcion);
                    response.NombreArchivo = nombreArchivo;
                }

                return response;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static CreditNote FillXML(NotaDeCredito notaCred)
        {
            #region Instanciacion
            Emisor emisor = notaCred.Emisor;
            DomicilioFiscal domicilioFiscal = emisor.DomicilioFiscal;
            Cliente cliente = notaCred.Cliente;
            Detalles detalles = notaCred.Detalles;
            List<ItemNotaCredito> listaItems = detalles.ListaItems;
            MontosGlobales montosGlobales = detalles.MontosGlobales;
            GuiaDeRemision guiaDeRemision = detalles.GuiaDeRemision;
            DocumentoAsociado documentoAsociado = detalles.DocumentoAsociado;
            #endregion

            CreditNote creditNote = new CreditNote();

            creditNote.UBLVersionID = "2.0";
            creditNote.CustomizationID = "1.0";
            creditNote.ID = detalles.ID;
            creditNote.DocumentCurrencyCode = detalles.Moneda;
            creditNote.IssueDate = detalles.FechaEmision;

            DiscrepancyResponse DRDetalleNota = new DiscrepancyResponse();
            DRDetalleNota.ReferenceId = detalles.NotaElectronicaDocumentoModificadoNumero;
            DRDetalleNota.ResponseCode = detalles.NotaElectronicaCodigoTipo;
            DRDetalleNota.Description = detalles.NotaElectronicaDescripcion;
            creditNote.DiscrepancyResponses.Add(DRDetalleNota);

            BillingReference BRDetalleNota = new BillingReference();
            BRDetalleNota.InvoiceDocumentReference.ID = detalles.NotaElectronicaDocumentoModificadoNumero;
            BRDetalleNota.InvoiceDocumentReference.DocumentTypeCode = detalles.NotaElectronicaDocumentoModificadoTipo;
            creditNote.BillingReferences.Add(BRDetalleNota);

            #region Firma
            SignatureCac signature = new SignatureCac();
            signature.ID = emisor.FirmaDigital;
            signature.SignatoryParty.PartyIdentification.ID.Value = emisor.DocumentoNumero;
            signature.SignatoryParty.PartyName.Name = emisor.Nom_RazonSoc;
            signature.DigitalSignatureAttachment.ExternalReference.URI = "#SignatureSP";
            creditNote.Signature = signature;
            #endregion
            
            #region Emisor
            creditNote.AccountingSupplierParty.CustomerAssignedAccountId = emisor.DocumentoNumero;
            creditNote.AccountingSupplierParty.AdditionalAccountId = emisor.DocumentoTipo;

            Party partyEmisor = new Party();
            partyEmisor.PartyName.Name = emisor.NombreComercial;
            partyEmisor.PostalAddress.ID = domicilioFiscal.Ubigeo;
            partyEmisor.PostalAddress.StreetName = domicilioFiscal.Direccion;
            partyEmisor.PostalAddress.CitySubdivisionName = domicilioFiscal.Urbanizacion;
            partyEmisor.PostalAddress.CityName = domicilioFiscal.Departamento;
            partyEmisor.PostalAddress.CountrySubentity = domicilioFiscal.Provincia;
            partyEmisor.PostalAddress.District = domicilioFiscal.Distrito;
            partyEmisor.PostalAddress.Country.IdentificationCode = domicilioFiscal.PaisCodigo;
            partyEmisor.PartyLegalEntity.RegistrationName = emisor.Nom_RazonSoc;
            creditNote.AccountingSupplierParty.Party = partyEmisor;
            #endregion

            #region Cliente
            creditNote.AccountingCustomerParty.CustomerAssignedAccountId = cliente.DocumentoNumero;
            creditNote.AccountingCustomerParty.AdditionalAccountId = cliente.DocumentoTipo;

            Party partyCliente = new Party();
            partyCliente.PartyLegalEntity.RegistrationName = cliente.Nom_RazonSoc;
            creditNote.AccountingCustomerParty.Party = partyCliente;
            #endregion

            #region Items
            foreach (ItemNotaCredito item in listaItems)
            {
                VoidedDocumentLine invoiceLine = new VoidedDocumentLine();
                invoiceLine.ID = item.ID;
                invoiceLine.Item.SellersItemIdentification.ID = item.CodigoProducto;

                // Cantidad/Descripcion
                invoiceLine.CreditedQuantity.UnitCode = item.UnidadTipo;
                invoiceLine.CreditedQuantity.Value = item.UnidadCantidad;
                invoiceLine.Item.Description = item.DescripcionDetallada;

                // ValorUnitario
                invoiceLine.Price.PriceAmount.CurrencyID = detalles.Moneda;
                invoiceLine.Price.PriceAmount.Value = item.ValorUnitario;

                // PrecioVenta
                AlternativeConditionPrice alternativePrecioVenta = new AlternativeConditionPrice();
                alternativePrecioVenta.PriceAmount.CurrencyID = detalles.Moneda;
                alternativePrecioVenta.PriceAmount.Value = item.PrecioVenta;
                alternativePrecioVenta.PriceTypeCode = "01";
                invoiceLine.PricingReference.AlternativeConditionPrices.Add(alternativePrecioVenta);

                // AfectacionIGV
                TaxTotal TaxIGV = new TaxTotal();
                TaxIGV.TaxAmount.CurrencyID = detalles.Moneda;
                TaxIGV.TaxAmount.Value = item.ItemMontoIGV;
                TaxIGV.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
                TaxIGV.TaxSubtotal.TaxAmount.Value = item.ItemMontoIGV;
                TaxIGV.TaxSubtotal.TaxCategory.TaxExemptionReasonCode = item.AfectacionIGV;
                TaxIGV.TaxSubtotal.TaxCategory.TaxScheme.ID = "1000";
                TaxIGV.TaxSubtotal.TaxCategory.TaxScheme.Name = "IGV";
                TaxIGV.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "VAT";
                invoiceLine.TaxTotals.Add(TaxIGV);

                // SistemaISC
                TaxTotal TaxISC = new TaxTotal();
                TaxISC.TaxAmount.CurrencyID = detalles.Moneda;
                TaxISC.TaxAmount.Value = item.ItemMontoISC;
                TaxISC.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
                TaxISC.TaxSubtotal.TaxAmount.Value = item.ItemMontoISC;
                TaxISC.TaxSubtotal.TaxCategory.TierRange = item.SistemaISC;
                TaxISC.TaxSubtotal.TaxCategory.TaxScheme.ID = "2000";
                TaxISC.TaxSubtotal.TaxCategory.TaxScheme.Name = "ISC";
                TaxISC.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "EXC";
                invoiceLine.TaxTotals.Add(TaxISC);

                // Valor Referencial Unitario por Item en Operaciones No Onerosas
                AlternativeConditionPrice ACPONOValorReferencial = new AlternativeConditionPrice();
                ACPONOValorReferencial.PriceAmount.CurrencyID = detalles.Moneda;
                ACPONOValorReferencial.PriceAmount.Value = item.OperacionesNoOnerosasValUnitario;
                ACPONOValorReferencial.PriceTypeCode = "02";
                invoiceLine.PricingReference.AlternativeConditionPrices.Add(ACPONOValorReferencial);

                // Valor Venta de Item
                invoiceLine.LineExtensionAmount.CurrencyID = detalles.Moneda;
                invoiceLine.LineExtensionAmount.Value = item.ItemValorVenta;

                creditNote.CreditNoteLines.Add(invoiceLine);
            }
            #endregion

            UBLExtension ublExtension = new UBLExtension();

            #region Montos Globales

            #region Total Valores de Venta

            #region OperacionesGravadas
            AdditionalMonetaryTotal AMTOperacionesGravadas = new AdditionalMonetaryTotal();
            AMTOperacionesGravadas.ID = "1001";
            AMTOperacionesGravadas.PayableAmount.CurrencyID = detalles.Moneda;
            AMTOperacionesGravadas.PayableAmount.Value = montosGlobales.TVVOperacionesGravadas;
            ublExtension.ExtensionContent.AdditionalInformation.AdditionalMonetaryTotals.Add(AMTOperacionesGravadas);
            #endregion
            #region OperacionesInafectas
            AdditionalMonetaryTotal AMTOperacionesInafectas = new AdditionalMonetaryTotal();
            AMTOperacionesInafectas.ID = "1002";
            AMTOperacionesInafectas.PayableAmount.CurrencyID = detalles.Moneda;
            AMTOperacionesInafectas.PayableAmount.Value = montosGlobales.TVVOperacionesInafectas;
            ublExtension.ExtensionContent.AdditionalInformation.AdditionalMonetaryTotals.Add(AMTOperacionesInafectas);
            #endregion
            #region OperacionesExoneradas
            AdditionalMonetaryTotal AMTOperacionesExoneradas = new AdditionalMonetaryTotal();
            AMTOperacionesExoneradas.ID = "1003";
            AMTOperacionesExoneradas.PayableAmount.CurrencyID = detalles.Moneda;
            AMTOperacionesExoneradas.PayableAmount.Value = montosGlobales.TVVOperacionesExoneradas;
            ublExtension.ExtensionContent.AdditionalInformation.AdditionalMonetaryTotals.Add(AMTOperacionesExoneradas);
            #endregion

            #endregion
            #region Sumatorias

            #region SumatoriaIGV
            TaxTotal TaxSumaIGV = new TaxTotal();
            TaxSumaIGV.TaxAmount.CurrencyID = detalles.Moneda;
            TaxSumaIGV.TaxAmount.Value = montosGlobales.SumatoriaIGV;
            TaxSumaIGV.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
            TaxSumaIGV.TaxSubtotal.TaxAmount.Value = montosGlobales.SumatoriaIGV;
            TaxSumaIGV.TaxSubtotal.TaxCategory.TaxScheme.ID = "1000";
            TaxSumaIGV.TaxSubtotal.TaxCategory.TaxScheme.Name = "IGV";
            TaxSumaIGV.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "VAT";
            creditNote.TaxTotals.Add(TaxSumaIGV);
            #endregion
            #region SumatoriaISC
            TaxTotal TaxSumaISC = new TaxTotal();
            TaxSumaISC.TaxAmount.CurrencyID = detalles.Moneda;
            TaxSumaISC.TaxAmount.Value = montosGlobales.SumatoriaISC;
            TaxSumaISC.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
            TaxSumaISC.TaxSubtotal.TaxAmount.Value = montosGlobales.SumatoriaISC;
            TaxSumaISC.TaxSubtotal.TaxCategory.TaxScheme.ID = "2000";
            TaxSumaISC.TaxSubtotal.TaxCategory.TaxScheme.Name = "ISC";
            TaxSumaISC.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "EXC";
            creditNote.TaxTotals.Add(TaxSumaISC);
            #endregion
            #region SumatoriaOtrosTributos
            TaxTotal TaxSumaOtrosTributos = new TaxTotal();
            TaxSumaOtrosTributos.TaxAmount.CurrencyID = detalles.Moneda;
            TaxSumaOtrosTributos.TaxAmount.Value = montosGlobales.SumatoriaOtrosTributos;
            TaxSumaOtrosTributos.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
            TaxSumaOtrosTributos.TaxSubtotal.TaxAmount.Value = montosGlobales.SumatoriaOtrosTributos;
            TaxSumaOtrosTributos.TaxSubtotal.TaxCategory.TaxScheme.ID = "9999";
            TaxSumaOtrosTributos.TaxSubtotal.TaxCategory.TaxScheme.Name = "OTROS";
            TaxSumaOtrosTributos.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "OTH";
            creditNote.TaxTotals.Add(TaxSumaOtrosTributos);
            #endregion
            #region SumatoriaOtrosCargos
            creditNote.LegalMonetaryTotal.ChargeTotalAmount.CurrencyID = detalles.Moneda;
            creditNote.LegalMonetaryTotal.ChargeTotalAmount.Value = montosGlobales.SumatorioOtrosCargos;
            #endregion

            #endregion

            // Total Descuentos
            AdditionalMonetaryTotal AMTTotalDescuentos = new AdditionalMonetaryTotal();
            AMTTotalDescuentos.ID = "2005";
            AMTTotalDescuentos.PayableAmount.CurrencyID = detalles.Moneda;
            AMTTotalDescuentos.PayableAmount.Value = montosGlobales.TotalDescuentos;
            ublExtension.ExtensionContent.AdditionalInformation.AdditionalMonetaryTotals.Add(AMTTotalDescuentos);

            // ImporteTotalVenta
            creditNote.LegalMonetaryTotal.PayableAmount.CurrencyID = detalles.Moneda;
            creditNote.LegalMonetaryTotal.PayableAmount.Value = montosGlobales.ImporteTotalVenta;
            
            #endregion

            // Guia De Remision
            InvoiceDocumentReference IDRGuiaDeRemision = new InvoiceDocumentReference();
            IDRGuiaDeRemision.ID = guiaDeRemision.GuiaNumeroDeGuia;
            IDRGuiaDeRemision.DocumentTypeCode = guiaDeRemision.GuiaTipoDocumento;
            creditNote.DespatchDocumentReferences.Add(IDRGuiaDeRemision);

            // Documento Relacionado
            InvoiceDocumentReference IDRDocumentoRelacionado = new InvoiceDocumentReference();
            IDRDocumentoRelacionado.ID = documentoAsociado.DocumentoNumero;
            IDRDocumentoRelacionado.DocumentTypeCode = documentoAsociado.DocumentoTipo;
            creditNote.AdditionalDocumentReferences.Add(IDRDocumentoRelacionado);

            
            creditNote.UBLExtensions.Extension2 = ublExtension;
            
            return creditNote;

        }
    }
}
