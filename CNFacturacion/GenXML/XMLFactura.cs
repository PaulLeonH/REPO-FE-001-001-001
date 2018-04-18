using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDFacturacion;
using CDFacturacion.Factura;
using CNFacturacion.Structure;
using CNFacturacion.Structure.EstandarUBL;
using CNFacturacion.Structure.CommonAggregateComponents;
using CNFacturacion.Structure.SunatAggregateComponents;
using CNFacturacion.Signed;
using CNFacturacion.Services;
using CNFacturacion.CommonDTO.Exchange;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using CNFacturacion.Structure.CommonExtensionComponents;
using System.Reflection;
using System.Web;
using System.Globalization;

namespace CNFacturacion.GenXML
{
    public class XMLFactura
    {
        private static readonly ISerializador _serializador = new Serializador();
        private static readonly ICertificador _certificador = new Certificador();
        private static readonly IServicioSunatDocumentos _servicioDocumentoSunat = new ServicioSunatDocumentos();

        public static async Task<EnviarDocumentoResponse> RutaXMLAsync(Factura fac)
        {
            try
            {
                Invoice invoice = FillXML(fac);
                var TramaXMLSinFirma = await _serializador.GenerarXml(invoice);

                string rootPath;
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("HOME")))
                    rootPath = Environment.GetEnvironmentVariable("HOME") + "\\site\\wwwroot\\Content\\";
                else if (HttpContext.Current != null)
                    rootPath = HttpContext.Current.Server.MapPath("~");
                else
                    rootPath = ".";
                string path = rootPath + "TemporaryFacturacion.pfx";
                
                var firmadoRequest = new FirmadoRequest
                {
                    TramaXmlSinFirma = TramaXMLSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(path)),
                    PasswordCertificado = "123456789",
                    UnSoloNodoExtension = false
                };

                var respuestaFirmado = await _certificador.FirmarXml(firmadoRequest);

                var enviarDocumentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = fac.Emisor.DocumentoNumero,
                    UsuarioSol = $"{fac.Emisor.DocumentoNumero}MODDATOS",
                    ClaveSol = "moddatos",
                    EndPointUrl = @"https://e-beta.sunat.gob.pe:443/ol-ti-itcpfegem-beta/billService",
                    IdDocumento = fac.Detalles.DocumentoNumero,
                    TipoDocumento = fac.Detalles.DocumentoTipo,
                    TramaXmlFirmado = respuestaFirmado.TramaXmlFirmado
                };

                var response = new EnviarDocumentoResponse();
                var nombreArchivo = $"{enviarDocumentoRequest.Ruc}-{enviarDocumentoRequest.TipoDocumento}-{enviarDocumentoRequest.IdDocumento}";
                var tramaZip = await _serializador.GenerarZip(enviarDocumentoRequest.TramaXmlFirmado, nombreArchivo);
                
                _servicioDocumentoSunat.Inicializar(new ParametrosConexion
                {
                    Ruc = fac.Emisor.DocumentoNumero,
                    UserName = $"{fac.Emisor.DocumentoNumero}MODDATOS",
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

        public static Invoice FillXML(Factura fac)
        {
            #region Instanciacion
            Emisor emisor = fac.Emisor;
            DomicilioFiscal domicilioFiscal = emisor.DomicilioFiscal;
            Cliente cliente = fac.Cliente;
            Detalles detalles = fac.Detalles;
            List<ItemFactura> listaItems = detalles.ListaItems;
            MontosGlobales montosGlobales = detalles.MontosGlobales;
            GuiaDeRemision guiaDeRemision = detalles.GuiaDeRemision;
            DocumentoAsociado documentoAsociado = detalles.DocumentoAsociado;
            List<Leyenda> listaLeyendas = detalles.Leyendas;
            Detraccion detraccion = detalles.Detraccion;
            List<DetalleDetraccion> detallesDetraccion = detraccion.Detracciones;
            #endregion
            
            Invoice invoice = new Invoice();

            invoice.UBLVersionID = "2.0";
            invoice.CustomizationID = "1.0";
            invoice.ID = detalles.DocumentoNumero;
            invoice.InvoiceTypeCode = Catalogos.Catalogo01.TipoDeDocumento.FirstOrDefault(x => x.Value == "Factura").Key;
            invoice.DocumentCurrencyCode = detalles.Moneda;
            invoice.IssueDate = DateTime.ParseExact(detalles.FechaEmision, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            #region Firma
            SignatureCac signature = new SignatureCac();
            signature.ID = emisor.FirmaDigital;
            signature.SignatoryParty.PartyIdentification.ID.Value = emisor.DocumentoNumero;
            signature.SignatoryParty.PartyName.Name = emisor.NomRazonSoc;
            signature.DigitalSignatureAttachment.ExternalReference.URI = "#SignatureSP";
            invoice.Signature = signature;
            #endregion

            #region Emisor
            invoice.AccountingSupplierParty.CustomerAssignedAccountId = emisor.DocumentoNumero;
            invoice.AccountingSupplierParty.AdditionalAccountId = emisor.DocumentoTipo;

            Party partyEmisor = new Party();
            partyEmisor.PartyName.Name = emisor.NombreComercial;
            partyEmisor.PostalAddress.ID = domicilioFiscal.Ubigeo;
            partyEmisor.PostalAddress.StreetName = domicilioFiscal.Direccion;
            partyEmisor.PostalAddress.CitySubdivisionName = domicilioFiscal.Urbanizacion;
            partyEmisor.PostalAddress.CityName = domicilioFiscal.Departamento;
            partyEmisor.PostalAddress.CountrySubentity = domicilioFiscal.Provincia;
            partyEmisor.PostalAddress.District = domicilioFiscal.Distrito;
            partyEmisor.PostalAddress.Country.IdentificationCode = domicilioFiscal.PaisCodigo;
            partyEmisor.PartyLegalEntity.RegistrationName = emisor.NomRazonSoc;
            invoice.AccountingSupplierParty.Party = partyEmisor;
            #endregion

            #region Cliente
            invoice.AccountingCustomerParty.CustomerAssignedAccountId = cliente.DocumentoNumero;
            invoice.AccountingCustomerParty.AdditionalAccountId = cliente.DocumentoTipo;

            Party partyCliente = new Party();
            partyCliente.PartyLegalEntity.RegistrationName = cliente.NomRazonSoc;
            invoice.AccountingCustomerParty.Party = partyCliente;
            #endregion

            #region Items
            foreach (ItemFactura item in listaItems)
            {
                VoidedDocumentLine invoiceLine = new VoidedDocumentLine();
                invoiceLine.ID = (Int16)item.ID;
                invoiceLine.Item.SellersItemIdentification.ID = item.CodigoProducto;

                // Cantidad/Descripcion
                invoiceLine.InvoicedQuantity.UnitCode = item.UnidadTipo;
                invoiceLine.InvoicedQuantity.Value = item.UnidadCantidad;
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
                ACPONOValorReferencial.PriceAmount.Value = (decimal)item.OperacionesNoOnerosasValUnitario;
                ACPONOValorReferencial.PriceTypeCode = "02";
                invoiceLine.PricingReference.AlternativeConditionPrices.Add(ACPONOValorReferencial);

                // Descuento por Item
                invoiceLine.AllowanceCharge.ChargeIndicator = false;
                invoiceLine.AllowanceCharge.Amount.CurrencyID = detalles.Moneda;
                invoiceLine.AllowanceCharge.Amount.Value = (decimal)item.ItemDescuentos;

                // Valor Venta de Item
                invoiceLine.LineExtensionAmount.CurrencyID = detalles.Moneda;
                invoiceLine.LineExtensionAmount.Value = item.ItemValorVenta;

                invoice.InvoiceLines.Add(invoiceLine);
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
                TaxSumaIGV.TaxAmount.Value = (decimal)montosGlobales.SumatoriaIGV;
                TaxSumaIGV.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
                TaxSumaIGV.TaxSubtotal.TaxAmount.Value = (decimal)montosGlobales.SumatoriaIGV;
                TaxSumaIGV.TaxSubtotal.TaxCategory.TaxScheme.ID = "1000";
                TaxSumaIGV.TaxSubtotal.TaxCategory.TaxScheme.Name = "IGV";
                TaxSumaIGV.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "VAT";
                invoice.TaxTotals.Add(TaxSumaIGV);
                #endregion
                #region SumatoriaISC
                TaxTotal TaxSumaISC = new TaxTotal();
                TaxSumaISC.TaxAmount.CurrencyID = detalles.Moneda;
                TaxSumaISC.TaxAmount.Value = (decimal)montosGlobales.SumatoriaISC;
                TaxSumaISC.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
                TaxSumaISC.TaxSubtotal.TaxAmount.Value = (decimal)montosGlobales.SumatoriaISC;
                TaxSumaISC.TaxSubtotal.TaxCategory.TaxScheme.ID = "2000";
                TaxSumaISC.TaxSubtotal.TaxCategory.TaxScheme.Name = "ISC";
                TaxSumaISC.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "EXC";
                invoice.TaxTotals.Add(TaxSumaISC);
                #endregion
                #region SumatoriaOtrosTributos
                TaxTotal TaxSumaOtrosTributos = new TaxTotal();
                TaxSumaOtrosTributos.TaxAmount.CurrencyID = detalles.Moneda;
                TaxSumaOtrosTributos.TaxAmount.Value = (decimal)montosGlobales.SumatoriaOtrosTributos;
                TaxSumaOtrosTributos.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
                TaxSumaOtrosTributos.TaxSubtotal.TaxAmount.Value = (decimal)montosGlobales.SumatoriaOtrosTributos;
                TaxSumaOtrosTributos.TaxSubtotal.TaxCategory.TaxScheme.ID = "9999";
                TaxSumaOtrosTributos.TaxSubtotal.TaxCategory.TaxScheme.Name = "OTROS";
                TaxSumaOtrosTributos.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "OTH";
                invoice.TaxTotals.Add(TaxSumaOtrosTributos);
            #endregion
                #region SumatoriaOtrosCargos
                invoice.LegalMonetaryTotal.ChargeTotalAmount.CurrencyID = detalles.Moneda;
                invoice.LegalMonetaryTotal.ChargeTotalAmount.Value = (decimal)montosGlobales.SumatoriaOtrosCargos;
            #endregion

            #endregion

            // Total Descuentos
            AdditionalMonetaryTotal AMTTotalDescuentos = new AdditionalMonetaryTotal();
            AMTTotalDescuentos.ID = "2005";
            AMTTotalDescuentos.PayableAmount.CurrencyID = detalles.Moneda;
            AMTTotalDescuentos.PayableAmount.Value = (decimal)montosGlobales.TotalDescuentos;
            ublExtension.ExtensionContent.AdditionalInformation.AdditionalMonetaryTotals.Add(AMTTotalDescuentos);

            // ImporteTotalVenta
            invoice.LegalMonetaryTotal.PayableAmount.CurrencyID = detalles.Moneda;
            invoice.LegalMonetaryTotal.PayableAmount.Value = montosGlobales.ImporteTotalVenta;

            // Percepcion
            AdditionalMonetaryTotal AMTPercepcion = new AdditionalMonetaryTotal();
            AMTPercepcion.ID = "2001";
            AMTPercepcion.ReferenceAmount.CurrencyID = detalles.Moneda;
            AMTPercepcion.ReferenceAmount.Value = (decimal)montosGlobales.PercepcionBaseImponible;
            AMTPercepcion.PayableAmount.CurrencyID = detalles.Moneda;
            AMTPercepcion.PayableAmount.Value = (decimal)montosGlobales.PercepcionMonto;
            AMTPercepcion.TotalAmount.CurrencyID = detalles.Moneda;
            AMTPercepcion.TotalAmount.Value = (decimal)montosGlobales.PercepcionMontoTotal;
            
            #endregion

            // Guia De Remision
            InvoiceDocumentReference IDRGuiaDeRemision = new InvoiceDocumentReference();
            IDRGuiaDeRemision.ID = guiaDeRemision.GuiaNumeroDeGuia;
            IDRGuiaDeRemision.DocumentTypeCode = guiaDeRemision.GuiaTipoDocumento;
            invoice.DespatchDocumentReferences.Add(IDRGuiaDeRemision);

            // Documento Relacionado
            InvoiceDocumentReference IDRDocumentoRelacionado = new InvoiceDocumentReference();
            IDRDocumentoRelacionado.ID = documentoAsociado.DocumentoNumero;
            IDRDocumentoRelacionado.DocumentTypeCode = documentoAsociado.DocumentoTipo;
            invoice.AdditionalDocumentReferences.Add(IDRDocumentoRelacionado);
            
            AdditionalInformation additionalInformation = new AdditionalInformation();

            #region Leyendas
            foreach (Leyenda leyenda in listaLeyendas)
            {
                AdditionalProperty APLeyenda = new AdditionalProperty();
                APLeyenda.ID = leyenda.LeyendaCodigo;
                APLeyenda.Value = leyenda.LeyendaDescripcion;
                additionalInformation.AdditionalProperties.Add(APLeyenda);
            }
            #endregion

            // Detracciones
            AdditionalMonetaryTotal AMTDetracciones = new AdditionalMonetaryTotal();
            AMTDetracciones.ID = "2003";
            AMTDetracciones.ReferenceAmount.CurrencyID = detalles.Moneda;
            AMTDetracciones.ReferenceAmount.Value = detraccion.ValorReferencial;
            AMTDetracciones.PayableAmount.CurrencyID = detalles.Moneda;
            AMTDetracciones.PayableAmount.Value = detraccion.Monto;
            AMTDetracciones.Percent = detraccion.Porcentaje;
            additionalInformation.AdditionalMonetaryTotals.Add(AMTDetracciones);

            // Operaciones Gratuitas
            AdditionalMonetaryTotal AMTOperacionesGratuitas = new AdditionalMonetaryTotal();
            AMTOperacionesGratuitas.ID = "1004";
            AMTOperacionesGratuitas.PayableAmount.CurrencyID = detalles.Moneda;
            AMTOperacionesGratuitas.PayableAmount.Value = (decimal)montosGlobales.TVVOperacionesGratuitas;
            additionalInformation.AdditionalMonetaryTotals.Add(AMTOperacionesGratuitas);

            ublExtension.ExtensionContent.AdditionalInformation = additionalInformation;

            invoice.UBLExtensions.Extension2 = ublExtension;

            // Descuentos Globales
            invoice.LegalMonetaryTotal.AllowanceTotalAmount.CurrencyID = detalles.Moneda;
            invoice.LegalMonetaryTotal.AllowanceTotalAmount.Value = (decimal)montosGlobales.DescuentosGlobales;

            return invoice;
        }

    }
}
