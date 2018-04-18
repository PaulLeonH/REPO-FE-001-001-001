using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.Factura
{
    public class Detalles
    {
        public String DocumentoTipo { get; set; }
        public String DocumentoNumero { get; set; }
        public String Moneda { get; set; }
        public String FechaEmision { get; set; }
        public List<ItemFactura> ListaItems { get; set; }
        public MontosGlobales MontosGlobales { get; set; }
        public GuiaDeRemision GuiaDeRemision { get; set; }
        public DocumentoAsociado DocumentoAsociado { get; set; }
        public List<Leyenda> Leyendas { get; set; }
        public Detraccion Detraccion { get; set; }

        public Detalles()
        {
            ListaItems = new List<ItemFactura>();
            MontosGlobales = new MontosGlobales();
            GuiaDeRemision = new GuiaDeRemision();
            DocumentoAsociado = new DocumentoAsociado();
            Leyendas = new List<Leyenda>();
            Detraccion = new Detraccion();
        }
    }
}
