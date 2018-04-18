using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class TaxCategory
    {
        public string TaxExemptionReasonCode { get; set; }

        public string TierRange { get; set; }

        public TaxScheme TaxScheme { get; set; }

        public string Id { get; set; }

        public TaxCategory()
        {
            TaxScheme = new TaxScheme();
        }
    }
}
