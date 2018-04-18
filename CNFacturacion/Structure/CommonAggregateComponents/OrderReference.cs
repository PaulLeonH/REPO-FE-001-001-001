using System;

using CNFacturacion.Structure.CommonBasicComponents;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class OrderReference
    {
        public string Id { get; set; }

        public OrderTypeCode OrderTypeCode { get; set; }

        public OrderReference()
        {
            OrderTypeCode = new OrderTypeCode();
        }
    }
}
