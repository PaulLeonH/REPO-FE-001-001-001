using System;

using CNFacturacion.Structure.CommonBasicComponents;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class AlternativeConditionPrice
    {
        public PayableAmount PriceAmount { get; set; }

        public string PriceTypeCode { get; set; }

        public AlternativeConditionPrice()
        {
            PriceAmount = new PayableAmount();
        }
    }
}
