using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.Factura
{
    public class Cliente
    {
        public string DocumentoTipo { get; set; }
        public string DocumentoNumero { get; set; }
        public string NomRazonSoc { get; set; }
        public DomicilioFiscal DomicilioFiscal { get; set; }

        public Cliente()
        {
            DomicilioFiscal = new DomicilioFiscal();
        }
    }
}
