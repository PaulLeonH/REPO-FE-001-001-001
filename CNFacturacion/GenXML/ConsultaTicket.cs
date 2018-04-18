using CNFacturacion.CommonDTO.Exchange;
using CNFacturacion.Services;
using CNFacturacion.Signed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.GenXML
{
    public class ConsultaTicket
    {
        private static readonly ISerializador _serializador = new Serializador();
        private static readonly IServicioSunatDocumentos _servicioDocumentoSunat = new ServicioSunatDocumentos();

        public static async Task<EnviarDocumentoResponse> ConsultaTicketAsync(ConsultaTicketRequest request)
        {
            var response = new EnviarDocumentoResponse();

            try
            {
                _servicioDocumentoSunat.Inicializar(new ParametrosConexion {
                    Ruc = request.Ruc,
                    UserName = request.UsuarioSol,
                    Password = request.ClaveSol,
                    EndPointUrl = request.EndPointUrl
                });

                var resultado = _servicioDocumentoSunat.ConsultarTicket(request.NroTicket);

                if (!resultado.Exito)
                {
                    response.Exito = false;
                    response.MensajeError = resultado.MensajeError;
                }
                else
                    response = await _serializador.GenerarDocumentoRespuesta(resultado.ConstanciaDeRecepcion);
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Source == "DotNetZip" ? "El Ticket no existe" : ex.Message;
                response.Pila = ex.StackTrace;
                response.Exito = false;
            }

            return response;

        }
    }
}
