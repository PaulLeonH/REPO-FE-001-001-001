using System;

using CNFacturacion.Structure.CommonBasicComponents;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class LegalMonetaryTotal
    {
        public PayableAmount PayableAmount { get; set; }

        public PayableAmount AllowanceTotalAmount { get; set; }

        public PayableAmount ChargeTotalAmount { get; set; }

        public PayableAmount PrepaidAmount { get; set; }

        public LegalMonetaryTotal()
        {
            PayableAmount = new PayableAmount();
            AllowanceTotalAmount = new PayableAmount();
            ChargeTotalAmount = new PayableAmount();
            PrepaidAmount = new PayableAmount();
        }
    }
}
