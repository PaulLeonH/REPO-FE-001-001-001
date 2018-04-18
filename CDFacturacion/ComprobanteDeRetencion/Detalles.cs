using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComprobanteDeRetencion
{
    public class Detalles
    {
        public string ID { get; set; }
        public string FechaEmision { get; set; }
        public string RetencionRegimen { get; set; }
        public Decimal RetencionTasa { get; set; }
        public string Notas_Observaciones { get; set; }
        public Decimal ImporteTotalRetenido { get; set; }
        public string ImporteTotalRetenidoMoneda { get; set; }
        public Decimal ImporteTotalPagado { get; set; }
        public string ImporteTotalPagadoMoneda { get; set; }
        public ComprobanteRelacionado ComprobanteRelacionado { get; set; }

        public Detalles()
        {
            ComprobanteRelacionado = new ComprobanteRelacionado();
        }
    }
}
