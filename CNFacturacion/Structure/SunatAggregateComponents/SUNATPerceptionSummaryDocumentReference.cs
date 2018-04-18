using CNFacturacion.Structure.CommonBasicComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.SunatAggregateComponents
{
    public class SUNATPerceptionSummaryDocumentReference
    {
        public string SUNATPerceptionSystemCode { get; set; }
        public string SUNATPerceptionPercent { get; set; }
        public PayableAmount TotalInvoiceAmount { get; set; }
        public PayableAmount SUNATTotalCashed { get; set; }
        public PayableAmount TaxableAmount { get; set; }

        public SUNATPerceptionSummaryDocumentReference()
        {
            TotalInvoiceAmount = new PayableAmount();
            SUNATTotalCashed = new PayableAmount();
            TaxableAmount = new PayableAmount();
        }

    }
}
