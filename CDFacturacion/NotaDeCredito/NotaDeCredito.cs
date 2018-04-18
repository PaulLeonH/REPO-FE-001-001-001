using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.NotaDeCredito
{
    public class NotaDeCredito
    {
        public Emisor Emisor { get; set; }
        public Cliente Cliente { get; set; }
        public Detalles Detalles { get; set; }

        public NotaDeCredito()
        {
            Emisor = new Emisor();
            Cliente = new Cliente();
            Detalles = new Detalles();
        }
    }
}
