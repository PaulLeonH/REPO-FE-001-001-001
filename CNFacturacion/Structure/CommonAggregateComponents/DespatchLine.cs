using CNFacturacion.Structure.CommonBasicComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class DespatchLine
    {
        public int Id { get; set; }

        public InvoicedQuantity DeliveredQuantity { get; set; }

        /// <summary>
        /// cac:OrderLineReference/cbc:LineID
        /// </summary>
        public int OrderLineReferenceId { get; set; }

        public DespatchLineItem Item { get; set; }
    }
}
