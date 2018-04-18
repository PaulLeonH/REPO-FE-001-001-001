using System;

using CNFacturacion.Structure.CommonBasicComponents;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class Price
    {
        public PayableAmount PriceAmount { get; set; }

        public Price()
        {
            PriceAmount = new PayableAmount();
        }
    }
}
