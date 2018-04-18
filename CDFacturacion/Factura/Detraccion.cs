using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.Factura
{
    public class Detraccion
    {
        public Decimal Porcentaje { get; set; }
        public Decimal Monto { get; set; }
        public Decimal ValorReferencial { get; set; }
        public List<DetalleDetraccion> Detracciones { get; set; }

        public Detraccion()
        {
            Detracciones = new List<DetalleDetraccion>();
        }

    }
}
