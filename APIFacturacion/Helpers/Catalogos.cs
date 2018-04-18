using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFacturacion.Helpers
{
    public class Catalogos
    {
        public static readonly Dictionary<string, string> Catalogo01 = new Dictionary<string, string>() {
            { "01" , "Factura" },
            { "03" , "Boleta de Venta" },
            { "07" , "Nota de Crédito" },
            { "08" , "Nota de Debito" },
            { "09" , "Guía de Remisión remitente" },
            { "12" , "Ticket de máquina registradora" },
            { "13" , "Documento emitido por bancos, instituciones financieras, crediticias y de seguros" +
                        " que se encuentren bajo el control de la Superintendencia de Banca y Seguros" },
            { "18" , "Documentos emitidos por las AFP" },
            { "31" , "Guía de Remisión Transportista" },
        };
        
        public static readonly Dictionary<string, string> Catalogo02 = new Dictionary<string, string>() {
            { "PEN" , "Nuevo Sol"},
            { "USD" , "Dolar Américano" }
        };
        
        public static readonly Dictionary<string, string> Catalogo03 = new Dictionary<string, string>() {
            { "GRM" , "Gramo" },
            { "KGM" , "Kilogramo" },
            { "LTR" , "Litro" },
            { "NIU" , "Unidad" }
        };

        public static readonly Dictionary<string, string> Catalogo06 = new Dictionary<string, string>() {
            { "0" , "Doc. Trib. No. Dom. Sin. Ruc" },
            { "1" , "DNI" },
            { "4" , "Carnet de Extranjeria" },
            { "6" , "RUC" },
            { "7" , "Pasaporte" },
            { "A" , "Ced. Diplomatica de Identidad" }
        };

        public static readonly Dictionary<string, string> Catalogo07 = new Dictionary<string, string>() {
            { "10" , "Gravado - Operación onorosa" },
            { "11" , "Gravado - Retiro por premio" },
            { "12" , "Gravado - Retiro por donación" },
            { "13" , "Gravado - Retiro" },
            { "14" , "Gravado - Retiro por publicidad" },
            { "15" , "Gravado - Bonificaciones" },
            { "16" , "Gravado - Retiro por entrega a trabajadores" },
            { "20" , "Exonerado - Operación onorosa" },
            { "30" , "Inafecto - Operación onorosa" },
            { "31" , "Inafecto - Retiro por bonificación" },
            { "32" , "Inafecto - Retiro" },
            { "33" , "Inafecto - Retiro por muestras médicas" },
            { "34" , "Inafecto - Retiro por convenio colectivo" },
            { "35" , "Inafecto - Retiro por premio" },
            { "36" , "Inafecto - Retiro por publicidad" },
            { "40" , "Exportación" }
        };

        public static readonly Dictionary<string, string> Catalogo08 = new Dictionary<string, string>() {
            { "01" , "Sistema al valor" },
            { "02" , "Aplicación del monto fijo" },
            { "03" , "Sistema de precios de venta al público" }
        };

        public static readonly Dictionary<string, string> Catalogo16 = new Dictionary<string, string>() {
            { "01" , "Precio unitario" },
            { "02" , "Valor referencial unitario en operaciones no onerosas" }
        };
    }
}