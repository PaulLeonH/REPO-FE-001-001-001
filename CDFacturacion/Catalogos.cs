using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDFacturacion
{
    public class Catalogos
    {

        /// <summary>
        ///  Códigos de Tipo de Documento
        /// </summary>
        public class Catalogo01
        {
            public static readonly Dictionary<string, string> TipoDeDocumento = new Dictionary<string, string>() {
                { "01" , "Factura" },
                { "03" , "BoletaDeVenta" },
                { "06" , "CartaDePorteAereo" },
                { "07" , "NotaDeCredito" },
                { "08" , "NotaDeDebito" },
                { "09" , "GuiaDeRemisionRemitente" },
                { "12" , "TicketDeMaquinaRegistradora" },
                { "13" , "" },
                { "14" , "ReciboDeServiciosPublicos" },
                { "15" , "" },
                { "16" , "" },
                { "18" , "DocumentosEmitidosPorAFP" },
                { "20" , "ComprobanteDeRetencion" },
                { "21" , "ConocimientoDeEmbarque" },
                { "24" , "" },
                { "31" , "GuiaDeRemisionTransportista" },
                { "37" , "" },
                { "40" , "ComprobanteDePercepcion" },
                { "41" , "ComprobanteDePercepcion-VentaInterna" },
                { "43" , "" },
                { "45" , "" },
                { "56" , "ComprobanteDePagoSEAE" },
                { "71" , "GuiaDeRemisionRemitenteComplementaria" },
                { "72" , "GuiaDeRemisionTransportistaComplementaria" }
            };
        }


        /// <summary>
        ///  Códigos de Tipos de Moneda
        /// </summary>
        public class Catalogo02
        {
            public static readonly Dictionary<string, string> Monedas = new Dictionary<string, string>() {
                { "PEN" , "NuevoSol"},
                { "USD" , "DolarAmericano" }
            };
        }


        /// <summary>
        ///  Tipos de Unidad Comercial
        /// </summary>
        public class Catalogo03
        {
            public static readonly Dictionary<string, string> TiposDeUnidad = new Dictionary<string, string>() {
                { "NIU" , "Unidad" }
            };
        }


        /// <summary>
        ///  Códigos de Paises
        /// </summary>
        public class Catalogo04
        {
            public static readonly Dictionary<string, string> Paises = new Dictionary<string, string>() {
                { "PE" , "Peru" }
            };
        }


        /// <summary>
        ///  Tipos de Tributos
        /// </summary>
        public class Catalogo05
        {
            public static readonly Dictionary<string, string> Tributos = new Dictionary<string, string>() {
                { "1000" , "IGV" },
                { "1016" , "ArrozApilado" },
                { "2000" , "ISC" },
                { "9995" , "Exportacion" },
                { "9996" , "Gratuito" },
                { "9997" , "Exonerado" },
                { "9998" , "Inafecto" },
                { "9999" , "Otros" }
            };
        }


        /// <summary>
        ///  Tipos de Documento de Identidad
        /// </summary>
        public class Catalogo06
        {
            public static readonly Dictionary<string, string> DocumentosDeIdentidad = new Dictionary<string, string>() {
                { "0" , "DocTribNoDomSinRuc" },
                { "1" , "DNI" },
                { "4" , "CarnetDeExtranjeria" },
                { "6" , "RegUnicoDeContribuyentes" },
                { "7" , "Pasaborte" },
                { "A" , "CedDiplomaticaDeIdentidad" }
            };
        }


        /// <summary>
        ///  Códigos de Tipo de Afectación del IGV
        /// </summary>
        public class Catalogo07
        {
            public static readonly Dictionary<string, string> TiposDeAfectacion = new Dictionary<string, string>() {
                { "10" , "GravOperacionOnorosa" },
                { "11" , "GravRetiroPorPremio" },
                { "12" , "GravRetiroPorDonacion" },
                { "13" , "GravRetiro" },
                { "14" , "GravRetiroPorPublicidad" },
                { "15" , "GravBonificaciones" },
                { "16" , "GravRetiroPorEntregaATrabajadores" },
                { "17" , "GravIVAP" },
                { "20" , "ExonOperacionOnorosa" },
                { "21" , "ExonTransferenciaGratuita" },
                { "30" , "InafOperacionOnorosa" },
                { "31" , "InafRetiroPorBonificacion" },
                { "32" , "InafRetiro" },
                { "33" , "InafRetiroPorMuestrasMedicas" },
                { "34" , "InafRetiroPorConvenioColectivo" },
                { "35" , "InafRetiroPorPremio" },
                { "36" , "InafRetiroPorPublicidad" },
                { "40" , "Exportacion" }
            };
        }


        /// <summary>
        ///  Códigos de Tipos de Sistema de Cálculo del ISC
        /// </summary>
        public class Catalogo08
        {
            public static readonly Dictionary<string, string> TiposDeSistema = new Dictionary<string, string>() {
                { "01" , "SistemaAlValor" },
                { "02" , "AplicacionDelMontoFijo" },
                { "03" , "SistemaDePreciosDeVentaAlPublico" }
            };
        }


        /// <summary>
        ///  Códigos de Tipo de Nota de Crédito Electrónica
        /// </summary>
        public class Catalogo09
        {
            public static readonly Dictionary<string, string> TiposDeNota = new Dictionary<string, string>() {
                { "01" , "AnulacionDeLaOperacion" },
                { "02" , "AnulacionPorErrorEnRUC" },
                { "03" , "CorrecionPorErrorEnDescripcion" },
                { "04" , "DescuentoGlobal" },
                { "05" , "DescuentoPorItem" },
                { "06" , "DevolucionTotal" },
                { "07" , "DevolucionPorItem" },
                { "08" , "Bonificacion" },
                { "09" , "DisminucionEnElValor" },
                { "10" , "OtrosConceptos" }
            };
        }


        /// <summary>
        ///  Códigos de Tipo de Nota de Débito Electrónica
        /// </summary>
        public class Catalogo10
        {
            public static readonly Dictionary<string, string> TiposDeNota = new Dictionary<string, string>() {
                { "01" , "InteresPorMora" },
                { "02" , "AumentoEnElValor" },
                { "03" , "PenalidadesOtrosConceptos" }
            };
        }


        /// <summary>
        ///  Códigos de Tipo de Valor de Venta
        /// </summary>
        public class Catalogo11
        {
            public static readonly Dictionary<string, string> TiposDeValor = new Dictionary<string, string>() {
                { "01" , "Gravado" },
                { "02" , "Exonerado" },
                { "03" , "Inafecto" },
                { "04" , "Exportacion" },
                { "05" , "Gratuitas" },
            };
        }


        /// <summary>
        ///  Códigos - Documentos Relacionados Tributarios
        /// </summary>
        public class Catalogo12
        {
            public static readonly Dictionary<string, string> Documentos = new Dictionary<string, string>() {
                { "01" , "FacturaEmitidaPorCorregirErrorEnRUC" },
                { "02" , "FacturaEmitidaPorAnticipos" },
                { "03" , "BoletaDeVentaEmitidaPorAnticipos" },
                { "04" , "TicketDeSalidaENAPU" },
                { "05" , "CodigoSCOP" },
                { "09" , "Otros" }
            };
        }


        /// <summary>
        ///  Códigos - Otros Conceptos Tributarios
        /// </summary>
        public class Catalogo14
        {
            public static readonly Dictionary<string, string> Conceptos = new Dictionary<string, string>() {
                { "1000" , "TVVOperacionesExportadas" },
                { "1001" , "TVVOperacionesGravadas" },
                { "1002" , "TVVOperacionesInafectas" },
                { "1003" , "TVVOperacionesExoneradas" },
                { "1004" , "TVVOperacionesGratuitas" },
                { "1005" , "SubTotalVenta" },
                { "2001" , "Percepciones" },
                { "2002" , "Retenciones" },
                { "2003" , "Detracciones" },
                { "2004" , "Bonificaciones" },
                { "2005" , "TotalDescuentos" },
                { "3001" , "FISE" }
            };
        }


        /// <summary>
        ///  Códigos - Elementos Adicionales en la Factura Electrónica y/o Boleta de Venta Electrónica
        /// </summary>
        public class Catalogo15
        {
            public static readonly Dictionary<string, string> Elementos = new Dictionary<string, string>()
            {

            };
        }


        /// <summary>
        ///  Códigos - Tipo de Precio de Venta Unitario
        /// </summary>
        public class Catalogo16
        {
            public static readonly Dictionary<string, string> PreciosDeVenta = new Dictionary<string, string>() {
                { "01" , "PrecioUnitario" },
                { "02" , "ValorReferencialUnitarioEnOperacionesNoOnerosas" }
            };
        }


        /// <summary>
        ///  Códigos - Tipos de Operación
        /// </summary>
        public class Catalogo17
        {
            public static readonly Dictionary<string, string> TiposDeOperacion = new Dictionary<string, string>() {
                { "01" , "VentaInterna" },
                { "02" , "Exportacion" },
                { "03" , "NoDomiciliados" },
                { "04" , "VentaInternaAnticipos" },
                { "05" , "VentaItinerante" },
                { "06" , "FacturaGuia" },
                { "07" , "VentaArrozPilado" },
                { "08" , "FacturaComprobanteDePercepcion" },
                { "10" , "FacturaGuiaRemitente" },
                { "11" , "FacturaGuiaTransportista" },
                { "12" , "BoletaDeVentaComprobanteDePercepcion" },
                { "13" , "GastoDeduciblePersonaNatural" }
            };
        }


        /// <summary>
        ///  Códigos - Modalidad de Traslado
        /// </summary>
        public class Catalogo18
        {
            public static readonly Dictionary<string, string> ModalidadDeTraslado = new Dictionary<string, string>() {
                { "01" , "TransportePublico" },
                { "02" , "TransportePrivado" }
            };
        }


        /// <summary>
        ///  Códigos de Estado de Item - Resumen Diario de Boletas De Venta Y Notas Electrónicas
        /// </summary>
        public class Catalogo19
        {
            public static readonly Dictionary<string, string> CodigosDeEstado = new Dictionary<string, string>() {
                { "1" , "Adicionar" },
                { "2" , "Modificar" },
                { "3" , "Anulado" },
                { "4" , "AnuladoEnElDia" },
            };
        }


        /// <summary>
        ///  Códigos - Motivos de Traslado
        /// </summary>
        public class Catalogo20
        {
            public static readonly Dictionary<string, string> MotivosDeTraslado = new Dictionary<string, string>() {
                { "01" , "Venta" },
                { "02" , "Compra" },
                { "04" , "TrasladoEntreEstablecimientoDeMismaEmpresa" },
                { "08" , "Importacion" },
                { "09" , "Exportacion" },
                { "13" , "Otros" },
                { "14" , "VentaSujetaAConfirmacionDelComprador" },
                { "18" , "TrasladoEmisorItineranteCP" },
                { "19" , "TrasladoAZonaPrimaria" }
            };
        }


        /// <summary>
        ///  Documentos Relacionados - Aplicable Solo Para La Guia De Remisión Electrónica
        /// </summary>
        public class Catalogo21
        {
            public static readonly Dictionary<string, string> DocumentosRelacionados = new Dictionary<string, string>() {
                { "01" , "NumeracionDAM" },
                { "02" , "NumeroDeOrdenDeEntrega" },
                { "03" , "NumeroSCOP" },
                { "04" , "NumeroDeManifestoDeCarga" },
                { "05" , "NumeroDeConstanciaDeDetraccion" },
                { "06" , "Otros" }
            };
        }


        /// <summary>
        ///  Regímenes de Percepción
        /// </summary>
        public class Catalogo22
        {
            public static readonly Dictionary<string, string> Regimenes = new Dictionary<string, string>() {
                { "01" , "PercepcionVentaInterna" },
                { "02" , "PercepcionAdquisicionDeCombustible" },
                { "03" , "PercepcionRealizadaAlAgenteTasaEspecial" }
            };
        }


        /// <summary>
        ///  Regímenes de Retención
        /// </summary>
        public class Catalogo23
        {
            public static readonly Dictionary<string, string> Regimenes = new Dictionary<string, string>() {
                { "01" , "Tasa3" }
            };
        }


        /// <summary>
        ///  Recibo Electrónico por Servicios Públicos
        /// </summary>
        public class Catalogo24
        {
            public static readonly Dictionary<string, string> Recibos = new Dictionary<string, string>() {
                { "L001" , "AT" },
                { "L002" , "MT2" },
                { "L003" , "MT3" },
                { "L004" , "MT4" },
                { "L005" , "BT2" },
                { "L006" , "BT3" },
                { "L009" , "BT5BNoResidencial" },
                { "L010" , "BT6" },
                { "A011" , "Comercial" },
                { "A012" , "Industrial" },
                { "A014" , "Domestico" },
                { "A015" , "Social" }
            };
        }
    }
}
