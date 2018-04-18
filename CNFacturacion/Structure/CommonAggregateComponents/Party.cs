using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class Party
    {
        public PartyName PartyName { get; set; }

        public PostalAddress PostalAddress { get; set; }

        public PartyLegalEntity PartyLegalEntity { get; set; }

        public PhysicalLocation PhysicalLocation { get; set; }

        public Party()
        {
            PartyName = new PartyName();
            PostalAddress = new PostalAddress();
            PartyLegalEntity = new PartyLegalEntity();
            PhysicalLocation = new PhysicalLocation();
        }
    }
}
