using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.CommonDTO.Exchange
{
    public class FirmadoRequest
    {
        //[JsonProperty(Required = Required.Always)]
        public string CertificadoDigital { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public string PasswordCertificado { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public string TramaXmlSinFirma { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public bool UnSoloNodoExtension { get; set; }
    }
}
