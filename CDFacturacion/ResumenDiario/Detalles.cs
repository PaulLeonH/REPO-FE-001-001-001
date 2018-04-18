using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ResumenDiario
{
    public class Detalles
    {
        public string ID { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaEmisionDocumentos { get; set; }
        public string Moneda { get; set; }
        public List<ItemResumenDiario> ListaItems { get; set; }

        public Detalles()
        {
            ListaItems = new List<ItemResumenDiario>();
        }
    }
}
