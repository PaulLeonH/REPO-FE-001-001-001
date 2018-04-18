using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class DespatchLineItem
    {
        public string Description { get; set; }

        /// <remarks>
        /// cac:SellersItemIdentification/cbc:ID
        /// </remarks>
        public string SellersIdentificationId { get; set; }
    }
}
