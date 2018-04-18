using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComunicacionDeBaja
{
    public class ComunicacionDeBaja
    {
        public Emisor Emisor { get; set; }
        public Detalles Detalles { get; set; }

        public ComunicacionDeBaja()
        {
            Emisor = new Emisor();
            Detalles = new Detalles();
        }
    }
}
