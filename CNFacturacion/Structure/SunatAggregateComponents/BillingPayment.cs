﻿using System;
using CNFacturacion.Structure.CommonBasicComponents;

namespace CNFacturacion.Structure.SunatAggregateComponents
{
    [Serializable]
    public class BillingPayment
    {
        public PartyIdentificationId Id { get; set; }

        public PayableAmount PaidAmount { get; set; }

        public string InstructionId { get; set; }

        public BillingPayment()
        {
            PaidAmount = new PayableAmount();
            Id = new PartyIdentificationId();
        }
    }
}
