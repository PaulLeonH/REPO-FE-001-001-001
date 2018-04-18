using CNFacturacion.CommonDTO.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFacturacion.Signed
{
    public interface ICertificador
    {
        Task<FirmadoResponse> FirmarXml(FirmadoRequest request);
    }
}
