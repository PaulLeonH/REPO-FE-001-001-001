using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComprobanteDeRetencion
{
    public class Proveedor
    {
        public string DocumentoTipo { get; set; }
        public string DocumentoNumero { get; set; }
        public string Nom_RazonSoc { get; set; }
        public string NombreComercial { get; set; }
        public DomicilioFiscal DomicilioFiscal { get; set; }

        public Proveedor()
        {
            DomicilioFiscal = new DomicilioFiscal();
        }
    }
}
