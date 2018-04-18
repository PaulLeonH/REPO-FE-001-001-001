using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComprobanteDeRetencion
{
    public class ComprobanteDeRetencion
    {
        public Emisor Emisor { get; set; }
        public Detalles Detalles { get; set; }
        public Proveedor Proveedor { get; set; }

        public ComprobanteDeRetencion()
        {
            Emisor = new Emisor();
            Detalles = new Detalles();
            Proveedor = new Proveedor();
        }
    }
}
