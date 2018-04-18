using CDFacturacion.ResumenDiario;
using CNFacturacion.CommonDTO.Exchange;
using CNFacturacion.Services;
using CNFacturacion.Signed;
using CNFacturacion.Structure.CommonAggregateComponents;
using CNFacturacion.Structure.EstandarUBL;
using CNFacturacion.Structure.SunatAggregateComponents;
using CNFacturacion.GenXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.GenXML
{
    public class XMLResumenDiario
    {

        private static readonly ISerializador _serializador = new Serializador();
        private static readonly ICertificador _certificador = new Certificador();
        private static readonly IServicioSunatDocumentos _servicioDocumentoSunat = new ServicioSunatDocumentos();

        public static async Task<EnviarDocumentoResponse> RutaXMLAsync(ResumenDiario resDiario)
        {
            try
            {
                SummaryDocuments summaryDocument = FillXML(resDiario);
                var TramaXMLSinFirma = await _serializador.GenerarXml(summaryDocument);

                var firmadoRequest = new FirmadoRequest
                {
                    TramaXmlSinFirma = TramaXMLSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(@"C:\Users\Diego-pc\Desktop\FacturacionSUNAT\CNFacturacion\TemporaryFacturacion.pfx")),
                    PasswordCertificado = "123456789",
                    UnSoloNodoExtension = true
                };

                var respuestaFirmado = await _certificador.FirmarXml(firmadoRequest);

                var response = new EnviarDocumentoResponse();
                var nombreArchivo = $"{resDiario.Emisor.DocumentoNumero}-{resDiario.Detalles.ID}";
                var tramaZip = await _serializador.GenerarZip(respuestaFirmado.TramaXmlFirmado, nombreArchivo);

                _servicioDocumentoSunat.Inicializar(new ParametrosConexion
                {
                    Ruc = resDiario.Emisor.DocumentoNumero,
                    UserName = "MODDATOS",
                    Password = "moddatos",
                    EndPointUrl = @"https://e-beta.sunat.gob.pe:443/ol-ti-itcpfegem-beta/billService",
                });

                var resultado = _servicioDocumentoSunat.EnviarResumen(new DocumentoSunat
                {
                    NombreArchivo = $"{nombreArchivo}.zip",
                    TramaXml = tramaZip
                });


                var consultaTicketRequest = new ConsultaTicketRequest
                {
                    Ruc = resDiario.Emisor.DocumentoNumero,
                    UsuarioSol = $"{resDiario.Emisor.DocumentoNumero}MODDATOS",
                    ClaveSol = "moddatos",
                    EndPointUrl = @"https://e-beta.sunat.gob.pe:443/ol-ti-itcpfegem-beta/billService",
                    IdDocumento = resDiario.Detalles.ID,
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

        public static SummaryDocuments FillXML(ResumenDiario resDiario)
        {
            #region Instanciacion
            Emisor emisor = resDiario.Emisor;
            Detalles detalles = resDiario.Detalles;
            List<ItemResumenDiario> listaItems = detalles.ListaItems;
            #endregion

            SummaryDocuments summaryDocuments = new SummaryDocuments();

            summaryDocuments.UBLVersionID = "2.0";
            summaryDocuments.CustomizationID = "1.1";
            summaryDocuments.ID = detalles.ID;
            summaryDocuments.IssueDate = detalles.FechaEmision;
            summaryDocuments.ReferenceDate = detalles.FechaEmisionDocumentos;

            #region Firma
            SignatureCac signature = new SignatureCac();
            signature.ID = emisor.FirmaDigital;
            signature.SignatoryParty.PartyIdentification.ID.Value = emisor.DocumentoNumero;
            signature.SignatoryParty.PartyName.Name = emisor.Nom_RazonSoc;
            signature.DigitalSignatureAttachment.ExternalReference.URI = "#SignatureSP";
            summaryDocuments.Signature = signature;
            #endregion

            #region Emisor
            summaryDocuments.AccountingSupplierParty.CustomerAssignedAccountId = emisor.DocumentoNumero;
            summaryDocuments.AccountingSupplierParty.AdditionalAccountId = emisor.DocumentoTipo;

            Party partyEmisor = new Party();
            partyEmisor.PartyLegalEntity.RegistrationName = emisor.Nom_RazonSoc;
            summaryDocuments.AccountingSupplierParty.Party = partyEmisor;
            #endregion

            #region Items
            foreach (ItemResumenDiario item in listaItems)
            {
                VoidedDocumentsLine summaryDocumentsLine = new VoidedDocumentsLine();
                summaryDocumentsLine.LineId = item.LineID;
                summaryDocumentsLine.Id = item.ID;
                summaryDocumentsLine.DocumentTypeCode = item.TipoDocumento;

                // Adquiriente O Usuario
                summaryDocumentsLine.AccountingCustomerParty.CustomerAssignedAccountId = item.AdquirienteNumeroDocumento;
                summaryDocumentsLine.AccountingCustomerParty.AdditionalAccountId = item.AdquirienteTipoDocumento;


                // Numero de Serie De Boleta Que Modifica
                summaryDocumentsLine.BillingReference.InvoiceDocumentReference.ID = item.DocumentoAdjuntadoSerie;
                summaryDocumentsLine.BillingReference.InvoiceDocumentReference.DocumentTypeCode = item.DocumentoAdjuntadoTipo;


                // Percepcion
                summaryDocumentsLine.SUNATPerceptionSummaryDocumentReference.SUNATPerceptionSystemCode = item.PercepcionRegimen;
                summaryDocumentsLine.SUNATPerceptionSummaryDocumentReference.SUNATPerceptionPercent = item.PercepcionTasa;
                summaryDocumentsLine.SUNATPerceptionSummaryDocumentReference.TotalInvoiceAmount.CurrencyID = detalles.Moneda;
                summaryDocumentsLine.SUNATPerceptionSummaryDocumentReference.TotalInvoiceAmount.Value = item.PercepcionMonto;
                summaryDocumentsLine.SUNATPerceptionSummaryDocumentReference.SUNATTotalCashed.CurrencyID = detalles.Moneda;
                summaryDocumentsLine.SUNATPerceptionSummaryDocumentReference.SUNATTotalCashed.Value = item.PercepcionMontoIncluidoPer;
                summaryDocumentsLine.SUNATPerceptionSummaryDocumentReference.TaxableAmount.CurrencyID = detalles.Moneda;
                summaryDocumentsLine.SUNATPerceptionSummaryDocumentReference.TaxableAmount.Value = item.PercepcionBaseImponible;

                summaryDocumentsLine.ConditionCode = item.ItemEstado;

                // Operaciones Gravadas
                BillingPayment BPOperacionesGravadas = new BillingPayment();
                BPOperacionesGravadas.PaidAmount.CurrencyID = detalles.Moneda;
                BPOperacionesGravadas.PaidAmount.Value = item.TVVOperacionesGravadas;
                BPOperacionesGravadas.InstructionId = "01";
                summaryDocumentsLine.BillingPayments.Add(BPOperacionesGravadas);

                // Operaciones Exoneradas
                BillingPayment BPOperacionesExoneradas = new BillingPayment();
                BPOperacionesExoneradas.PaidAmount.CurrencyID = detalles.Moneda;
                BPOperacionesExoneradas.PaidAmount.Value = item.TVVOperacionesExoneradas;
                BPOperacionesExoneradas.InstructionId = "02";
                summaryDocumentsLine.BillingPayments.Add(BPOperacionesExoneradas);

                // Operaciones Exoneradas
                BillingPayment BPOperacionesInafectas = new BillingPayment();
                BPOperacionesInafectas.PaidAmount.CurrencyID = detalles.Moneda;
                BPOperacionesInafectas.PaidAmount.Value = item.TVVOperacionesInafectas;
                BPOperacionesInafectas.InstructionId = "03";
                summaryDocumentsLine.BillingPayments.Add(BPOperacionesInafectas);

                // Operaciones Gratuitas
                BillingPayment BPOperacionesGratuitas = new BillingPayment();
                BPOperacionesGratuitas.PaidAmount.CurrencyID = detalles.Moneda;
                BPOperacionesGratuitas.PaidAmount.Value = item.TVVOperacionesGratuitas;
                BPOperacionesGratuitas.InstructionId = "05";
                summaryDocumentsLine.BillingPayments.Add(BPOperacionesGratuitas);

                // Sumatoria Otros Cargos
                summaryDocumentsLine.AllowanceCharge.ChargeIndicator = !item.SumatorioOtrosCargosEsDescuento;
                summaryDocumentsLine.AllowanceCharge.Amount.CurrencyID = detalles.Moneda;
                summaryDocumentsLine.AllowanceCharge.Amount.Value = item.SumatorioOtrosCargos;


                // Total IGV
                TaxTotal TTTotalIGV = new TaxTotal();
                TTTotalIGV.TaxAmount.CurrencyID = detalles.Moneda;
                TTTotalIGV.TaxAmount.Value = item.MontoIGV;
                TTTotalIGV.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
                TTTotalIGV.TaxSubtotal.TaxAmount.Value = item.MontoIGV;
                TTTotalIGV.TaxSubtotal.TaxCategory.TaxScheme.ID = "1000";
                TTTotalIGV.TaxSubtotal.TaxCategory.TaxScheme.Name = "IGV";
                TTTotalIGV.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "VAT";
                summaryDocumentsLine.TaxTotals.Add(TTTotalIGV);

                // Total ISC
                TaxTotal TTTotalISC = new TaxTotal();
                TTTotalISC.TaxAmount.CurrencyID = detalles.Moneda;
                TTTotalISC.TaxAmount.Value = item.MontoISC;
                TTTotalISC.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
                TTTotalISC.TaxSubtotal.TaxAmount.Value = item.MontoISC;
                TTTotalISC.TaxSubtotal.TaxCategory.TaxScheme.ID = "2000";
                TTTotalISC.TaxSubtotal.TaxCategory.TaxScheme.Name = "ISC";
                TTTotalISC.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "EXC";
                summaryDocumentsLine.TaxTotals.Add(TTTotalISC);

                // Total Otros Tributos
                TaxTotal TTTotalOtrosTributos = new TaxTotal();
                TTTotalOtrosTributos.TaxAmount.CurrencyID = detalles.Moneda;
                TTTotalOtrosTributos.TaxAmount.Value = item.SumatoriaOtrosTributos;
                TTTotalOtrosTributos.TaxSubtotal.TaxAmount.CurrencyID = detalles.Moneda;
                TTTotalOtrosTributos.TaxSubtotal.TaxAmount.Value = item.SumatoriaOtrosTributos;
                TTTotalOtrosTributos.TaxSubtotal.TaxCategory.TaxScheme.ID = "9999";
                TTTotalOtrosTributos.TaxSubtotal.TaxCategory.TaxScheme.Name = "OTROS";
                TTTotalOtrosTributos.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode = "OTH";
                summaryDocumentsLine.TaxTotals.Add(TTTotalOtrosTributos);


                // Importe Total de la Venta
                summaryDocumentsLine.TotalAmount.CurrencyID = detalles.Moneda;
                summaryDocumentsLine.TotalAmount.Value = item.ItemImporteTotal;

                summaryDocuments.SummaryDocumentsLines.Add(summaryDocumentsLine);
            }
            #endregion


            return summaryDocuments;
        }
    }
}
