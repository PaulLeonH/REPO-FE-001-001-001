using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComprobanteDePercepcion
{
    public class ComprobanteDePercepcion
    {
        public Emisor Emisor { get; set; }
        public Detalles Detalles { get; set; }
        public Cliente Cliente { get; set; }

        public ComprobanteDePercepcion()
        {
            Emisor = new Emisor();
            Detalles = new Detalles();
            Cliente = new Cliente();
        }
    }
}
