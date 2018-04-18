using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.SunatAggregateComponents
{
    [Serializable]
    public class AdditionalProperty
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
