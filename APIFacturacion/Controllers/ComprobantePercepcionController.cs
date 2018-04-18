using CDFacturacion.ComprobanteDePercepcion;
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
    public class ComprobantePercepcionController : ApiController
    {
        [HttpPost]
        [ActionName("EnviarResumen")]
        public async Task<HttpResponseMessage> EnviarResumen(ComprobanteDePercepcion comprobPercep)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                EnviarResumenResponse respuesta = await XMLComprobanteDePercepcion.RutaXMLAsync(comprobPercep);
                if (respuesta.Exito)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, respuesta.NroTicket);
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
