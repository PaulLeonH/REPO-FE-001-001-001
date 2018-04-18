using CNFacturacion.Common;
using CNFacturacion.Common.Constants;
using CNFacturacion.Structure.CommonAggregateComponents;
using CNFacturacion.Structure.CommonExtensionComponents;
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
    public class DebitNote : IXmlSerializable, IEstructuraXML
    {
        public UBLExtensions UBLExtensions { get; set; }

        public string UBLVersionID { get; set; }

        public string CustomizationID { get; set; }

        public string ID { get; set; }

        public DateTime IssueDate { get; set; }

        public string DocumentCurrencyCode { get; set; }

        public List<DiscrepancyResponse> DiscrepancyResponses { get; set; }

        public List<BillingReference> BillingReferences { get; set; }

        public List<InvoiceDocumentReference> DespatchDocumentReferences { get; set; }

        public List<InvoiceDocumentReference> AdditionalDocumentReferences { get; set; }

        public SignatureCac Signature { get; set; }

        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        public AccountingSupplierParty AccountingCustomerParty { get; set; }

        public List<TaxTotal> TaxTotals { get; set; }

        public LegalMonetaryTotal RequestedMonetaryTotal { get; set; }

        public List<VoidedDocumentLine> DebitNoteLines { get; set; }

        public IFormatProvider Formato { get; set; }

        public DebitNote()
        {
            UBLExtensions = new UBLExtensions();
            DiscrepancyResponses = new List<DiscrepancyResponse>();
            BillingReferences = new List<BillingReference>();
            DespatchDocumentReferences = new List<InvoiceDocumentReference>();
            AdditionalDocumentReferences = new List<InvoiceDocumentReference>();
            Signature = new SignatureCac();
            AccountingCustomerParty = new AccountingSupplierParty();
            AccountingSupplierParty = new AccountingSupplierParty();
            TaxTotals = new List<TaxTotal>();
            RequestedMonetaryTotal = new LegalMonetaryTotal();
            DebitNoteLines = new List<VoidedDocumentLine>();
            UBLVersionID = "2.0";
            CustomizationID = "1.0";
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
            writer.WriteAttributeString("xmlns", EspacioNombres.xmlnsDebitNote);
            writer.WriteAttributeString("xmlns:cac", EspacioNombres.cac);
            writer.WriteAttributeString("xmlns:cbc", EspacioNombres.cbc);
            writer.WriteAttributeString("xmlns:ccts", EspacioNombres.ccts);
            writer.WriteAttributeString("xmlns:ds", EspacioNombres.ds);
            writer.WriteAttributeString("xmlns:ext", EspacioNombres.ext);
            writer.WriteAttributeString("xmlns:qdt", EspacioNombres.qdt);
            writer.WriteAttributeString("xmlns:sac", EspacioNombres.sac);
            writer.WriteAttributeString("xmlns:udt", EspacioNombres.udt);
            writer.WriteAttributeString("xmlns:xsi", EspacioNombres.xsi);

            #region UBLExtensions

            {
                writer.WriteStartElement("ext:UBLExtensions");

                #region UBLExtension

                {
                    var ext2 = UBLExtensions.Extension2.ExtensionContent.AdditionalInformation;
                    writer.WriteStartElement("ext:UBLExtension");

                    #region ExtensionContent

                    {
                        writer.WriteStartElement("ext:ExtensionContent");

                        #region AdditionalInformation

                        {
                            if (ext2.AdditionalMonetaryTotals.Count > 0)
                                writer.WriteStartElement("sac:AdditionalInformation");

                            #region AdditionalMonetaryTotal

                            {
                                foreach (var additionalMonetaryTotal in ext2.AdditionalMonetaryTotals)
                                {
                                    writer.WriteStartElement("sac:AdditionalMonetaryTotal");
                                    writer.WriteElementString("cbc:ID", additionalMonetaryTotal.ID);

                                    #region PayableAmount

                                    {
                                        writer.WriteStartElement("cbc:PayableAmount");
                                        writer.WriteAttributeString("currencyID", additionalMonetaryTotal.PayableAmount.CurrencyID);
                                        writer.WriteValue(additionalMonetaryTotal.PayableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                                        writer.WriteEndElement();
                                    }

                                    #endregion PayableAmount

                                    writer.WriteEndElement();
                                }
                            }

                            #endregion AdditionalMonetaryTotal

                            #region AdditionalProperty

                            {
                                foreach (var additionalProperty in ext2.AdditionalProperties)
                                {
                                    writer.WriteStartElement("sac:AdditionalProperty");
                                    writer.WriteElementString("cbc:ID", additionalProperty.ID);

                                    #region Value

                                    writer.WriteElementString("cbc:Value", additionalProperty.Value);

                                    #endregion Value

                                    writer.WriteEndElement();
                                }
                            }

                            #endregion AdditionalProperty

                            writer.WriteEndElement();
                        }

                        #endregion AdditionalInformation

                        writer.WriteEndElement();
                    }

                    #endregion ExtensionContent

                    writer.WriteEndElement();
                }

                #endregion UBLExtension

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
            writer.WriteElementString("cbc:IssueDate", IssueDate.ToString("yyyy-MM-dd"));
            writer.WriteElementString("cbc:DocumentCurrencyCode", DocumentCurrencyCode);

            #region DiscrepancyResponse

            foreach (var discrepancy in DiscrepancyResponses)
            {
                writer.WriteStartElement("cac:DiscrepancyResponse");
                {
                    writer.WriteElementString("cbc:ReferenceID", discrepancy.ReferenceId);
                    writer.WriteElementString("cbc:ResponseCode", discrepancy.ResponseCode);
                    writer.WriteElementString("cbc:Description", discrepancy.Description);
                }
                writer.WriteEndElement();
            }

            #endregion DiscrepancyResponse

            #region BillingReference

            foreach (var item in BillingReferences)
            {
                writer.WriteStartElement("cac:BillingReference");
                {
                    writer.WriteStartElement("cac:InvoiceDocumentReference");
                    {
                        writer.WriteElementString("cbc:ID", item.InvoiceDocumentReference.ID);
                        writer.WriteElementString("cbc:DocumentTypeCode", item.InvoiceDocumentReference.DocumentTypeCode);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            #endregion BillingReference

            #region DespatchDocumentReference

            foreach (var item in DespatchDocumentReferences)
            {
                writer.WriteStartElement("cac:DespatchDocumentReference");
                {
                    writer.WriteElementString("cbc:ID", item.ID);
                    writer.WriteElementString("cbc:DocumentTypeCode", item.DocumentTypeCode);
                }
                writer.WriteEndElement();
            }

            #endregion DespatchDocumentReference

            #region AdditionalDocumentReference

            foreach (var item in AdditionalDocumentReferences)
            {
                writer.WriteStartElement("cac:AdditionalDocumentReference");
                {
                    writer.WriteElementString("cbc:ID", item.ID);
                    writer.WriteElementString("cbc:DocumentTypeCode", item.DocumentTypeCode);
                }
                writer.WriteEndElement();
            }

            #endregion AdditionalDocumentReference

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
            writer.WriteElementString("cbc:AdditionalAccountID",
                AccountingSupplierParty.AdditionalAccountId);

            #region Party

            writer.WriteStartElement("cac:Party");

            #region PartyName

            writer.WriteStartElement("cac:PartyName");

            writer.WriteStartElement("cbc:Name");
            writer.WriteString(AccountingSupplierParty.Party.PartyName.Name);
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion PartyName

            #region PostalAddress

            writer.WriteStartElement("cac:PostalAddress");
            writer.WriteElementString("cbc:ID", AccountingSupplierParty.Party.PostalAddress.ID);
            writer.WriteElementString("cbc:StreetName", AccountingSupplierParty.Party.PostalAddress.StreetName);
            writer.WriteElementString("cbc:CitySubdivisionName", AccountingSupplierParty.Party.PostalAddress.CitySubdivisionName);
            writer.WriteElementString("cbc:CityName", AccountingSupplierParty.Party.PostalAddress.CityName);
            writer.WriteElementString("cbc:CountrySubentity", AccountingSupplierParty.Party.PostalAddress.CountrySubentity);
            writer.WriteElementString("cbc:District", AccountingSupplierParty.Party.PostalAddress.District);

            #region Country

            writer.WriteStartElement("cac:Country");
            writer.WriteElementString("cbc:IdentificationCode",
                AccountingSupplierParty.Party.PostalAddress.Country.IdentificationCode);
            writer.WriteEndElement();

            #endregion Country

            writer.WriteEndElement();

            #endregion PostalAddress

            #region PartyLegalEntity

            writer.WriteStartElement("cac:PartyLegalEntity");

            writer.WriteStartElement("cbc:RegistrationName");
            writer.WriteString(AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName);
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion PartyLegalEntity

            writer.WriteEndElement();

            #endregion Party

            writer.WriteEndElement();

            #endregion AccountingSupplierParty

            #region AccountingCustomerParty

            writer.WriteStartElement("cac:AccountingCustomerParty");

            writer.WriteElementString("cbc:CustomerAssignedAccountID", AccountingCustomerParty.CustomerAssignedAccountId);
            writer.WriteElementString("cbc:AdditionalAccountID",
                AccountingCustomerParty.AdditionalAccountId);

            #region Party

            writer.WriteStartElement("cac:Party");

            #region cbc:PartyLegalEntity

            writer.WriteStartElement("cac:PartyLegalEntity");

            writer.WriteStartElement("cbc:RegistrationName");
            writer.WriteString(AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName);
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion cbc:PartyLegalEntity

            writer.WriteEndElement();

            #endregion Party

            writer.WriteEndElement();

            #endregion AccountingCustomerParty

            #region TaxTotal

            foreach (var taxTotal in TaxTotals)
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

            #endregion TaxTotal

            #region RequestedMonetaryTotal

            writer.WriteStartElement("cac:RequestedMonetaryTotal");

            writer.WriteStartElement("cbc:PayableAmount");
            writer.WriteAttributeString("currencyID", RequestedMonetaryTotal.PayableAmount.CurrencyID);
            writer.WriteValue(RequestedMonetaryTotal.PayableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion RequestedMonetaryTotal

            #region DebitNoteLines

            foreach (var line in DebitNoteLines)
            {
                writer.WriteStartElement("cac:DebitNoteLine");

                writer.WriteElementString("cbc:ID", line.ID.ToString());

                #region DebitedQuantity

                writer.WriteStartElement("cbc:DebitedQuantity");
                {
                    writer.WriteAttributeString("unitCode", line.DebitedQuantity.UnitCode);
                    writer.WriteValue(line.DebitedQuantity.Value.ToString(Formatos.FormatoNumerico, Formato));
                }
                writer.WriteEndElement();

                #endregion DebitedQuantity

                #region LineExtensionAmount

                writer.WriteStartElement("cbc:LineExtensionAmount");
                {
                    writer.WriteAttributeString("currencyID", line.LineExtensionAmount.CurrencyID);
                    writer.WriteValue(line.LineExtensionAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                }
                writer.WriteEndElement();

                #endregion LineExtensionAmount

                #region PricingReference

                writer.WriteStartElement("cac:PricingReference");

                #region AlternativeConditionPrice

                foreach (var item in line.PricingReference.AlternativeConditionPrices)
                {
                    writer.WriteStartElement("cac:AlternativeConditionPrice");

                    #region PriceAmount

                    writer.WriteStartElement("cbc:PriceAmount");
                    writer.WriteAttributeString("currencyID", item.PriceAmount.CurrencyID);
                    writer.WriteValue(item.PriceAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    writer.WriteEndElement();

                    #endregion PriceAmount

                    writer.WriteElementString("cbc:PriceTypeCode", item.PriceTypeCode);

                    writer.WriteEndElement();
                }

                #endregion AlternativeConditionPrice

                writer.WriteEndElement();

                #endregion PricingReference

                #region AllowanceCharge

                if (line.AllowanceCharge.ChargeIndicator)
                {
                    writer.WriteStartElement("cac:AllowanceCharge");

                    writer.WriteElementString("cbc:ChargeIndicator", line.AllowanceCharge.ChargeIndicator.ToString());

                    #region Amount

                    writer.WriteStartElement("cbc:Amount");
                    writer.WriteAttributeString("currencyID", line.AllowanceCharge.Amount.CurrencyID);
                    writer.WriteValue(line.AllowanceCharge.Amount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    writer.WriteEndElement();

                    #endregion Amount

                    writer.WriteEndElement();
                }

                #endregion AllowanceCharge

                #region TaxTotal

                {
                    foreach (var taxTotal in line.TaxTotals)
                    {
                        writer.WriteStartElement("cac:TaxTotal");

                        writer.WriteStartElement("cbc:TaxAmount");
                        writer.WriteAttributeString("currencyID", taxTotal.TaxAmount.CurrencyID);
                        writer.WriteString(taxTotal.TaxAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                        writer.WriteEndElement();

                        #region TaxSubtotal

                        writer.WriteStartElement("cac:TaxSubtotal");

                        #region TaxableAmount

                        if (!string.IsNullOrEmpty(taxTotal.TaxableAmount.CurrencyID))
                        {
                            writer.WriteStartElement("cbc:TaxableAmount");
                            writer.WriteAttributeString("currencyID", taxTotal.TaxableAmount.CurrencyID);
                            writer.WriteString(taxTotal.TaxableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                            writer.WriteEndElement();
                        }

                        #endregion TaxableAmount

                        writer.WriteStartElement("cbc:TaxAmount");
                        writer.WriteAttributeString("currencyID", taxTotal.TaxSubtotal.TaxAmount.CurrencyID);
                        writer.WriteString(taxTotal.TaxAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                        writer.WriteEndElement();
                        if (taxTotal.TaxSubtotal.Percent > 0)
                            writer.WriteElementString("cbc:Percent", taxTotal.TaxSubtotal.Percent.ToString(Formatos.FormatoNumerico, Formato));

                        #region TaxCategory

                        writer.WriteStartElement("cac:TaxCategory");
                        //writer.WriteElementString("cbc:ID", invoiceLine.TaxTotal.TaxSubtotal.TaxCategory.ID);
                        writer.WriteElementString("cbc:TaxExemptionReasonCode", taxTotal.TaxSubtotal.TaxCategory.TaxExemptionReasonCode);
                        if (!string.IsNullOrEmpty(taxTotal.TaxSubtotal.TaxCategory.TierRange))
                            writer.WriteElementString("cbc:TierRange", taxTotal.TaxSubtotal.TaxCategory.TierRange);

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

                        #endregion TaxCategory

                        writer.WriteEndElement();

                        #endregion TaxSubtotal

                        writer.WriteEndElement();
                    }
                }

                #endregion TaxTotal

                #region Item

                writer.WriteStartElement("cac:Item");

                #region Description

                writer.WriteElementString("cbc:Description", line.Item.Description);
                //writer.WriteStartElement("cbc:Description");
                //writer.WriteCData(invoiceLine.Item.Description);
                //writer.WriteEndElement();

                #endregion Description

                #region SellersItemIdentification

                writer.WriteStartElement("cac:SellersItemIdentification");
                writer.WriteElementString("cbc:ID", line.Item.SellersItemIdentification.ID);
                writer.WriteEndElement();

                #endregion SellersItemIdentification

                writer.WriteEndElement();

                #endregion Item

                #region Price

                writer.WriteStartElement("cac:Price");

                writer.WriteStartElement("cbc:PriceAmount");
                writer.WriteAttributeString("currencyID", line.Price.PriceAmount.CurrencyID);
                writer.WriteString(line.Price.PriceAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                writer.WriteEndElement();

                writer.WriteEndElement();

                #endregion Price

                writer.WriteEndElement();
            }

            #endregion DebitNoteLines
        }
    }
}
