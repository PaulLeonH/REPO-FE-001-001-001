﻿using CNFacturacion.Common;
using CNFacturacion.Structure.CommonAggregateComponents;
using CNFacturacion.Structure.CommonExtensionComponents;
using CNFacturacion.Structure.SunatAggregateComponents;
using CNFacturacion.Common.Constants;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace CNFacturacion.Structure.EstandarUBL
{
    [Serializable]
    public class Invoice : IXmlSerializable, IEstructuraXML
    {
        public DateTime IssueDate { get; set; }

        public UBLExtensions UBLExtensions { get; set; }

        public SignatureCac Signature { get; set; }

        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        public string InvoiceTypeCode { get; set; }

        public string ID { get; set; }

        public AccountingSupplierParty AccountingCustomerParty { get; set; }

        public List<VoidedDocumentLine> InvoiceLines { get; set; }

        public List<InvoiceDocumentReference> DespatchDocumentReferences { get; set; }

        public List<InvoiceDocumentReference> AdditionalDocumentReferences { get; set; }

        public string DocumentCurrencyCode { get; set; }

        public List<TaxTotal> TaxTotals { get; set; }

        public LegalMonetaryTotal LegalMonetaryTotal { get; set; }

        public BillingPayment PrepaidPayment { get; set; }

        public string UBLVersionID { get; set; }

        public string CustomizationID { get; set; }

        public IFormatProvider Formato { get; set; }

        public Invoice()
        {
            AccountingSupplierParty = new AccountingSupplierParty();
            AccountingCustomerParty = new AccountingSupplierParty();
            DespatchDocumentReferences = new List<InvoiceDocumentReference>();
            AdditionalDocumentReferences = new List<InvoiceDocumentReference>();
            UBLExtensions = new UBLExtensions();
            Signature = new SignatureCac();
            InvoiceLines = new List<VoidedDocumentLine>();
            TaxTotals = new List<TaxTotal>();
            LegalMonetaryTotal = new LegalMonetaryTotal();
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
            writer.WriteAttributeString("xmlns", EspacioNombres.xmlnsInvoice);
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

            writer.WriteStartElement("ext:UBLExtensions");

            #region UBLExtension

            var ext2 = UBLExtensions.Extension2.ExtensionContent.AdditionalInformation;

            writer.WriteStartElement("ext:UBLExtension");

            #region ExtensionContent

            writer.WriteStartElement("ext:ExtensionContent");

            #region AdditionalInformation

            writer.WriteStartElement("sac:AdditionalInformation");
            {
                #region AdditionalMonetaryTotal

                {
                    foreach (var additionalMonetaryTotal in ext2.AdditionalMonetaryTotals)
                    {
                        writer.WriteStartElement("sac:AdditionalMonetaryTotal");
                        if (additionalMonetaryTotal.ReferenceAmount.Value > 0)
                        {
                            writer.WriteStartElement("cbc:ID");
                            {
                                writer.WriteAttributeString("schemeID", "01");
                                writer.WriteValue("2001");
                            }
                            writer.WriteEndElement();

                            #region ReferenceAmount

                            writer.WriteStartElement("sac:ReferenceAmount");
                            {
                                writer.WriteAttributeString("currencyID", additionalMonetaryTotal.ReferenceAmount.CurrencyID);
                                writer.WriteValue(additionalMonetaryTotal.ReferenceAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                            }
                            writer.WriteEndElement();

                            #endregion ReferenceAmount

                            #region PayableAmount

                            {
                                writer.WriteStartElement("cbc:PayableAmount");
                                {
                                    writer.WriteAttributeString("currencyID", additionalMonetaryTotal.PayableAmount.CurrencyID);
                                    writer.WriteValue(additionalMonetaryTotal.PayableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                                }
                                writer.WriteEndElement();
                            }

                            #endregion PayableAmount

                            #region TotalAmount

                            {
                                writer.WriteStartElement("sac:TotalAmount");
                                {
                                    writer.WriteAttributeString("currencyID",
                                        additionalMonetaryTotal.TotalAmount.CurrencyID);
                                    writer.WriteValue(
                                        additionalMonetaryTotal.TotalAmount.Value.ToString(Formatos.FormatoNumerico,
                                            Formato));
                                }
                                writer.WriteEndElement();
                            }

                            #endregion TotalAmount
                        }
                        else
                        {
                            writer.WriteElementString("cbc:ID", additionalMonetaryTotal.ID);

                            #region PayableAmount

                            {
                                writer.WriteStartElement("cbc:PayableAmount");
                                {
                                    writer.WriteAttributeString("currencyID", additionalMonetaryTotal.PayableAmount.CurrencyID);
                                    writer.WriteValue(additionalMonetaryTotal.PayableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                                }
                                writer.WriteEndElement();
                            }
                            if (additionalMonetaryTotal.Percent > 0)
                            {
                                writer.WriteElementString("cbc:Percent",
                                    additionalMonetaryTotal.Percent.ToString(Formatos.FormatoNumerico, Formato));
                            }

                            #endregion PayableAmount
                        }

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

                #region SUNATEmbededDespatchAdvice

                // Para el caso de Factura-Guia.
                if (!string.IsNullOrEmpty(ext2.SunatEmbededDespatchAdvice.DeliveryAddress.ID))
                {
                    writer.WriteStartElement("sac:SUNATEmbededDespatchAdvice");
                    {
                        #region DeliveryAddress

                        writer.WriteStartElement("cac:DeliveryAddress");
                        {
                            writer.WriteElementString("cbc:ID", ext2.SunatEmbededDespatchAdvice.DeliveryAddress.ID);
                            writer.WriteElementString("cbc:StreetName", ext2.SunatEmbededDespatchAdvice.DeliveryAddress.StreetName);
                            if (!string.IsNullOrEmpty(ext2.SunatEmbededDespatchAdvice.DeliveryAddress.CitySubdivisionName))
                                writer.WriteElementString("cbc:CitySubdivisionName", ext2.SunatEmbededDespatchAdvice.DeliveryAddress.CitySubdivisionName);
                            writer.WriteElementString("cbc:CityName", ext2.SunatEmbededDespatchAdvice.DeliveryAddress.CityName);
                            writer.WriteElementString("cbc:CountrySubentity", ext2.SunatEmbededDespatchAdvice.DeliveryAddress.CountrySubentity);
                            writer.WriteElementString("cbc:District", ext2.SunatEmbededDespatchAdvice.DeliveryAddress.District);
                            writer.WriteStartElement("cac:Country");
                            {
                                writer.WriteElementString("cbc:IdentificationCode", ext2.SunatEmbededDespatchAdvice.DeliveryAddress.Country.IdentificationCode);
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();

                        #endregion DeliveryAddress

                        #region OriginAddress

                        writer.WriteStartElement("cac:OriginAddress");
                        {
                            writer.WriteElementString("cbc:ID", ext2.SunatEmbededDespatchAdvice.OriginAddress.ID);
                            writer.WriteElementString("cbc:StreetName", ext2.SunatEmbededDespatchAdvice.OriginAddress.StreetName);
                            if (!string.IsNullOrEmpty(ext2.SunatEmbededDespatchAdvice.OriginAddress.CitySubdivisionName))
                                writer.WriteElementString("cbc:CitySubdivisionName", ext2.SunatEmbededDespatchAdvice.OriginAddress.CitySubdivisionName);
                            writer.WriteElementString("cbc:CityName", ext2.SunatEmbededDespatchAdvice.OriginAddress.CityName);
                            writer.WriteElementString("cbc:CountrySubentity", ext2.SunatEmbededDespatchAdvice.OriginAddress.CountrySubentity);
                            writer.WriteElementString("cbc:District", ext2.SunatEmbededDespatchAdvice.OriginAddress.District);
                            writer.WriteStartElement("cac:Country");
                            {
                                writer.WriteElementString("cbc:IdentificationCode", ext2.SunatEmbededDespatchAdvice.OriginAddress.Country.IdentificationCode);
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();

                        #endregion OriginAddress

                        #region SUNATCarrierParty

                        writer.WriteStartElement("sac:SUNATCarrierParty");
                        {
                            writer.WriteElementString("cbc:CustomerAssignedAccountID", ext2.SunatEmbededDespatchAdvice.SunatCarrierParty.CustomerAssignedAccountId);
                            writer.WriteElementString("cbc:AdditionalAccountID", ext2.SunatEmbededDespatchAdvice.SunatCarrierParty.AdditionalAccountId);
                            writer.WriteStartElement("cac:Party");
                            {
                                writer.WriteStartElement("cac:PartyLegalEntity");
                                {
                                    writer.WriteElementString("cbc:RegistrationName", ext2.SunatEmbededDespatchAdvice.SunatCarrierParty.Party.PartyLegalEntity.RegistrationName);
                                }
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();

                        #endregion SUNATCarrierParty

                        #region DriverParty

                        writer.WriteStartElement("sac:DriverParty");
                        {
                            writer.WriteStartElement("cac:Party");
                            {
                                writer.WriteStartElement("cac:PartyIdentification");
                                {
                                    writer.WriteElementString("cbc:ID", ext2.SunatEmbededDespatchAdvice.DriverParty.PartyIdentification.ID.Value);
                                }
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();

                        #endregion DriverParty

                        #region SUNATRoadTransport

                        writer.WriteStartElement("sac:SUNATRoadTransport");
                        {
                            writer.WriteElementString("cbc:LicensePlateID", ext2.SunatEmbededDespatchAdvice.SunatRoadTransport.LicensePlateId);
                            writer.WriteElementString("cbc:TransportAuthorizationCode", ext2.SunatEmbededDespatchAdvice.SunatRoadTransport.TransportAuthorizationCode);
                            writer.WriteElementString("cbc:BrandName", ext2.SunatEmbededDespatchAdvice.SunatRoadTransport.BrandName);
                        }
                        writer.WriteEndElement();

                        #endregion SUNATRoadTransport

                        writer.WriteElementString("cbc:TransportModeCode", ext2.SunatEmbededDespatchAdvice.TransportModeCode);

                        #region GrossWeightMeasure

                        writer.WriteStartElement("cbc:GrossWeightMeasure");
                        {
                            writer.WriteAttributeString("unitCode", ext2.SunatEmbededDespatchAdvice.GrossWeightMeasure.UnitCode);
                            writer.WriteValue(ext2.SunatEmbededDespatchAdvice.GrossWeightMeasure.Value.ToString(Formatos.FormatoNumerico, Formato));
                        }
                        writer.WriteEndElement();

                        #endregion GrossWeightMeasure
                    }
                    writer.WriteEndElement();
                }

                #endregion SUNATEmbededDespatchAdvice

                #region SUNATCosts

                if (!string.IsNullOrEmpty(ext2.SunatCosts.RoadTransport.LicensePlateId))
                {
                    writer.WriteStartElement("sac:SUNATCosts");
                    {
                        writer.WriteStartElement("cac:RoadTransport");
                        {
                            writer.WriteElementString("cbc:LicensePlateID", ext2.SunatCosts.RoadTransport.LicensePlateId);
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }

                #endregion SUNATCosts

                #region SUNATTransaction

                if (!string.IsNullOrEmpty(ext2.SunatTransaction.Id)
                    && string.IsNullOrEmpty(ext2.SunatCosts.RoadTransport.LicensePlateId))
                {
                    writer.WriteStartElement("sac:SUNATTransaction");
                    {
                        writer.WriteElementString("cbc:ID", ext2.SunatTransaction.Id);
                    }
                    writer.WriteEndElement();
                }

                #endregion SUNATTransaction
            }
            writer.WriteEndElement();

            #endregion AdditionalInformation

            writer.WriteEndElement();

            #endregion ExtensionContent

            writer.WriteEndElement();

            #endregion UBLExtension

            #region UBLExtension

            writer.WriteStartElement("ext:UBLExtension");

            #region ExtensionContent

            writer.WriteStartElement("ext:ExtensionContent");

            // En esta zona va el certificado digital.

            writer.WriteEndElement();

            #endregion ExtensionContent

            writer.WriteEndElement();

            #endregion UBLExtension

            writer.WriteEndElement();

            #endregion UBLExtensions

            writer.WriteElementString("cbc:UBLVersionID", UBLVersionID);
            writer.WriteElementString("cbc:CustomizationID", CustomizationID);
            writer.WriteElementString("cbc:ID", ID);
            writer.WriteElementString("cbc:IssueDate", IssueDate.ToString(Formatos.FormatoFecha));
            writer.WriteElementString("cbc:InvoiceTypeCode", InvoiceTypeCode);
            writer.WriteElementString("cbc:DocumentCurrencyCode", DocumentCurrencyCode);

            #region DespatchDocumentReferences

            foreach (var reference in DespatchDocumentReferences)
            {
                writer.WriteStartElement("cac:DespatchDocumentReference");
                {
                    writer.WriteElementString("cbc:ID", reference.ID);
                    writer.WriteElementString("cbc:DocumentTypeCode", reference.DocumentTypeCode);
                }
                writer.WriteEndElement();
            }

            #endregion DespatchDocumentReferences

            #region AdditionalDocumentReferences

            foreach (var reference in AdditionalDocumentReferences)
            {
                writer.WriteStartElement("cac:AdditionalDocumentReference");
                {
                    writer.WriteElementString("cbc:ID", reference.ID);
                    writer.WriteElementString("cbc:DocumentTypeCode", reference.DocumentTypeCode);
                }
                writer.WriteEndElement();
            }

            #endregion AdditionalDocumentReferences

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
            if (!string.IsNullOrEmpty(AccountingSupplierParty.Party.PostalAddress.CitySubdivisionName))
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

            #region PrepaidPayment

            if (PrepaidPayment != null)
            {
                writer.WriteStartElement("cac:PrepaidPayment");
                {
                    writer.WriteStartElement("cbc:ID");
                    {
                        writer.WriteAttributeString("schemeID", PrepaidPayment.Id.SchemeId);
                        writer.WriteValue(PrepaidPayment.Id.Value);
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("cbc:PaidAmount");
                    {
                        writer.WriteAttributeString("currencyID", PrepaidPayment.PaidAmount.CurrencyID);
                        writer.WriteValue(PrepaidPayment.PaidAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("cbc:InstructionID");
                    {
                        writer.WriteAttributeString("schemeID", "6");
                        writer.WriteValue(PrepaidPayment.InstructionId);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            #endregion PrepaidPayment

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

            #region LegalMonetaryTotal

            writer.WriteStartElement("cac:LegalMonetaryTotal");
            {
                if (LegalMonetaryTotal.AllowanceTotalAmount.Value > 0)
                {
                    writer.WriteStartElement("cbc:AllowanceTotalAmount");
                    {
                        writer.WriteAttributeString("currencyID", LegalMonetaryTotal.AllowanceTotalAmount.CurrencyID);
                        writer.WriteValue(LegalMonetaryTotal.AllowanceTotalAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    }
                    writer.WriteEndElement();
                }
                if (LegalMonetaryTotal.PrepaidAmount.Value > 0)
                {
                    writer.WriteStartElement("cbc:PrepaidAmount");
                    {
                        writer.WriteAttributeString("currencyID", LegalMonetaryTotal.PrepaidAmount.CurrencyID);
                        writer.WriteValue(LegalMonetaryTotal.PrepaidAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    }
                    writer.WriteEndElement();
                }
                writer.WriteStartElement("cbc:PayableAmount");
                {
                    writer.WriteAttributeString("currencyID", LegalMonetaryTotal.PayableAmount.CurrencyID);
                    writer.WriteValue(LegalMonetaryTotal.PayableAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            #endregion LegalMonetaryTotal

            #region InvoiceLines

            foreach (var invoiceLine in InvoiceLines)
            {
                writer.WriteStartElement("cac:InvoiceLine");

                writer.WriteElementString("cbc:ID", invoiceLine.ID.ToString());

                #region InvoicedQuantity

                writer.WriteStartElement("cbc:InvoicedQuantity");
                writer.WriteAttributeString("unitCode", invoiceLine.InvoicedQuantity.UnitCode);
                writer.WriteValue(invoiceLine.InvoicedQuantity.Value.ToString(Formatos.FormatoNumerico, Formato));
                writer.WriteEndElement();

                #endregion InvoicedQuantity

                #region LineExtensionAmount

                writer.WriteStartElement("cbc:LineExtensionAmount");
                writer.WriteAttributeString("currencyID", invoiceLine.LineExtensionAmount.CurrencyID);
                writer.WriteValue(invoiceLine.LineExtensionAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                writer.WriteEndElement();

                #endregion LineExtensionAmount

                #region PricingReference

                writer.WriteStartElement("cac:PricingReference");

                #region AlternativeConditionPrice

                foreach (var item in invoiceLine.PricingReference.AlternativeConditionPrices)
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

                if (invoiceLine.AllowanceCharge.Amount.Value > 0)
                {
                    writer.WriteStartElement("cac:AllowanceCharge");

                    writer.WriteElementString("cbc:ChargeIndicator", invoiceLine.AllowanceCharge.ChargeIndicator.ToString().ToLower());

                    #region Amount

                    writer.WriteStartElement("cbc:Amount");
                    writer.WriteAttributeString("currencyID", invoiceLine.AllowanceCharge.Amount.CurrencyID);
                    writer.WriteValue(invoiceLine.AllowanceCharge.Amount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    writer.WriteEndElement();

                    #endregion Amount

                    writer.WriteEndElement();
                }

                #endregion AllowanceCharge

                #region TaxTotal

                {
                    foreach (var taxTotal in invoiceLine.TaxTotals)
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

                writer.WriteElementString("cbc:Description", invoiceLine.Item.Description);
                //writer.WriteStartElement("cbc:Description");
                //writer.WriteCData(invoiceLine.Item.Description);
                //writer.WriteEndElement();

                #endregion Description

                #region SellersItemIdentification

                writer.WriteStartElement("cac:SellersItemIdentification");
                writer.WriteElementString("cbc:ID", invoiceLine.Item.SellersItemIdentification.ID);
                writer.WriteEndElement();

                #endregion SellersItemIdentification

                writer.WriteEndElement();

                #endregion Item

                #region Price

                writer.WriteStartElement("cac:Price");

                writer.WriteStartElement("cbc:PriceAmount");
                writer.WriteAttributeString("currencyID", invoiceLine.Price.PriceAmount.CurrencyID);
                writer.WriteString(invoiceLine.Price.PriceAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                writer.WriteEndElement();

                writer.WriteEndElement();

                #endregion Price

                writer.WriteEndElement();
            }

            #endregion InvoiceLines
        }
    }
}
