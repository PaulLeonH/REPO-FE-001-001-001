using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class Item
    {
        public string Description { get; set; }

        public SellersItemIdentification SellersItemIdentification { get; set; }

        public Item()
        {
            SellersItemIdentification = new SellersItemIdentification();
        }
    }

    [Serializable]
    public class SellersItemIdentification
    {
        public string ID { get; set; }
    }
}
