using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComprobanteDePercepcion
{
    public class ComprobanteRelacionado
    {
        public string DocumentoTipo { get; set; }
        public string DocumentoNumero { get; set; }
        public string FechaEmision { get; set; }
        public Decimal ImporteTotal { get; set; }
        public string Moneda { get; set; }

        public string CobroFecha { get; set; }
        public Int32 CobroNumero { get; set; }
        public Decimal CobroImporteSinRetencion { get; set; }
        public string CobroMoneda { get; set; }

        public Decimal PercepcionImporte { get; set; }
        public string PercepcionMoneda { get; set; }
        public string PercepcionFecha { get; set; }

        public Decimal TotalNetoMonto { get; set; }
        public string TotalNetoMoneda { get; set; }

        public string TipoCambioMonedaReferencia { get; set; }
        public string TipoCambioMonedaObjetivo { get; set; }
        public Decimal TipoCambioFactorAplicado { get; set; }
        public string TipoCambioFecha { get; set; }
    }
}
