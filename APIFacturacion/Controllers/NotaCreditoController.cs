using CDFacturacion.NotaDeCredito;
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
    public class NotaCreditoController : ApiController
    {
        [HttpPost]
        [ActionName("EnviarDocumento")]
        public async Task<HttpResponseMessage> EnviarDocumento(NotaDeCredito notaCred)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                EnviarDocumentoResponse respuesta = await XMLNotaDeCredito.RutaXMLAsync(notaCred);
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
