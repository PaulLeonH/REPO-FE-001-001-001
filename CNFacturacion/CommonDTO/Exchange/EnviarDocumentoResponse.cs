namespace CNFacturacion.CommonDTO.Exchange
{
    public class EnviarDocumentoResponse : RespuestaComunConArchivo
    {
        public string CodigoRespuesta { get; set; }

        public string MensajeRespuesta { get; set; }

        public string TramaZipCdr { get; set; }
    }
}
