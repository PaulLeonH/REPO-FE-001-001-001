using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.GuiaDeRemisionRemitente
{
    public class GuiaDeRemisionRemitente
    {
        public Emisor Emisor { get; set; }
        public Destinatario Destinatario { get; set; }
        public Proveedor Proveedor { get; set; }
        public Detalles Detalles { get; set; }

        public GuiaDeRemisionRemitente()
        {
            Detalles = new Detalles();
            Emisor = new Emisor();
            Destinatario = new Destinatario();
            Proveedor = new Proveedor();
        }
    }
}
