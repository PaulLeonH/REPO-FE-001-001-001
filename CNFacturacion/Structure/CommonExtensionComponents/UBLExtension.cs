using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonExtensionComponents
{
    [Serializable]
    public class UBLExtension
    {
        public ExtensionContent ExtensionContent { get; set; }

        public UBLExtension()
        {
            ExtensionContent = new ExtensionContent();
        }
    }
}
