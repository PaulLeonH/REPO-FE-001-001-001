using APIFacturacion.Helpers;
using System;
using System.Text.RegularExpressions;

namespace APIFacturacion.Util.Validation.Shared
{
    public class FieldsValidators
    {
        public static bool ValidateFirmaDigital(String Trigger, String FirmaDigital, out String ResponseMessage)
        {
            String Field = $"Firma Digital ({Trigger})";
            ResponseMessage = "";

            if (String.IsNullOrEmpty(FirmaDigital))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (FirmaDigital.Length > ConstantData.Validation.MaxLength.FirmaDigital)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.MaxLength, Field, ConstantData.Validation.MaxLength.FirmaDigital);
                return false;
            }

            return true;
        }
        
        public static bool ValidateTipoDocumentoTributario(String Trigger, String TipoDocumento, out String ResponseMessage)
        {
            String Field = $"Tipo de Documento Tributario ({Trigger})";
            ResponseMessage = "";
            if (String.IsNullOrEmpty(TipoDocumento))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }
            else
            {
                // La longitud del Tipo de Documento debe ser 2.
                if (TipoDocumento.Length != ConstantData.Validation.ExactLength.TipoDocumentoTributario)
                {
                    ResponseMessage = String.Format(ConstantData.Validation.Messages.ExactLength, Field, ConstantData.Validation.ExactLength.TipoDocumentoTributario);
                    return false;
                }

                // El Tipo de Documento ingresado debe pertenecer al Catálogo-01 (Código de tipo de documento autorizado para efectos tributarios) de la SUNAT.
                if (!Catalogos.Catalogo01.ContainsKey(TipoDocumento))
                {
                    ResponseMessage = String.Format(ConstantData.Validation.Messages.AllowedCatalogValues, Field, "Catálogo 01");
                    return false;
                }

                return true;
            }
        }

        public static bool ValidateNumeroDocumentoTributario(String Trigger, String TipoDocumento, String NumeroDocumento, out String ResponseMessage)
        {
            String Field = $"Número de Documento Tributario ({Trigger})";
            ResponseMessage = "";
            if (String.IsNullOrEmpty(NumeroDocumento))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }
            else
            {
                switch (TipoDocumento)
                {
                    case ConstantData.Catalogos.Catalogo01.Factura:
                        {
                            if (NumeroDocumento.Length > ConstantData.Validation.MaxLength.Factura)
                            {
                                ResponseMessage = String.Format(ConstantData.Validation.Messages.ExactLength, Field, ConstantData.Validation.MaxLength.Factura);
                                return false;
                            }

                            Match match = Regex.Match(NumeroDocumento, ConstantData.Validation.RegEx.Factura);
                            if (!match.Success)
                            {
                                ResponseMessage = String.Format(ConstantData.Validation.Messages.InvalidData, Field);
                                return false;
                            }

                        }; break;
                    default:
                        {
                            ResponseMessage = "Datos alterados en el proceso.";
                            return false;
                        }
                }

            }
            return true;
        }

        public static bool ValidateTipoDocumentoIdentidad(String Trigger, String TipoDocumento, out String ResponseMessage)
        {
            String Field = $"Tipo de Documento de Identidad ({Trigger})";
            ResponseMessage = "";

            if (String.IsNullOrEmpty(TipoDocumento))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }
            else
            {
                // La longitud del Tipo de Documento debe ser 1.
                if (TipoDocumento.Length != ConstantData.Validation.ExactLength.TipoDocumentoIdentidad)
                {
                    ResponseMessage = String.Format(ConstantData.Validation.Messages.ExactLength, Field, ConstantData.Validation.ExactLength.TipoDocumentoIdentidad);
                    return false;
                }

                // El Tipo de Documento ingresado debe pertenecer al Catálogo-06 (Códigos de Tipos de Documentos de Identidad) de la SUNAT.
                if (!Catalogos.Catalogo06.ContainsKey(TipoDocumento))
                {
                    ResponseMessage = String.Format(ConstantData.Validation.Messages.AllowedCatalogValues, Field, "Catálogo 06");
                    return false;
                }

                return true;
            }
        }

        public static bool ValidateNumeroDocumentoIdentidad(String Trigger, String TipoDocumento, String NumeroDocumento, out String ResponseMessage)
        {
            String Field = $"Número de Documento de Identidad ({Trigger})";
            ResponseMessage = "";

            if (String.IsNullOrEmpty(NumeroDocumento))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }
            else
            {
                switch (TipoDocumento)
                {
                    case ConstantData.Catalogos.Catalogo06.RUC:
                        {
                            if (NumeroDocumento.Length != ConstantData.Validation.ExactLength.RUC)
                            {
                                ResponseMessage = String.Format(ConstantData.Validation.Messages.ExactLength, Field, ConstantData.Validation.ExactLength.RUC);
                                return false;
                            }

                            Match match = Regex.Match(NumeroDocumento, ConstantData.Validation.RegEx.RUC);
                            if (!match.Success)
                            {
                                ResponseMessage = String.Format(ConstantData.Validation.Messages.InvalidData, Field);
                                return false;
                            }

                        }; break;
                    default:
                        {
                            ResponseMessage = "Datos alterados en el proceso.";
                            return false;
                        }
                }

            }

            return true;
        }

        public static bool ValidateRazonSocial(String Trigger, String RazonSocial, out String ResponseMessage)
        {
            String Field = $"Razón Social ({Trigger})";
            ResponseMessage = "";
            
            if (String.IsNullOrEmpty(RazonSocial))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }
                
            if (RazonSocial.Length > ConstantData.Validation.MaxLength.RazonSocial)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.MaxLength, Field, ConstantData.Validation.MaxLength.RazonSocial);
                return false;
            }

            return true;
        }
        
        public static bool ValidateNombreComercial(String Trigger, String NombreComercial, out String ResponseMessage)
        {
            String Field = $"Razón Social ({Trigger})";
            ResponseMessage = "";
            
            if (NombreComercial.Length > ConstantData.Validation.MaxLength.NombreComercial)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.MaxLength, Field, ConstantData.Validation.MaxLength.NombreComercial);
                return false;
            }
            
            return true;
        }

        public static bool ValidateNumeroCelular(String Trigger, String NumeroCelular, out String ResponseMessage)
        {
            String Field = $"Número Celular ({Trigger})";
            ResponseMessage = "";
            
            if (NumeroCelular.Length != ConstantData.Validation.ExactLength.Celular)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.ExactLength, Field, ConstantData.Validation.ExactLength.Celular);
                return false;
            }

            Match match = Regex.Match(NumeroCelular, ConstantData.Validation.RegEx.Celular);
            if (!match.Success)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.InvalidData, Field);
                return false;
            }

            return true;
        }

        public static bool ValidateEmail(String Trigger, String Email, out String ResponseMessage)
        {
            String Field = $"Email ({Trigger})";
            ResponseMessage = "";
            
            Match match = Regex.Match(Email, ConstantData.Validation.RegEx.Email);
            if (!match.Success)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.InvalidData, Field);
                return false;
            }
            
            return true;
        }

        public static bool ValidateMoneda(String Trigger, String Moneda, out String ResponseMessage)
        {
            String Field = $"Moneda({Trigger})";
            ResponseMessage = "";
            if (String.IsNullOrEmpty(Moneda))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (Moneda.Length != ConstantData.Validation.ExactLength.Moneda)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.ExactLength, Field, ConstantData.Validation.ExactLength.Moneda);
                return false;
            }

            if (!Catalogos.Catalogo02.ContainsKey(Moneda))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.AllowedCatalogValues, Field, "Catálogo 02");
                return false;
            }

            return true;
        }

        public static bool ValidateFecha(String Trigger, String Fecha, out String ResponseMessage)
        {
            String Field = $"Fecha({Trigger})";
            ResponseMessage = "";
            
            if (String.IsNullOrEmpty(Fecha))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            var SplitedFecha = (Fecha.Split('-').Length == 3) ? Fecha.Split('-') : Fecha.Split('/');
            Fecha = $"{SplitedFecha[2].Trim()}-{SplitedFecha[1].Trim()}-{SplitedFecha[0].Trim()}";

            Int16 FechaSplit1 = (Int16)SplitedFecha.Length;
                
            if (FechaSplit1 != 3)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.InvalidDate, Field);
                return false;
            }

            if (!DateTime.TryParse(Fecha, out DateTime dateTime))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.InvalidDate, Field);
                return false;
            }

            return true;
        }

        #region   -- Items -- 

        public static bool ValidateItemUnidadMedida(String Trigger, String UnidadMedida, out String ResponseMessage)
        {
            String Field = $"Unidad de Medida({Trigger})";
            ResponseMessage = "";

            if (String.IsNullOrEmpty(UnidadMedida))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (UnidadMedida.Length > ConstantData.Validation.MaxLength.UnidadMedida)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.MaxLength, Field, ConstantData.Validation.MaxLength.UnidadMedida);
                return false;
            }

            if (!Catalogos.Catalogo03.ContainsKey(UnidadMedida))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.AllowedCatalogValues, Field, "Catálogo 03");
                return false;
            }

            return true;
        }

        public static bool ValidateItemCantidadUnidades(String Trigger, long? CantidadUnidades, out String ResponseMessage)
        {
            String Field = $"Cantidad Unidades({Trigger})";
            ResponseMessage = "";

            if (CantidadUnidades == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (CantidadUnidades.ToString().Length > ConstantData.Validation.MaxLength.CantidadUnidades)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.MaxLength, Field, ConstantData.Validation.MaxLength.CantidadUnidades);
                return false;
            }

            Match match = Regex.Match(CantidadUnidades.ToString(), ConstantData.Validation.RegEx.CantidadUnidades);
            if (!match.Success)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.InvalidData, Field);
                return false;
            }

            if (CantidadUnidades <= 0)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.InvalidData, Field);
                return false;
            }

            return true;
        }

        public static bool ValidateItemDescripcion(String Trigger, String Descripcion, out String ResponseMessage)
        {
            String Field = $"Descripcion ({Trigger})";
            ResponseMessage = "";

            if (String.IsNullOrEmpty(Descripcion))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (Descripcion.Length > ConstantData.Validation.MaxLength.DescripcionItem)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.MaxLength, Field, ConstantData.Validation.MaxLength.DescripcionItem);
                return false;
            }

            return true;
        }

        public static bool ValidateItemValorUnitario(String Trigger, decimal? ValorUnitario, out String ResponseMessage)
        {
            String Field = $"Valor Unitario ({Trigger})";
            ResponseMessage = "";

            if (ValorUnitario == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal(Field, ValorUnitario, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateItemPrecioVenta(String Trigger, decimal? PrecioVenta, out String ResponseMessage)
        {
            String Field = $"Precio Venta ({Trigger})";
            ResponseMessage = "";

            if (PrecioVenta == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal(Field, PrecioVenta, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateItemIGV(String Trigger, String Afectacion, decimal? Monto, out String ResponseMessage)
        {
            ResponseMessage = "";

            if (String.IsNullOrEmpty(Afectacion))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, $"Afectacion IGV ({Trigger})");
                return false;
            }

            if (Afectacion.Length != ConstantData.Validation.ExactLength.AfectacionIGV)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.ExactLength, $"Afectacion IGV ({Trigger})", ConstantData.Validation.ExactLength.AfectacionIGV);
                return false;
            }

            if (!Catalogos.Catalogo07.ContainsKey(Afectacion))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.AllowedCatalogValues, $"Afectacion IGV ({Trigger})", "Catálogo 07");
                return false;
            }

            if (Monto == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, $"Monto IGV ({Trigger})");
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal($"Monto IGV ({Trigger})", Monto, out ResponseMessage))
                return false;


            return true;
        }

        public static bool ValidateItemISC(String Trigger, String Sistema, decimal? Monto, out String ResponseMessage)
        {
            ResponseMessage = "";

            if (String.IsNullOrEmpty(Sistema))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, $"Sistema ISC ({Trigger})");
                return false;
            }

            if (Sistema.Length != ConstantData.Validation.ExactLength.SistemaISC)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.ExactLength, $"Sistema ISC ({Trigger})", ConstantData.Validation.ExactLength.SistemaISC);
                return false;
            }

            if (!Catalogos.Catalogo08.ContainsKey(Sistema))
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.AllowedCatalogValues, $"Sistema ISC ({Trigger})", "Catálogo 08");
                return false;
            }

            if (Monto == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, $"Monto ISC ({Trigger})");
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal($"Monto ISC ({Trigger})", Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateItemValorVenta(String Trigger, decimal? ValorVenta, out String ResponseMessage)
        {
            String Field = $"Valor Venta ({Trigger})";
            ResponseMessage = "";

            if (ValorVenta == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal(Field, ValorVenta, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateItemCodigoProducto(String Trigger, String Codigo, out String ResponseMessage)
        {
            String Field = $"Codigo Producto {Trigger}";
            ResponseMessage = "";

            if (Codigo.Length > ConstantData.Validation.MaxLength.CodigoItem)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.MaxLength, Field, ConstantData.Validation.MaxLength.CodigoItem);
                return false;
            }

            return true;
        }

        public static bool ValidateItemOpeNoOnerosas(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            ResponseMessage = "";

            if (Monto == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, $"Valor Referencial Operaciones No Onerosas({Trigger})");
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal($"Valor Referencial Operaciones No Onerosas ({Trigger})", Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateItemDescuento(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Descuento ({Trigger})";
            ResponseMessage = "";

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        #endregion

        #region  -- Montos Globales -- 

        public static bool ValidateMGOpeGravadas(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Total Operaciones Gravadas({Trigger})";
            ResponseMessage = "";
            
            if (Monto == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGOpeInafectas(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Total Operaciones Inafectas({Trigger})";
            ResponseMessage = "";
            
            if (Monto == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGOpeExoneradas(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Total Operaciones Exoneradas({Trigger})";
            ResponseMessage = "";
            
            if (Monto == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGOpeGratuitas(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Total Operaciones Gratuitas({Trigger})";
            ResponseMessage = "";
            
            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGSumaIGV(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Total Sumatoria IGV({Trigger})";
            ResponseMessage = "";
            
            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGSumaISC(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Total Sumatoria ISC({Trigger})";
            ResponseMessage = "";

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGSumaOtrosTributos(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Total Sumatoria Otros Tributos({Trigger})";
            ResponseMessage = "";

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGSumaOtrosCargos(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Total Sumatoria Otros Cargos({Trigger})";
            ResponseMessage = "";

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGImporteTotalVenta(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Importe Total Venta ({Trigger})";
            ResponseMessage = "";

            if (Monto == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, Field);
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGImportePercepcion(String Trigger, decimal? BaseImponible, decimal? Monto, decimal? MontoTotal, out String ResponseMessage)
        {
            ResponseMessage = "";

            if (BaseImponible == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, $"Monto Base Imponible Percepcion({Trigger})");
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal($"Monto Base Imponible Percepcion({Trigger})", BaseImponible, out ResponseMessage))
                return false;


            if (Monto == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, $"Monto Percepcion({Trigger})");
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal($"Monto Percepcion({Trigger})", Monto, out ResponseMessage))
                return false;


            if (MontoTotal == null)
            {
                ResponseMessage = String.Format(ConstantData.Validation.Messages.Required, $"Monto Total Percepcion({Trigger})");
                return false;
            }

            if (!DataTypesValidators.ValidateDecimal($"Monto Total Percepcion({Trigger})", MontoTotal, out ResponseMessage))
                return false;

            return true;
        }
        
        public static bool ValidateMGTotalDescuentosGlobales(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Total Descuentos Globales({Trigger})";
            ResponseMessage = "";

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        public static bool ValidateMGImporteTotalDescuentos(String Trigger, decimal? Monto, out String ResponseMessage)
        {
            String Field = $"Importe Total Descuentos ({Trigger})";
            ResponseMessage = "";

            if (!DataTypesValidators.ValidateDecimal(Field, Monto, out ResponseMessage))
                return false;

            return true;
        }

        #endregion
        
        #region -- Referral Guide --

        public static bool ValidateReferralGuide(String trigger, String guideType, String guideIdentifier, out String responseMessage)
        {
            responseMessage = "";
            if (!Catalogos.Catalogo01.ContainsKey(guideType))
            {
                responseMessage = String.Format(ConstantData.Validation.Messages.AllowedCatalogValues, $"Tipo ({trigger})", "Catálogo 01");
                return false;
            }

            return true;
        }

        #endregion



    }

    public class DataTypesValidators
    {
        public static bool ValidateDecimal(String Field, decimal? Number, out String Message)
        {
            Message = "";

            if (Number.ToString().Length > ConstantData.Validation.MaxLength.DecimalValues)
            {
                Message = String.Format(ConstantData.Validation.Messages.MaxLength, Field, ConstantData.Validation.MaxLength.DecimalValues);
                return false;
            }

            Match match = Regex.Match(Number.ToString(), ConstantData.Validation.RegEx.DecimalValues);
            if (!match.Success)
            {
                Message = String.Format(ConstantData.Validation.Messages.InvalidData, Field);
                return false;
            }

            return true;
        }
    }
}