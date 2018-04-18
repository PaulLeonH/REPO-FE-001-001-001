using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.CommonDTO.Exchange
{
    public class EnvioDocumentoComun
    {
        //[JsonProperty(Required = Required.Always)]
        public string Ruc { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public string UsuarioSol { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public string ClaveSol { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public string IdDocumento { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public string TipoDocumento { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public string EndPointUrl { get; set; }
    }
}
