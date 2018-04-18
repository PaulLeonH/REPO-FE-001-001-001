using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Services
{
    public interface IServicioSunatDocumentos : IServicioSunat
    {
        RespuestaSincrono EnviarDocumento(DocumentoSunat request);
        RespuestaAsincrono EnviarResumen(DocumentoSunat request);
        RespuestaSincrono ConsultarTicket(string numeroTicket);
    }
}
