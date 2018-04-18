using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ResumenDiario
{
    public class ResumenDiario
    {
        public Emisor Emisor { get; set; }
        public Detalles Detalles { get; set; }

        public ResumenDiario()
        {
            Emisor = new Emisor();
            Detalles = new Detalles();
        }
    }
}
