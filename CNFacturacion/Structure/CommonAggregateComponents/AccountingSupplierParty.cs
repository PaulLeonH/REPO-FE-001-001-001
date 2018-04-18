using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonAggregateComponents
{
    [Serializable]
    public class AccountingSupplierParty
    {
        public string CustomerAssignedAccountId { get; set; }

        public string AdditionalAccountId { get; set; }

        public Party Party { get; set; }
    }
}
