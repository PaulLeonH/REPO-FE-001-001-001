using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.NotaDeDebito
{
    public class ItemNotaDebito
    {
        public Int16 ID { get; set; }
        public string CodigoProducto { get; set; }
        public string UnidadTipo { get; set; }
        public Int64 UnidadCantidad { get; set; }
        public string DescripcionDetallada { get; set; }
        public Decimal ValorUnitario { get; set; }
        public Decimal PrecioVenta { get; set; }
        public string AfectacionIGV { get; set; }
        public Decimal ItemMontoIGV { get; set; }
        public string SistemaISC { get; set; }
        public Decimal ItemMontoISC { get; set; }
        public Decimal ItemValorVenta { get; set; }
    }
}
