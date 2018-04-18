using APIFacturacion.Helpers;
using APIFacturacion.Util.Validation.Shared;
using CDFacturacion.Factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace APIFacturacion.Util.Validation
{
    public class CheckFactura
    {
        public static bool ValidateFactura(Factura Factura)
        {
            try
            {
                String Trigger = "";
                String ValidationResponse = "";

                #region  -- Emisor -- 

                var Emisor = Factura.Emisor;
                Trigger = "Emisor";


                // Validar Firma Digital
                if (!FieldsValidators.ValidateFirmaDigital(Trigger, Emisor.FirmaDigital, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);


                // Validar Tipo de Documento de Identidad
                if (FieldsValidators.ValidateTipoDocumentoIdentidad(Trigger, Emisor.DocumentoTipo, out ValidationResponse))
                {
                    // La Facturación Electrónica solo es permitido para emisores cuyo documento de identidad sea 'RUC'.
                    if (Emisor.DocumentoTipo != ConstantData.Catalogos.Catalogo06.RUC)
                        throw new CustomInvalidDataException("Proceso disponible para emisores cuyo tipo de documento de identidad sea RUC");
                }
                else
                {
                    throw new CustomInvalidDataException(ValidationResponse);
                }

                
                // Validar Número de Documento
                if (!FieldsValidators.ValidateNumeroDocumentoIdentidad(Trigger, Emisor.DocumentoTipo, Emisor.DocumentoNumero, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);
                
                
                // Validar Razón Social
                if (!FieldsValidators.ValidateRazonSocial(Trigger, Emisor.NomRazonSoc, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);


                // Validar Nombre Comercial
                if (!String.IsNullOrEmpty(Emisor.NombreComercial))
                {
                    if (!FieldsValidators.ValidateNombreComercial(Trigger, Emisor.NombreComercial, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }


                // Validar Número de Celular
                if (!String.IsNullOrEmpty(Emisor.NumCelular))
                {
                    if (!FieldsValidators.ValidateNumeroCelular(Trigger, Emisor.NumCelular, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }


                // Validar Email
                if (!String.IsNullOrEmpty(Emisor.Email))
                {
                    if (!FieldsValidators.ValidateEmail(Trigger, Emisor.Email, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }

                #endregion

                #region  -- Cliente -- 

                var Cliente = Factura.Cliente;
                Trigger = "Cliente";

                // Validar Tipo de Documento de Identidad
                if (FieldsValidators.ValidateTipoDocumentoIdentidad(Trigger, Cliente.DocumentoTipo, out ValidationResponse))
                {
                    // La Facturación Electrónica solo es permitido para clientes cuyo documento de identidad sea 'RUC'.
                    if (Emisor.DocumentoTipo != ConstantData.Catalogos.Catalogo06.RUC)
                        throw new CustomInvalidDataException("Proceso disponible para clientes cuyo tipo de documento de identidad sea RUC");
                }
                else
                {
                    throw new CustomInvalidDataException(ValidationResponse);
                }
                
                // Validar Número de Documento
                if (!FieldsValidators.ValidateNumeroDocumentoIdentidad(Trigger, Emisor.DocumentoTipo, Cliente.DocumentoNumero, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);

                // Validar Razón Social
                if (!FieldsValidators.ValidateRazonSocial(Trigger, Cliente.NomRazonSoc, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);
                
                #endregion

                #region -- Detalles --

                var Detalles = Factura.Detalles;
                Trigger = "Detalles";
                

                if (FieldsValidators.ValidateTipoDocumentoTributario(Trigger, Detalles.DocumentoTipo, out ValidationResponse))
                {
                    // La Facturación Electrónica solo es permitido para documentos tributarios 'Factura'.
                    if (Detalles.DocumentoTipo != ConstantData.Catalogos.Catalogo01.Factura)
                        throw new CustomInvalidDataException("Proceso disponible solo para documento tributario 'Factura'");
                }
                else
                {
                    throw new CustomInvalidDataException(ValidationResponse);
                }
                

                if (!FieldsValidators.ValidateNumeroDocumentoTributario(Trigger, Detalles.DocumentoTipo, Detalles.DocumentoNumero, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);


                if (!FieldsValidators.ValidateMoneda(Trigger, Detalles.Moneda, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);


                if (!FieldsValidators.ValidateFecha(Trigger, Detalles.FechaEmision, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);


                #region  --- Items Factura --- 

                var Items = Detalles.ListaItems;
                Int16 i = 0;
                foreach (var item in Items)
                {
                    i++;
                    Trigger = $"Detalle - Item {i}";

                    if (item.ID == null)
                    {
                        if (item.ID != i)
                            throw new CustomInvalidDataException("Verificar orden correcto de los items.");
                    }
                    else
                    {
                        throw new CustomInvalidDataException("El valor ID de los items es obligatorio.");
                    }


                    if (!FieldsValidators.ValidateItemUnidadMedida(Trigger, item.UnidadTipo, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);


                    if (!FieldsValidators.ValidateItemCantidadUnidades(Trigger, item.UnidadCantidad, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);


                    if (!FieldsValidators.ValidateItemDescripcion(Trigger, item.DescripcionDetallada, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);


                    if (!FieldsValidators.ValidateItemValorUnitario(Trigger, item.ValorUnitario, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);


                    if (!FieldsValidators.ValidateItemPrecioVenta(Trigger, item.PrecioVenta, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);


                    if (!FieldsValidators.ValidateItemIGV(Trigger, item.AfectacionIGV, item.ItemMontoIGV, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);


                    if (!String.IsNullOrEmpty(item.SistemaISC))
                    {
                        if (!FieldsValidators.ValidateItemISC(Trigger, item.SistemaISC, item.ItemMontoISC, out ValidationResponse))
                            throw new CustomInvalidDataException(ValidationResponse);
                    }


                    if (!FieldsValidators.ValidateItemValorVenta(Trigger, item.ItemValorVenta, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);


                    if (!String.IsNullOrEmpty(item.CodigoProducto))
                    {
                        if (!FieldsValidators.ValidateItemCodigoProducto(Trigger, item.CodigoProducto, out ValidationResponse))
                            throw new CustomInvalidDataException(ValidationResponse);
                    }

                    if (item.ItemDescuentos != null)
                    {
                        if (!FieldsValidators.ValidateItemDescuento(Trigger, item.ItemDescuentos, out ValidationResponse))
                            throw new CustomInvalidDataException(ValidationResponse);
                    }

                    if (item.OperacionesNoOnerosasValUnitario != null)
                    {
                        if (!FieldsValidators.ValidateItemOpeNoOnerosas(Trigger, item.OperacionesNoOnerosasValUnitario, out ValidationResponse))
                            throw new CustomInvalidDataException(ValidationResponse);
                    }
                }

                #endregion

                #region --- Montos Globales ---

                var MontosGlobales = Detalles.MontosGlobales;

                if (!FieldsValidators.ValidateMGOpeGravadas(Trigger, MontosGlobales.TVVOperacionesGravadas, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);


                if (!FieldsValidators.ValidateMGOpeInafectas(Trigger, MontosGlobales.TVVOperacionesInafectas, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);


                if (!FieldsValidators.ValidateMGOpeExoneradas(Trigger, MontosGlobales.TVVOperacionesExoneradas, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);

                if (MontosGlobales.SumatoriaIGV != null)
                {
                    if (!FieldsValidators.ValidateMGSumaIGV(Trigger, MontosGlobales.SumatoriaIGV, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }

                if (MontosGlobales.SumatoriaISC != null)
                {
                    if (!FieldsValidators.ValidateMGSumaISC(Trigger, MontosGlobales.SumatoriaISC, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }

                if (MontosGlobales.SumatoriaOtrosTributos != null)
                {
                    if (!FieldsValidators.ValidateMGSumaOtrosTributos(Trigger, MontosGlobales.SumatoriaOtrosTributos, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }

                if (MontosGlobales.SumatoriaOtrosCargos != null)
                {
                    if (!FieldsValidators.ValidateMGSumaOtrosCargos(Trigger, MontosGlobales.SumatoriaOtrosCargos, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }

                
                if (!FieldsValidators.ValidateMGImporteTotalVenta(Trigger, MontosGlobales.ImporteTotalVenta, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);

                if (MontosGlobales.PercepcionBaseImponible != null && MontosGlobales.PercepcionMonto != null && MontosGlobales.PercepcionMontoTotal != null)
                {
                    if (!FieldsValidators.ValidateMGImportePercepcion(Trigger, MontosGlobales.PercepcionBaseImponible, MontosGlobales.PercepcionMonto, MontosGlobales.PercepcionMontoTotal, out ValidationResponse))
                       throw new CustomInvalidDataException(ValidationResponse);
                }

                if (MontosGlobales.TVVOperacionesGratuitas != null)
                {
                    if (!FieldsValidators.ValidateMGOpeGratuitas(Trigger, MontosGlobales.TVVOperacionesGratuitas, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }

                if (MontosGlobales.DescuentosGlobales != null)
                {
                    if (!FieldsValidators.ValidateMGTotalDescuentosGlobales(Trigger, MontosGlobales.DescuentosGlobales, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }

                if (MontosGlobales.TotalDescuentos != null)
                {
                    if (!FieldsValidators.ValidateMGImporteTotalDescuentos(Trigger, MontosGlobales.TotalDescuentos, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }

                #endregion

                #region --- Guía de Remisión ---



                #endregion
                return true;
                
                #endregion

            }
            catch (CustomInvalidDataException e)
            {
                throw e;
            }
            catch (Exception)
            {
                throw new CustomInvalidDataException("Error interno.");
            }
        }
    }
}