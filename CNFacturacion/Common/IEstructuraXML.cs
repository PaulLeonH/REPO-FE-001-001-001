using System;

namespace CNFacturacion.Common
{
    public interface IEstructuraXML
    {
        string UBLVersionID { get; set; }
        string CustomizationID { get; set; }
        string ID { get; set; }
        IFormatProvider Formato { get; set; }
    }
}
