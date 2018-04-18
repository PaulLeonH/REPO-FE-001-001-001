using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.SunatAggregateComponents
{
    [Serializable]
    public class AdditionalInformation
    {
        public List<AdditionalMonetaryTotal> AdditionalMonetaryTotals { get; set; }

        public List<AdditionalProperty> AdditionalProperties { get; set; }

        public SunatEmbededDespatchAdvice SunatEmbededDespatchAdvice { get; set; }

        public SunatCosts SunatCosts { get; set; }

        public SunatTransaction SunatTransaction { get; private set; }

        public AdditionalInformation()
        {
            AdditionalMonetaryTotals = new List<AdditionalMonetaryTotal>();
            AdditionalProperties = new List<AdditionalProperty>();
            SunatEmbededDespatchAdvice = new SunatEmbededDespatchAdvice();
            SunatTransaction = new SunatTransaction();
            SunatCosts = new SunatCosts();
        }
    }
}
