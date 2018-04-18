using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class ExchangeRate
    {
        public string SourceCurrencyCode { get; set; }

        public string TargetCurrencyCode { get; set; }

        public decimal CalculationRate { get; set; }

        public string Date { get; set; }
    }
}
