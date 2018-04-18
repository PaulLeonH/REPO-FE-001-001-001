using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class InvoiceDocumentReference : IEquatable<InvoiceDocumentReference>
    {
        public string ID { get; set; }

        public string DocumentTypeCode { get; set; }

        public InvoiceDocumentReference()
        {
            ID = string.Empty;
        }

        public bool Equals(InvoiceDocumentReference other)
        {
            if (other == null) return false;

            if (string.IsNullOrEmpty(ID))
                return false;
            return ID.Equals(other.ID);
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(ID))
                return base.GetHashCode();

            return ID.GetHashCode();
        }
    }
}
