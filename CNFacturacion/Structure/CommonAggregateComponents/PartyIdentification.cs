using System;

using CNFacturacion.Structure.CommonBasicComponents;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class PartyIdentification
    {
        public PartyIdentificationId ID { get; set; }

        public PartyIdentification()
        {
            ID = new PartyIdentificationId();
        }
    }
}
