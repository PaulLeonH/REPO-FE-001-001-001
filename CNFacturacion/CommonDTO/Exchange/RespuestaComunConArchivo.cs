using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.CommonDTO.Exchange
{
    public abstract class RespuestaComunConArchivo : RespuestaComun
    {
        public string NombreArchivo { get; set; }
    }
}
