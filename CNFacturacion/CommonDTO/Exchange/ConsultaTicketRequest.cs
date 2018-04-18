namespace CNFacturacion.CommonDTO.Exchange
{
    public class ConsultaTicketRequest : EnvioDocumentoComun
    {
        //[JsonProperty(Required = Required.Always)]
        public string NroTicket { get; set; }
    }
}