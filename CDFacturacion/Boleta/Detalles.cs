using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.Boleta
{
    public class Detalles
    {
        public DateTime FechaEmision { get; set; }
        public string DocumentoTipo { get; set; }
        public string DocumentoNumero { get; set; }
        public string Moneda { get; set; }
        public List<ItemBoleta> ListaItems { get; set; }
        public DocumentoAsociado DocumentoAsociado { get; set; }
        public GuiaDeRemision GuiaDeRemision { get; set; }
        public MontosGlobales MontosGlobales { get; set; }
        public List<Leyenda> Leyendas { get; set; }

        public Detalles()
        {
            Leyendas = new List<Leyenda>();
            ListaItems = new List<ItemBoleta>();
            MontosGlobales = new MontosGlobales();
            GuiaDeRemision = new GuiaDeRemision();
            DocumentoAsociado = new DocumentoAsociado();
        }
    }
}
