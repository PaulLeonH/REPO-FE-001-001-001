using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion.GuiaDeRemisionRemitente
{
    public class Detalles
    {
        public string ID { get; set; }
        public DateTime FechaEmision { get; set; }
        public string TipoDocumento { get; set; }
        public string Notas_Observaciones { get; set; }
        public GuiaDadaDeBaja GuiaDadaDeBaja { get; set; }
        public DocumentoRelacionado DocumentoRelacionado { get; set; }
        public DatosEnvio DatosEnvio { get; set; }
        public Transportista Transportista { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Conductor Conductor { get; set; }
        public PuntoPartida PuntoPartida { get; set; }
        public PuntoLlegada PuntoLlegada { get; set; }
        public string Puerto_AeropuertoCodigo { get; set; }
        public List<BienATransportar> ListaBienes { get; set; }

        public Detalles()
        {
            GuiaDadaDeBaja = new GuiaDadaDeBaja();
            DocumentoRelacionado = new DocumentoRelacionado();
            DatosEnvio = new DatosEnvio();
            Transportista = new Transportista();
            Vehiculo = new Vehiculo();
            Conductor = new Conductor();
            PuntoPartida = new PuntoPartida();
            PuntoLlegada = new PuntoLlegada();
            ListaBienes = new List<BienATransportar>();
        }
    }
}
