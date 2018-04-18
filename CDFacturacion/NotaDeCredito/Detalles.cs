using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.NotaDeCredito
{
    public class Detalles
    {
        public string ID { get; set; }
        public DateTime FechaEmision { get; set; }
        public string NotaElectronicaDocumentoModificadoTipo { get; set; }
        public string NotaElectronicaDocumentoModificadoNumero { get; set; }
        public string NotaElectronicaCodigoTipo { get; set; }
        public string NotaElectronicaDescripcion { get; set; }
        public string Moneda { get; set; }
        public List<ItemNotaCredito> ListaItems { get; set; }
        public MontosGlobales MontosGlobales { get; set; }
        public GuiaDeRemision GuiaDeRemision { get; set; }
        public DocumentoAsociado DocumentoAsociado { get; set; }

        public Detalles()
        {
            ListaItems = new List<ItemNotaCredito>();
            MontosGlobales = new MontosGlobales();
            GuiaDeRemision = new GuiaDeRemision();
            DocumentoAsociado = new DocumentoAsociado();
        }

    }
}
