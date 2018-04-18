using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.SunatAggregateComponents
{
    [Serializable]
    public class SunatRoadTransport
    {
        public string LicensePlateId { get; set; }

        public string TransportAuthorizationCode { get; set; }

        public string BrandName { get; set; }
    }
}
