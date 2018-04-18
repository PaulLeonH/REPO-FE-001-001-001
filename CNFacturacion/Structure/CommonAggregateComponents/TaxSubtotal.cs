using System;
using CNFacturacion.Structure.CommonBasicComponents;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class TaxSubtotal
    {
        public PayableAmount TaxAmount { get; set; }

        public TaxCategory TaxCategory { get; set; }

        public decimal Percent { get; set; }

        public TaxSubtotal()
        {
            TaxAmount = new PayableAmount();
            TaxCategory = new TaxCategory();
        }
    }
}
