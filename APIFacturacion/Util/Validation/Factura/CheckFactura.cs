using APIFacturacion.Helpers;
using APIFacturacion.Util.Validation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace APIFacturacion.Util.Validation.Factura
{
    public class CheckFactura
    {
        public static bool ValidateFactura(CDFacturacion.Factura.Factura Factura)
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
                if (!FieldsValidators.ValidateNombreComercial(Trigger, Emisor.NombreComercial, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);

                // Validar Número de Celular
                if (!FieldsValidators.ValidateNumeroCelular(Trigger, Emisor.NumCelular, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);

                // Validar Email
                if (!FieldsValidators.ValidateEmail(Trigger, Emisor.Email, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);

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

                // Validar Tipo de Documento Tributario
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

                // Validar Número de Documento Tributario
                if (!FieldsValidators.ValidateNumeroDocumentoTributario(Trigger, Detalles.DocumentoTipo, Detalles.DocumentoNumero, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);

                // Validar Moneda
                if (!FieldsValidators.ValidateMoneda(Trigger, Detalles.Moneda, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);

                // Validar Fecha
                if (!FieldsValidators.ValidateFecha(Trigger, Detalles.FechaEmision, out ValidationResponse))
                    throw new CustomInvalidDataException(ValidationResponse);

                #region  --- Items Factura --- 

                var Items = Detalles.ListaItems;
                Int16 i = 0;
                foreach (var item in Items)
                {
                    i++;
                    Trigger = $"Detalle - Item {i}";

                    // Validar Unidad de Medida
                    if (!FieldsValidators.ValidateItemUnidadMedida(Trigger, item.UnidadTipo, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);

                    // Validar Cantidad de Unidades
                    if (!FieldsValidators.ValidateItemCantidadUnidades(Trigger, item.UnidadCantidad, out ValidationResponse))
                        throw new CustomInvalidDataException(ValidationResponse);
                }

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