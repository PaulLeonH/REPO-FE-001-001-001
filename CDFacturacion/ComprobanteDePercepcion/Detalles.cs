using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComprobanteDePercepcion
{
    public class Detalles
    {
        public string ID { get; set; }
        public string FechaEmision { get; set; }
        public string PercepcionRegimen { get; set; }
        public Decimal PercepcionTasa { get; set; }
        public string Notas_Observaciones { get; set; }
        public Decimal ImporteTotalPercibido { get; set; }
        public string ImporteTotalPercibidoMoneda { get; set; }
        public Decimal ImporteTotalCobrado { get; set; }
        public string ImporteTotalCobradoMoneda { get; set; }
        public ComprobanteRelacionado ComprobanteRelacionado { get; set; }

        public Detalles()
        {
            ComprobanteRelacionado = new ComprobanteRelacionado();
        }
    }
}
