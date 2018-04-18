using System.Threading.Tasks;

using CNFacturacion.Common;
using CNFacturacion.CommonDTO.Exchange;

namespace CNFacturacion.Signed
{
    public interface ISerializador
    {
        Task<string> GenerarXml<T>(T objectToSerialize) where T : IEstructuraXML;
        Task<string> GenerarZip(string tramaXml, string nombreArchivo);
        Task<EnviarDocumentoResponse> GenerarDocumentoRespuesta(string constanciaRecepcion);
    }
}
