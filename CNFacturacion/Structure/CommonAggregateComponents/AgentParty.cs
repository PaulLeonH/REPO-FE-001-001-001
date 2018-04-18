using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class AgentParty
    {
        public PartyIdentification PartyIdentification { get; set; }

        public PartyName PartyName { get; set; }

        public PostalAddress PostalAddress { get; set; }

        public PartyLegalEntity PartyLegalEntity { get; set; }

        public AgentParty()
        {
            PartyIdentification = new PartyIdentification();
            PartyName = new PartyName();
            PostalAddress = new PostalAddress();
            PartyLegalEntity = new PartyLegalEntity();
        }
    }
}
