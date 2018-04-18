using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.Factura
{
    public class Emisor
    {
        public string FirmaDigital { get; set; }

        private string _documentoTipo;
        public string DocumentoTipo
        {
            get { return _documentoTipo?.Trim().ToUpper(); }
            set { _documentoTipo = value; }
        }

        private string _documentoNumero;
        public string DocumentoNumero
        {
            get { return _documentoNumero?.Trim(); }
            set { _documentoNumero = value; }
        }

        private string _nomRazonSocial;
        public string NomRazonSoc
        {
            get { return _nomRazonSocial?.Trim(); }
            set { _nomRazonSocial = value; }
        }

        private string _nombreComercial;
        public string NombreComercial
        {
            get { return _nombreComercial?.Trim(); }
            set { _nombreComercial = value; }
        }

        private string _numCelular;
        public string NumCelular
        {
            get { return _numCelular?.Trim(); }
            set { _numCelular = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email?.Trim(); }
            set { _email = value; }
        }
        
        public DomicilioFiscal DomicilioFiscal { get; set; }

        public Emisor()
        {
            DomicilioFiscal = new DomicilioFiscal();
        }
    }
}
