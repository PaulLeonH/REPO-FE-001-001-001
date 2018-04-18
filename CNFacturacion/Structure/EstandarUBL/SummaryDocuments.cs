using CNFacturacion.Common;
using CNFacturacion.Common.Constants;
using CNFacturacion.Structure.CommonAggregateComponents;
using CNFacturacion.Structure.CommonExtensionComponents;
using CNFacturacion.Structure.SunatAggregateComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CNFacturacion.Structure.EstandarUBL
{
    [Serializable]
    public class SummaryDocuments : IXmlSerializable, IEstructuraXML
    {
        public UBLExtensions UBLExtensions { get; set; }

        public string UBLVersionID { get; set; }

        public string CustomizationID { get; set; }

        public string ID { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ReferenceDate { get; set; }

        public SignatureCac Signature { get; set; }

        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        public List<VoidedDocumentsLine> SummaryDocumentsLines { get; set; }

        public IFormatProvider Formato { get; set; }

        public SummaryDocuments()
        {
            UBLExtensions = new UBLExtensions();
            Signature = new SignatureCac();
            AccountingSupplierParty = new AccountingSupplierParty();
            SummaryDocumentsLines = new List<VoidedDocumentsLine>();
            UBLVersionID = "2.0";
            CustomizationID = "1.1";
            Formato = new System.Globalization.CultureInfo(Formatos.Cultura);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("xmlns", EspacioNombres.xmlnsSummaryDocuments);
            writer.WriteAttributeString("xmlns:cac", EspacioNombres.cac);
            writer.WriteAttributeString("xmlns:cbc", EspacioNombres.cbc);
            writer.WriteAttributeString("xmlns:ds", EspacioNombres.ds);
            writer.WriteAttributeString("xmlns:ext", EspacioNombres.ext);
            writer.WriteAttributeString("xmlns:sac", EspacioNombres.sac);
            writer.WriteAttributeString("xmlns:xsi", EspacioNombres.xsi);

            #region UBLExtensions

            {
                writer.WriteStartElement("ext:UBLExtensions");

                #region UBLExtension

                {
                    writer.WriteStartElement("ext:UBLExtension");

                    #region ExtensionContent

                    {
                        writer.WriteStartElement("ext:ExtensionContent");

                        // En esta zona va el certificado digital.

                        writer.WriteEndElement();
                    }

                    #endregion ExtensionContent

                    writer.WriteEndElement();
                }

                #endregion UBLExtension

                writer.WriteEndElement();
            }

            #endregion UBLExtensions

            writer.WriteElementString("cbc:UBLVersionID", UBLVersionID);
            writer.WriteElementString("cbc:CustomizationID", CustomizationID);
            writer.WriteElementString("cbc:ID", ID);
            writer.WriteElementString("cbc:ReferenceDate", ReferenceDate.ToString("yyyy-MM-dd"));
            writer.WriteElementString("cbc:IssueDate", IssueDate.ToString("yyyy-MM-dd"));

            #region Signature

            writer.WriteStartElement("cac:Signature");
            writer.WriteElementString("cbc:ID", Signature.ID);

            #region SignatoryParty

            writer.WriteStartElement("cac:SignatoryParty");

            writer.WriteStartElement("cac:PartyIdentification");
            writer.WriteElementString("cbc:ID", Signature.SignatoryParty.PartyIdentification.ID.Value);
            writer.WriteEndElement();

            #region PartyName

            writer.WriteStartElement("cac:PartyName");

            //writer.WriteStartElement("cbc:Name");
            //writer.WriteCData(Signature.SignatoryParty.PartyName.Name);
            //writer.WriteEndElement();
            writer.WriteElementString("cbc:Name", Signature.SignatoryParty.PartyName.Name);

            writer.WriteEndElement();

            #endregion PartyName

            writer.WriteEndElement();

            #endregion SignatoryParty

            #region DigitalSignatureAttachment

            writer.WriteStartElement("cac:DigitalSignatureAttachment");

            writer.WriteStartElement("cac:ExternalReference");
            writer.WriteElementString("cbc:URI", Signature.DigitalSignatureAttachment.ExternalReference.URI.Trim());
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion DigitalSignatureAttachment

            writer.WriteEndElement();

            #endregion Signature

            #region AccountingSupplierParty

            writer.WriteStartElement("cac:AccountingSupplierParty");

            writer.WriteElementString("cbc:CustomerAssignedAccountID", AccountingSupplierParty.CustomerAssignedAccountId);
            writer.WriteElementString("cbc:AdditionalAccountID", AccountingSupplierParty.AdditionalAccountId);

            #region Party

            writer.WriteStartElement("cac:Party");

            #region PartyLegalEntity

            writer.WriteStartElement("cac:PartyLegalEntity");

            {
                writer.WriteStartElement("cbc:RegistrationName");
                writer.WriteString(AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            #endregion PartyLegalEntity

            writer.WriteEndElement();

            #endregion Party

            writer.WriteEndElement();

            #endregion AccountingSupplierParty

            #region SummaryDocumentsLines

            foreach (var item in SummaryDocumentsLines)
            {
                writer.WriteStartElement("sac:SummaryDocumentsLine");
                {
                    writer.WriteElementString("cbc:LineID", item.LineId.ToString());
                    writer.WriteElementString("cbc:DocumentTypeCode", item.DocumentTypeCode);
                    writer.WriteElementString("cbc:ID", item.Id);

                    if (!string.IsNullOrEmpty(item.AccountingCustomerParty.AdditionalAccountId))
                    {
                        writer.WriteStartElement("cac:AccountingCustomerParty");
                        {
                            writer.WriteElementString("cbc:CustomerAssignedAccountID", item.AccountingCustomerParty.CustomerAssignedAccountId);
                            writer.WriteElementString("cbc:AdditionalAccountID", item.AccountingCustomerParty.AdditionalAccountId);
                        }
                        writer.WriteEndElement();
                    }
                    if (!string.IsNullOrEmpty(item.BillingReference.InvoiceDocumentReference.ID))
                    {
                        writer.WriteStartElement("cac:BillingReference");
                        {
                            writer.WriteStartElement("cac:InvoiceDocumentReference");
                            {
                                writer.WriteElementString("cbc:ID", item.BillingReference.InvoiceDocumentReference.ID);
                                writer.WriteElementString("cbc:DocumentTypeCode", item.BillingReference.InvoiceDocumentReference.DocumentTypeCode);
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    if (!string.IsNullOrEmpty(item.SUNATPerceptionSummaryDocumentReference.SUNATPerceptionSystemCode))
                    {
                        writer.WriteStartElement("sac:SUNATPerceptionSummaryDocumentReference");
                        {
                            writer.WriteElementString("sac:SUNATPerceptionSystemCode", item.SUNATPerceptionSummaryDocumentReference.SUNATPerceptionSystemCode);
                            writer.WriteElementString("sac:SUNATPerceptionPercent", item.SUNATPerceptionSummaryDocumentReference.SUNATPerceptionPercent);
                            writer.WriteStartElement("cbc:TotalInvoiceAmount");
                            {
                                writer.WriteAttributeString("currencyID", item.SUNATPerceptionSummaryDocumentReference.TotalInvoiceAmount.CurrencyID);
                                writer.WriteValue(item.SUNATPerceptionSummaryDocumentReference.TotalInvoiceAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                            }
                            writer.WriteEndElement();
                            writer.WriteStartElement("sac:SUNATTotalCashed");
                            {
                                writer.WriteAttributeString("currencyID", item.SUNATPerceptionSummaryDocumentReference.SUNATTotalCashed.CurrencyID);
                                writer.WriteValue(item.SUNATPerceptionSummaryDocumentReference.SUNATTotalCashed.Value.ToString(Formatos.FormatoNumerico, Formato));
                            }
                            writer.WriteEndElement();
                            writer.WriteStartElement("cbc:TaxableAmount");
                            {
                                writer.WriteAttributeString("currencyID", item.SUNATPerceptionSummaryDocumentReference.TaxableAmount.CurrencyID);
                                writer.WriteValue(item.SUNATPerceptionSummaryDocumentReference.TaxableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                            }
                            writer.WriteEndElement();

                        }
                        writer.WriteEndElement();
                    }
                    if (item.ConditionCode.HasValue)
                    {
                        writer.WriteStartElement("cac:Status");
                        {
                            writer.WriteElementString("cbc:ConditionCode", item.ConditionCode.Value.ToString());
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteStartElement("sac:TotalAmount");
                    {
                        writer.WriteAttributeString("currencyID", item.TotalAmount.CurrencyID);
                        writer.WriteValue(item.TotalAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    }
                    writer.WriteEndElement();

                    foreach (var billing in item.BillingPayments)
                    {
                        writer.WriteStartElement("sac:BillingPayment");
                        {
                            writer.WriteStartElement("cbc:PaidAmount");
                            {
                                writer.WriteAttributeString("currencyID", item.TotalAmount.CurrencyID);
                                writer.WriteValue(billing.PaidAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                            }
                            writer.WriteEndElement();
                            writer.WriteElementString("cbc:InstructionID", billing.InstructionId);
                        }
                        writer.WriteEndElement();
                    }

                    writer.WriteStartElement("cac:AllowanceCharge");
                    {
                        writer.WriteElementString("cbc:ChargeIndicator", item.AllowanceCharge.ChargeIndicator ? "true" : "false");

                        writer.WriteStartElement("cbc:Amount");
                        {
                            writer.WriteAttributeString("currencyID", item.AllowanceCharge.Amount.CurrencyID);
                            writer.WriteValue(item.AllowanceCharge.Amount.Value.ToString(Formatos.FormatoNumerico, Formato));
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    foreach (var taxTotal in item.TaxTotals)
                    {
                        writer.WriteStartElement("cac:TaxTotal");

                        writer.WriteStartElement("cbc:TaxAmount");
                        writer.WriteAttributeString("currencyID", taxTotal.TaxAmount.CurrencyID);
                        writer.WriteString(taxTotal.TaxAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                        writer.WriteEndElement();

                        #region TaxSubtotal

                        {
                            writer.WriteStartElement("cac:TaxSubtotal");

                            writer.WriteStartElement("cbc:TaxAmount");
                            writer.WriteAttributeString("currencyID", taxTotal.TaxSubtotal.TaxAmount.CurrencyID);
                            writer.WriteString(taxTotal.TaxAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                            writer.WriteEndElement();

                            #region TaxCategory

                            {
                                writer.WriteStartElement("cac:TaxCategory");

                                #region TaxScheme

                                {
                                    writer.WriteStartElement("cac:TaxScheme");

                                    writer.WriteElementString("cbc:ID", taxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID);
                                    writer.WriteElementString("cbc:Name", taxTotal.TaxSubtotal.TaxCategory.TaxScheme.Name);
                                    writer.WriteElementString("cbc:TaxTypeCode", taxTotal.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode);

                                    writer.WriteEndElement();
                                }

                                #endregion TaxScheme

                                writer.WriteEndElement();
                            }

                            #endregion TaxCategory

                            writer.WriteEndElement();
                        }

                        #endregion TaxSubtotal

                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }

            #endregion SummaryDocumentsLines
        }
    }
}
