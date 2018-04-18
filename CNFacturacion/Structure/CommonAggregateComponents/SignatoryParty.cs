using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class SignatoryParty
    {
        public PartyIdentification PartyIdentification { get; set; }

        public PartyName PartyName { get; set; }

        public SignatoryParty()
        {
            PartyIdentification = new PartyIdentification();
            PartyName = new PartyName();
        }
    }
}
