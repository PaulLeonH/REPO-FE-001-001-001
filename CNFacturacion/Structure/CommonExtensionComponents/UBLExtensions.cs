using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Structure.CommonExtensionComponents
{
    [Serializable]
    public class UBLExtensions
    {
        public UBLExtension Extension1 { get; set; }

        public UBLExtension Extension2 { get; set; }

        public UBLExtensions()
        {
            Extension1 = new UBLExtension();
            Extension2 = new UBLExtension();
        }
    }
}
