using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.ResumenDiario
{
    public class ItemResumenDiario
    {
        public Int32 LineID { get; set; }
        public string ID { get; set; }
        public string TipoDocumento { get; set; }
        public string AdquirienteTipoDocumento { get; set; }
        public string AdquirienteNumeroDocumento { get; set; }
        public string DocumentoAdjuntadoSerie { get; set; }
        public string DocumentoAdjuntadoTipo { get; set; }
        public string PercepcionRegimen { get; set; }
        public string PercepcionTasa { get; set; }
        public Decimal PercepcionBaseImponible { get; set; }
        public Decimal PercepcionMonto { get; set; }
        public Decimal PercepcionMontoIncluidoPer { get; set; }
        public Int32 ItemEstado { get; set; }
        public Decimal ItemImporteTotal { get; set; }
        public Decimal TVVOperacionesGravadas { get; set; }
        public Decimal TVVOperacionesInafectas { get; set; }
        public Decimal TVVOperacionesExoneradas { get; set; }
        public Decimal TVVOperacionesGratuitas { get; set; }
        public Decimal MontoIGV { get; set; }
        public Decimal MontoISC { get; set; }
        public Decimal SumatoriaOtrosTributos { get; set; }
        public bool SumatorioOtrosCargosEsDescuento { get; set; }
        public Decimal SumatorioOtrosCargos { get; set; }
    }
}
