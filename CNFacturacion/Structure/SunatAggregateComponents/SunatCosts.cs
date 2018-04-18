using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.SunatAggregateComponents
{
    [Serializable]
    public class SunatCosts
    {
        public SunatRoadTransport RoadTransport { get; set; }

        public SunatCosts()
        {
            RoadTransport = new SunatRoadTransport();
        }
    }
}
