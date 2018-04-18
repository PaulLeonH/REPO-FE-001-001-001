using CDFacturacion.ComprobanteDeRetencion;
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
    public class ComprobanteRetencionController : ApiController
    {
        [HttpPost]
        [ActionName("EnviarResumen")]
        public async Task<HttpResponseMessage> EnviarResumen(ComprobanteDeRetencion comprobReten)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                EnviarResumenResponse respuesta = await XMLComprobanteDeRetencion.RutaXMLAsync(comprobReten);
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
