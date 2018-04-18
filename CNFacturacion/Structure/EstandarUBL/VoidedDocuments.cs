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
    public class VoidedDocuments : IXmlSerializable, IEstructuraXML
    {
        public UBLExtensions UBLExtensions { get; set; }

        public string UBLVersionID { get; set; }

        public string CustomizationID { get; set; }

        public string ID { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ReferenceDate { get; set; }

        public SignatureCac Signature { get; set; }

        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        public List<VoidedDocumentsLine> VoidedDocumentsLines { get; set; }

        public IFormatProvider Formato { get; set; }

        public VoidedDocuments()
        {
            UBLExtensions = new UBLExtensions();
            Signature = new SignatureCac();
            AccountingSupplierParty = new AccountingSupplierParty();
            VoidedDocumentsLines = new List<VoidedDocumentsLine>();
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
            writer.WriteAttributeString("xmlns", EspacioNombres.xmlnsVoidedDocuments);
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
            writer.WriteElementString("cbc:AdditionalAccountID",
                AccountingSupplierParty.AdditionalAccountId);

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

            #region VoidedDocumentsLines

            foreach (var item in VoidedDocumentsLines)
            {
                writer.WriteStartElement("sac:VoidedDocumentsLine");
                {
                    writer.WriteElementString("cbc:LineID", item.LineId.ToString());
                    writer.WriteElementString("cbc:DocumentTypeCode", item.DocumentTypeCode);
                    writer.WriteElementString("sac:DocumentSerialID", item.DocumentSerialId);
                    writer.WriteElementString("sac:DocumentNumberID", item.DocumentNumberId.ToString());
                    writer.WriteElementString("sac:VoidReasonDescription", item.VoidReasonDescription);
                }
                writer.WriteEndElement();
            }

            #endregion VoidedDocumentsLines
        }
    }
}
