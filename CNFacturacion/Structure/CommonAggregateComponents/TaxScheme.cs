using System;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class TaxScheme
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string TaxTypeCode { get; set; }
    }
}
