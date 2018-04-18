using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComunicacionDeBaja
{
    public class Detalles
    {
        public string ID { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public DateTime DocumentoDeBajaFechaGeneracion { get; set; }
        public List<DocumentoDeBaja> ListaDocumentos { get; set; }
        
        public Detalles()
        {
            ListaDocumentos = new List<DocumentoDeBaja>();
        }
    }
}
