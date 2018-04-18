using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComunicacionDeBaja
{
    public class DocumentoDeBaja
    {
        public Int32 ID { get; set; }
        public string TipoDocumento { get; set; }
        public string SerieDocumento { get; set; }
        public Int32 NumeroDocumento { get; set; }
        public string Descripcion { get; set; }
    }
}
