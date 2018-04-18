using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.Factura
{
    public class Factura
    {

        public Emisor Emisor { get; set; }
        public Cliente Cliente { get; set; }
        public Detalles Detalles { get; set; }

        public Factura()
        {
            Emisor = new Emisor();
            Cliente = new Cliente();
            Detalles = new Detalles();
        }
    }
}
