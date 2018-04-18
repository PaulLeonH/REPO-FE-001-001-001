using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.CommonDTO.Exchange
{
    public class EnviarDocumentoRequest : EnvioDocumentoComun
    {
        //[JsonProperty(Required = Required.Always)]
        public string TramaXmlFirmado { get; set; }
    }
}
