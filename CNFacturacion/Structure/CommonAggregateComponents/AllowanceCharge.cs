using System;

using CNFacturacion.Structure.CommonBasicComponents;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class AllowanceCharge
    {
        public bool ChargeIndicator { get; set; }

        public PayableAmount Amount { get; set; }

        public AllowanceCharge()
        {
            Amount = new PayableAmount();
        }
    }
}
