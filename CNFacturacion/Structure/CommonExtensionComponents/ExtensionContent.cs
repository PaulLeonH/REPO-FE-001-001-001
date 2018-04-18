using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNFacturacion.Structure.SunatAggregateComponents;
namespace CNFacturacion.Structure.CommonExtensionComponents
{
    [Serializable]
    public class ExtensionContent
    {
        public AdditionalInformation AdditionalInformation { get; set; }

        public ExtensionContent()
        {
            AdditionalInformation = new AdditionalInformation();
        }
    }
}
