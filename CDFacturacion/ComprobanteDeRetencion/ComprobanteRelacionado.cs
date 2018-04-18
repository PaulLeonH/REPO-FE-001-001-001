using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ComprobanteDeRetencion
{
    public class ComprobanteRelacionado
    {
        public string DocumentoTipo { get; set; }
        public string DocumentoNumero { get; set; }
        public string FechaEmision { get; set; }
        public Decimal ImporteTotal { get; set; }
        public string Moneda { get; set; }
        
        public string PagoFecha { get; set; }
        public Int32 PagoNumero { get; set; }
        public Decimal PagoImporteSinRetencion { get; set; }
        public string PagoMoneda { get; set; }

        public Decimal RetencionImporte { get; set; }
        public string RetencionMoneda { get; set; }
        public string RetencionFecha { get; set; }

        public Decimal TotalNetoMonto { get; set; }
        public string TotalNetoMoneda { get; set; }

        public string TipoCambioMonedaReferencia { get; set; }
        public string TipoCambioMonedaObjetivo { get; set; }
        public Decimal TipoCambioFactorAplicado { get; set; }
        public string TipoCambioFecha { get; set; }
    }
}
