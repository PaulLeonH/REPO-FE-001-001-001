using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class BillingReference : IEquatable<BillingReference>
    {
        public InvoiceDocumentReference InvoiceDocumentReference { get; set; }

        public BillingReference()
        {
            InvoiceDocumentReference = new InvoiceDocumentReference();
        }

        public bool Equals(BillingReference other)
        {
            return InvoiceDocumentReference.Equals(other.InvoiceDocumentReference);
        }

        public override int GetHashCode()
        {
            return InvoiceDocumentReference.GetHashCode();
        }
    }
}
