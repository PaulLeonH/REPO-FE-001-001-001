using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFacturacion.Helpers
{
    public class ConstantData
    {

        public class Validation
        {
            public class Messages
            {
                public const string AllowedCatalogValues = "El campo '{0}' debe tener un valor admitido en el {1}";

                public const string ExactLength = "La longitud del campo '{0}' debe ser de {1} dígito(s).";
                
                public const string InvalidData = "Verificar el valor ingresado en el campo '{0}'.";

                public const string InvalidDate = "Verificar la fecha en el campo '{0}', el formato aceptado es 'dd/mm/yyyy o dd-mm-yyyy'";

                public const string MaxLength = "La longitud del campo '{0}' tiene como máximo {1} dígito(s).";

                public const string Required = "El campo '{0}' es requerido.";
            }

            public class RegEx
            {
                public const String CantidadUnidades = @"^[0-9]{1,12}\.[0-9]{1,3}$";
                public const String Celular = @"^9[0-9]{8}$";
                public const String Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+[\.[a-zA-Z]{2,4}]+$";
                public const String Factura = @"^F[0-9]{3}-[0-9]{8}$";
                public const String RUC = @"^[0-9]{11}$";
                public const String DecimalValues = @"^[0-9]{1,12}\.[0-9]{1,2}$";
            }

            public class ExactLength
            {
                public const Int16 AfectacionIGV = 2;
                public const Int16 Celular = 9;
                public const Int16 Moneda = 3;
                public const Int16 RUC = 11;
                public const Int16 SistemaISC = 2;
                public const Int16 TipoDocumentoIdentidad = 1;
                public const Int16 TipoDocumentoTributario = 2;
            }

            public class MaxLength
            {
                public const Int16 CantidadUnidades = 16;
                public const Int16 CodigoItem = 30;
                public const Int16 DescripcionItem = 250;
                public const Int16 Factura = 13;
                public const Int16 FirmaDigital = 3000;
                public const Int16 NombreComercial = 100;
                public const Int16 RazonSocial = 100;
                public const Int16 UnidadMedida = 3;
                public const Int16 DecimalValues = 15;
            }
        }

        public class Catalogos
        {
            public class Catalogo01
            {
                public const String Factura = "01";
                public const String Boleta = "03";
                public const String NotaCredito = "07";
                public const String NotaDebito = "08";
                public const String GuiaRemisionRemitente = "09";
                public const String TicketMaquinaRegistradora = "12";
                public const String DocEmitidoBancoYSegurosSBS = "13";
                public const String DocEmitidosAFP = "18";
                public const String GuiaRemisionTransportista = "31";
            }

            public class Catalogo06
            {
                public const string DocTribNoDomSinRuc = "0";
                public const string DNI = "1";
                public const string CarnetDeExtranjeria = "4";
                public const string RUC = "6";
                public const string Pasaporte = "7";
                public const string CedDiplomaticaDeIdentidad = "A";
            }
        }
    }
}