using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class CarrierParty
    {
        public PartyIdentification PartyIdentification { get; set; }

        public PartyLegalEntity PartyLegalEntity { get; set; }

        public CarrierParty()
        {
            PartyIdentification = new PartyIdentification();
            PartyLegalEntity = new PartyLegalEntity();
        }
    }
}
