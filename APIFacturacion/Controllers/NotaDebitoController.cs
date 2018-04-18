using CDFacturacion.NotaDeDebito;
using CNFacturacion.CommonDTO.Exchange;
using CNFacturacion.GenXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIFacturacion.Controllers
{
    public class NotaDebitoController : ApiController
    {
        [HttpPost]
        [ActionName("EnviarDocumento")]
        public async Task<HttpResponseMessage> EnviarDocumento(NotaDeDebito notaDeb)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                EnviarDocumentoResponse respuesta = await XMLNotaDeDebito.RutaXMLAsync(notaDeb);
                if (respuesta.Exito)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, respuesta.MensajeRespuesta);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotAcceptable, respuesta.MensajeError);
                }
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, e.InnerException);
            }
            return response;
        }
    }
}
