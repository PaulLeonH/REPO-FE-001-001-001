using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonBasicComponents
{
    [Serializable]
    public class PayableAmount
    {
        public string CurrencyID { get; set; }

        public decimal Value { get; set; }
    }
}
