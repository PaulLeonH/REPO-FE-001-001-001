using CDFacturacion.Factura;
using CNFacturacion.GenXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using CNFacturacion.CommonDTO.Exchange;
using APIFacturacion.Util.GenerarPDF;
using APIFacturacion.Util.Validation.Factura;
using APIFacturacion.Util.Validation.Shared;

namespace APIFacturacion.Controllers
{
    public class FacturaController : ApiController
    {

        [HttpPost]
        [ActionName("EnviarDocumento")]
        public async Task<HttpResponseMessage> EnviarDocumento(Factura fac)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            
            try
            {
                if (CheckFactura.ValidateFactura(fac))
                {
                    EnviarDocumentoResponse respuesta = await XMLFactura.RutaXMLAsync(fac);

                    G_Factura g_Factura = new G_Factura(fac);
                    BlobUploadModel bum = new BlobUploadModel();

                    if (respuesta.Exito)
                    {
                        bum = await g_Factura.GenerarAsync();
                        bum.RequestResult = respuesta.MensajeRespuesta;
                        response = Request.CreateResponse(HttpStatusCode.OK, bum);
                    }
                    else
                    {
                        bum.RequestResult = respuesta.MensajeError;
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, bum);
                    }
                }
            }
            catch (CustomInvalidDataException e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, e.InnerException);
            }
            return response;
        }

    }
}
