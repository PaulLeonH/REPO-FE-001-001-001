using CNFacturacion.Common;
using CNFacturacion.Common.Constants;
using CNFacturacion.Structure.CommonAggregateComponents;
using CNFacturacion.Structure.CommonBasicComponents;
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
    public class Perception : IXmlSerializable, IEstructuraXML
    {
        public string UBLVersionID { get; set; }

        public string CustomizationID { get; set; }

        public string ID { get; set; }

        public UBLExtensions UBLExtensions { get; set; }

        public SignatureCac Signature { get; set; }

        public string IssueDate { get; set; }

        public AgentParty AgentParty { get; set; }

        public AgentParty ReceiverParty { get; set; }

        public string SunatPerceptionSystemCode { get; set; }

        public decimal SunatPerceptionPercent { get; set; }

        public string Note { get; set; }

        public PayableAmount TotalInvoiceAmount { get; set; }

        public PayableAmount TotalPaid { get; set; }

        public List<SunatRetentionDocumentReference> SunatPerceptionDocumentReference { get; set; }

        public IFormatProvider Formato { get; set; }

        public Perception()
        {
            UBLExtensions = new UBLExtensions();
            AgentParty = new AgentParty();
            ReceiverParty = new AgentParty();
            TotalInvoiceAmount = new PayableAmount();
            TotalPaid = new PayableAmount();
            SunatPerceptionDocumentReference = new List<SunatRetentionDocumentReference>();

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
            writer.WriteAttributeString("xmlns", EspacioNombres.xmlnsPerception);
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
                    writer.WriteStartElement("ext:UBLExtension");

                    #region ExtensionContent

                    {
                        writer.WriteStartElement("ext:ExtensionContent");

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

            #region Signature

            writer.WriteStartElement("cac:Signature");
            {
                writer.WriteElementString("cbc:ID", Signature.ID);

                #region SignatoryParty

                writer.WriteStartElement("cac:SignatoryParty");
                {
                    writer.WriteStartElement("cac:PartyIdentification");
                    {
                        writer.WriteElementString("cbc:ID", Signature.SignatoryParty.PartyIdentification.ID.Value);
                    }
                    writer.WriteEndElement();

                    #region PartyName

                    writer.WriteStartElement("cac:PartyName");
                    {
                        writer.WriteStartElement("cbc:Name");
                        writer.WriteString(Signature.SignatoryParty.PartyName.Name);
                        writer.WriteEndElement();
                        //writer.WriteElementString("cbc:Name", Signature.SignatoryParty.PartyName.Name);
                    }
                    writer.WriteEndElement();

                    #endregion PartyName
                }
                writer.WriteEndElement();

                #endregion SignatoryParty

                #region DigitalSignatureAttachment

                writer.WriteStartElement("cac:DigitalSignatureAttachment");
                {
                    writer.WriteStartElement("cac:ExternalReference");
                    {
                        writer.WriteElementString("cbc:URI", Signature.DigitalSignatureAttachment.ExternalReference.URI);
                    }
                    writer.WriteEndElement();

                    #endregion DigitalSignatureAttachment
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            #endregion Signature

            writer.WriteElementString("cbc:ID", ID);
            writer.WriteElementString("cbc:IssueDate", IssueDate);

            #region AgentParty

            writer.WriteStartElement("cac:AgentParty");

            #region PartyIdentification

            writer.WriteStartElement("cac:PartyIdentification");

            writer.WriteStartElement("cbc:ID");
            writer.WriteAttributeString("schemeID", AgentParty.PartyIdentification.ID.SchemeId);
            writer.WriteValue(AgentParty.PartyIdentification.ID.Value);
            writer.WriteEndElement();

            writer.WriteEndElement();

            #endregion PartyIdentification

            #region PartyName

            writer.WriteStartElement("cac:PartyName");
            {
                writer.WriteStartElement("cbc:Name");
                {
                    writer.WriteString(AgentParty.PartyName.Name);
                }
                writer.WriteEndElement();
                //writer.WriteElementString("cbc:Name", AgentParty.PartyName.Name);
            }
            writer.WriteEndElement();

            #endregion PartyName

            #region PostalAddress

            writer.WriteStartElement("cac:PostalAddress");
            writer.WriteElementString("cbc:ID", AgentParty.PostalAddress.ID);
            writer.WriteStartElement("cbc:StreetName");
            {
                writer.WriteString(AgentParty.PostalAddress.StreetName);
            }
            writer.WriteEndElement();
            //writer.WriteElementString("cbc:StreetName", AgentParty.PostalAddress.StreetName);
            writer.WriteElementString("cbc:CitySubdivisionName", AgentParty.PostalAddress.CitySubdivisionName);
            writer.WriteElementString("cbc:CityName", AgentParty.PostalAddress.CityName);
            writer.WriteElementString("cbc:CountrySubentity", AgentParty.PostalAddress.CountrySubentity);
            writer.WriteElementString("cbc:District", AgentParty.PostalAddress.District);

            #region Country

            writer.WriteStartElement("cac:Country");
            writer.WriteElementString("cbc:IdentificationCode",
                AgentParty.PostalAddress.Country.IdentificationCode);
            writer.WriteEndElement();

            #endregion Country

            writer.WriteEndElement();

            #endregion PostalAddress

            #region PartyLegalEntity

            writer.WriteStartElement("cac:PartyLegalEntity");
            {
                writer.WriteStartElement("cbc:RegistrationName");
                {
                    writer.WriteString(AgentParty.PartyLegalEntity.RegistrationName);
                }
                writer.WriteEndElement();
                //writer.WriteElementString("cbc:RegistrationName", AgentParty.PartyLegalEntity.RegistrationName);
            }
            writer.WriteEndElement();

            #endregion PartyLegalEntity

            writer.WriteEndElement();

            #endregion AgentParty

            #region ReceiverParty

            writer.WriteStartElement("cac:ReceiverParty");
            {
                #region PartyIdentification

                writer.WriteStartElement("cac:PartyIdentification");
                {
                    writer.WriteStartElement("cbc:ID");
                    {
                        writer.WriteAttributeString("schemeID", ReceiverParty.PartyIdentification.ID.SchemeId);
                        writer.WriteValue(ReceiverParty.PartyIdentification.ID.Value);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                #endregion PartyIdentification

                #region PartyName

                writer.WriteStartElement("cac:PartyName");
                {
                    writer.WriteStartElement("cbc:Name");
                    {
                        writer.WriteString(ReceiverParty.PartyName.Name);
                    }
                    writer.WriteEndElement();
                    //writer.WriteElementString("cbc:Name", ReceiverParty.PartyName.Name);
                }
                writer.WriteEndElement();

                #endregion PartyName

                #region PostalAddress

                writer.WriteStartElement("cac:PostalAddress");
                {
                    if (!string.IsNullOrEmpty(ReceiverParty.PostalAddress.ID))
                        writer.WriteElementString("cbc:ID", ReceiverParty.PostalAddress.ID);
                    writer.WriteElementString("cbc:StreetName", ReceiverParty.PostalAddress.StreetName);
                    writer.WriteElementString("cbc:CitySubdivisionName", ReceiverParty.PostalAddress.CitySubdivisionName);
                    writer.WriteElementString("cbc:CityName", ReceiverParty.PostalAddress.CityName);
                    writer.WriteElementString("cbc:CountrySubentity", ReceiverParty.PostalAddress.CountrySubentity);
                    writer.WriteElementString("cbc:District", ReceiverParty.PostalAddress.District);

                    #region Country

                    writer.WriteStartElement("cac:Country");
                    {
                        writer.WriteElementString("cbc:IdentificationCode",
                            ReceiverParty.PostalAddress.Country.IdentificationCode);
                        writer.WriteEndElement();
                    }

                    #endregion Country
                }
                writer.WriteEndElement();

                #endregion PostalAddress

                #region PartyLegalEntity

                writer.WriteStartElement("cac:PartyLegalEntity");
                {
                    writer.WriteStartElement("cbc:RegistrationName");
                    {
                        writer.WriteString(ReceiverParty.PartyLegalEntity.RegistrationName);
                    }
                    writer.WriteEndElement();
                    //writer.WriteElementString("cbc:RegistrationName", ReceiverParty.PartyLegalEntity.RegistrationName);
                }
                writer.WriteEndElement();

                #endregion PartyLegalEntity

                writer.WriteEndElement();
            }

            #endregion ReceiverParty

            writer.WriteElementString("sac:SUNATPerceptionSystemCode", SunatPerceptionSystemCode);
            writer.WriteElementString("sac:SUNATPerceptionPercent", SunatPerceptionPercent.ToString(Formatos.FormatoNumerico, Formato));
            if (!string.IsNullOrEmpty(Note))
                writer.WriteElementString("cbc:Note", Note);

            writer.WriteStartElement("cbc:TotalInvoiceAmount");
            {
                writer.WriteAttributeString("currencyID", TotalInvoiceAmount.CurrencyID);
                writer.WriteValue(TotalInvoiceAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
            }
            writer.WriteEndElement();

            writer.WriteStartElement("sac:SUNATTotalCashed");
            {
                writer.WriteAttributeString("currencyID", TotalPaid.CurrencyID);
                writer.WriteValue(TotalPaid.Value.ToString(Formatos.FormatoNumerico, Formato));
            }
            writer.WriteEndElement();

            #region SUNATPerceptionDocumentReference

            foreach (var info in SunatPerceptionDocumentReference)
            {
                writer.WriteStartElement("sac:SUNATPerceptionDocumentReference");

                #region ID

                writer.WriteStartElement("cbc:ID");
                {
                    writer.WriteAttributeString("schemeID", info.Id.SchemeId);
                    writer.WriteValue(info.Id.Value);
                }
                writer.WriteEndElement();

                #endregion ID

                writer.WriteElementString("cbc:IssueDate", info.IssueDate);

                #region TotalInvoiceAmount

                writer.WriteStartElement("cbc:TotalInvoiceAmount");
                {
                    writer.WriteAttributeString("currencyID", info.TotalInvoiceAmount.CurrencyID);
                    writer.WriteValue(info.TotalInvoiceAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                }
                writer.WriteEndElement();

                #endregion TotalInvoiceAmount

                #region Payment

                writer.WriteStartElement("cac:Payment");
                {
                    writer.WriteElementString("cbc:ID", info.Payment.IdPayment.ToString());

                    writer.WriteStartElement("cbc:PaidAmount");
                    {
                        writer.WriteAttributeString("currencyID", info.Payment.PaidAmount.CurrencyID);
                        writer.WriteValue(info.Payment.PaidAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                        writer.WriteEndElement();
                    }
                    writer.WriteElementString("cbc:PaidDate", info.Payment.PaidDate);
                }
                writer.WriteEndElement();

                #endregion Payment

                #region SUNATPerceptionInformation

                writer.WriteStartElement("sac:SUNATPerceptionInformation");
                {
                    #region SUNATPerceptionAmount

                    writer.WriteStartElement("sac:SUNATPerceptionAmount");
                    {
                        writer.WriteAttributeString("currencyID", info.SunatRetentionInformation.SunatRetentionAmount.CurrencyID);
                        writer.WriteValue(info.SunatRetentionInformation.SunatRetentionAmount.Value.ToString(Formatos.FormatoNumerico, Formato));
                    }
                    writer.WriteEndElement();

                    #endregion SUNATPerceptionAmount

                    writer.WriteElementString("sac:SUNATPerceptionDate", info.SunatRetentionInformation.SunatRetentionDate);

                    #region SUNATNetTotalCashed

                    writer.WriteStartElement("sac:SUNATNetTotalCashed");
                    {
                        writer.WriteAttributeString("currencyID", info.SunatRetentionInformation.SunatNetTotalPaid.CurrencyID);
                        writer.WriteValue(info.SunatRetentionInformation.SunatNetTotalPaid.Value.ToString(Formatos.FormatoNumerico, Formato));
                    }
                    writer.WriteEndElement();

                    #endregion SUNATNetTotalCashed

                    #region ExchangeRate

                    writer.WriteStartElement("cac:ExchangeRate");
                    {
                        writer.WriteElementString("cbc:SourceCurrencyCode", info.SunatRetentionInformation.ExchangeRate.SourceCurrencyCode);
                        writer.WriteElementString("cbc:TargetCurrencyCode", info.SunatRetentionInformation.ExchangeRate.TargetCurrencyCode);
                        writer.WriteElementString("cbc:CalculationRate", info.SunatRetentionInformation.ExchangeRate.CalculationRate.ToString(Formatos.FormatoNumerico, Formato));
                        writer.WriteElementString("cbc:Date",
                            !string.IsNullOrEmpty(info.SunatRetentionInformation.ExchangeRate.Date)
                                ? info.SunatRetentionInformation.ExchangeRate.Date
                                : info.SunatRetentionInformation.SunatRetentionDate);
                    }
                    writer.WriteEndElement();

                    #endregion ExchangeRate
                }
                writer.WriteEndElement();

                #endregion SUNATPerceptionInformation

                writer.WriteEndElement();
            }

            #endregion SUNATPerceptionDocumentReference
        }
    }
}
