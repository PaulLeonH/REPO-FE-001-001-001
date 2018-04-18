using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

using ZXing;
using System.Drawing;
using System.Drawing.Imaging;

using CDFacturacion.Factura;
using System.Globalization;
using System.Web;

namespace APIFacturacion.Util.GenerarPDF
{

    public class G_Factura
    {
        Factura factura;

        public G_Factura(Factura factura)
        {
            this.factura = factura;
        }

        public async Task<BlobUploadModel> GenerarAsync()
        {
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(0, 0, 60, 60);
            try
            {
                var negritaNegro = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7);
                var estandarNegro = FontFactory.GetFont(FontFactory.HELVETICA, 7);

                var negritaBlanco = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 15, BaseColor.WHITE);

                MemoryStream ms = new MemoryStream();
                PdfWriter pw = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                #region Detalles PDf

                doc.AddTitle("Factura Electrónica");
                doc.AddAuthor("ENCHUFATE");


                // DATOS EMISOR
                PdfContentByte cb0 = pw.DirectContent;

                DomicilioFiscal domFiscEmisor = factura.Emisor.DomicilioFiscal;

                Paragraph para0 = new Paragraph();
                para0.Leading = 10;
                para0.Add(new Chunk(factura.Emisor.NomRazonSoc, negritaNegro));
                para0.Add(Chunk.NEWLINE);
                para0.Add(new Chunk($"{domFiscEmisor.Direccion} {domFiscEmisor.Urbanizacion} - {domFiscEmisor.Distrito}", estandarNegro));
                para0.Add(Chunk.NEWLINE);
                para0.Add(new Chunk($"{domFiscEmisor.Provincia} - {domFiscEmisor.Departamento}", estandarNegro));
                para0.Add(Chunk.NEWLINE);
                para0.Add(new Chunk($"Telefono : {factura.Emisor.NumCelular}         Email : {factura.Emisor.Email}", estandarNegro));

                iTextSharp.text.Rectangle rec0 = new iTextSharp.text.Rectangle(55, 700, 365, 750);

                ColumnText ct0 = new ColumnText(cb0);
                ct0.SetSimpleColumn(rec0);
                ct0.AddElement(para0);
                ct0.Go();

                // DATOS ADQUIRIENTE
                PdfContentByte cb1 = pw.DirectContent;
                cb1.Rectangle(55, 630, 318, 60);
                cb1.Stroke();

                DomicilioFiscal domFiscCliente = factura.Cliente.DomicilioFiscal;

                Paragraph para1 = new Paragraph();
                para1.Leading = 11;
                para1.Add(new Chunk("ADQUIRIENTE", negritaNegro));
                para1.Add(Chunk.NEWLINE);
                para1.Add(new Chunk($"RUC : {factura.Cliente.DocumentoNumero}", estandarNegro));
                para1.Add(Chunk.NEWLINE);
                para1.Add(new Chunk($"{factura.Cliente.Nom_RazonSoc}", estandarNegro));
                para1.Add(Chunk.NEWLINE);
                para1.Add(new Chunk($"{domFiscCliente.Direccion} - {domFiscCliente.Distrito} - {domFiscCliente.Provincia} - {domFiscCliente.Departamento}", estandarNegro));

                iTextSharp.text.Rectangle rec1 = new iTextSharp.text.Rectangle(60, 630, 368, 685);

                ColumnText ct1 = new ColumnText(cb1);
                ct1.SetSimpleColumn(rec1);
                ct1.AddElement(para1);
                ct1.Go();


                // DATOS GENERALES DETALLE
                PdfContentByte cb2 = pw.DirectContent;
                cb2.Rectangle(378, 630, doc.PageSize.Width - 433, 60);
                cb2.Stroke();

                Paragraph para2 = new Paragraph();
                para2.Leading = 11;
                para2.Add(new Chunk("FECHA EMISIÓN : ", negritaNegro));
                para2.Add(new Chunk($"{factura.Detalles.FechaEmision.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}", estandarNegro));
                para2.Add(Chunk.NEWLINE);
                //para2.Add(new Chunk("FECHA DE VENC. : ", negritaNegro));
                //para2.Add(new Chunk("23/02/2018", estandarNegro));
                //para2.Add(Chunk.NEWLINE);
                para2.Add(new Chunk("MONEDA : ", negritaNegro));
                para2.Add(new Chunk("NUEVOS SOLES", estandarNegro));
                para2.Add(Chunk.NEWLINE);
                para2.Add(new Chunk("IGV : ", negritaNegro));
                para2.Add(new Chunk("18.03%", estandarNegro));

                iTextSharp.text.Rectangle rec2 = new iTextSharp.text.Rectangle(384, 635, doc.PageSize.Width - 63, 685);

                ColumnText ct2 = new ColumnText(cb2);
                ct2.SetSimpleColumn(rec2);
                ct2.AddElement(para2);
                ct2.Go();



                // NÚMERO DOCUMENTO
                PdfContentByte cb3 = pw.DirectContent;
                cb3.Rectangle(378, 710, doc.PageSize.Width - 433, 90);
                cb3.SetColorFill(BaseColor.BLUE);
                cb3.FillStroke();
                cb3.Stroke();

                Paragraph para3 = new Paragraph();
                para3.Leading = 17;
                para3.Alignment = Element.ALIGN_CENTER;
                para3.Add(new Chunk($"RUC {factura.Emisor.DocumentoNumero}", negritaBlanco));
                para3.Add(Chunk.NEWLINE);
                para3.Add(new Chunk("FACTURA", negritaBlanco));
                para3.Add(Chunk.NEWLINE);
                para3.Add(new Chunk("ELECTRÓNICA", negritaBlanco));
                para3.Add(Chunk.NEWLINE);
                para3.Add(new Chunk($"{factura.Detalles.DocumentoNumero}", negritaBlanco));

                iTextSharp.text.Rectangle rec3 = new iTextSharp.text.Rectangle(383, 715, doc.PageSize.Width - 63, 795);

                ColumnText ct3 = new ColumnText(cb3);
                ct3.SetSimpleColumn(rec3);
                ct3.AddElement(para3);
                ct3.Go();

                Paragraph pvacio = new Paragraph();
                pvacio.Add(new Chunk(".", negritaBlanco));

                doc.Add(pvacio);

                for (int i = 0; i < 9; i++) { doc.Add(Chunk.NEWLINE); }


                // Tabla items

                PdfPTable tablaItems = new PdfPTable(6);

                tablaItems.TotalWidth = 475f;
                tablaItems.LockedWidth = true;

                float[] widths = new float[] { 2.2f, 2.3f, 2.9f, 9f, 2.8f, 3.8f };
                tablaItems.SetWidths(widths);

                string[] titulos = new string[] { "CANT", "UM", "COD", "DESC", "P/U", "IMPORTE" };
                string[] montosDesc = new string[] { "ANTICIPO (-)", "GRAVADA", "IGV", "TOTAL" };
                decimal[] montosVal = new decimal[] { 0.00M, factura.Detalles.MontosGlobales.TVVOperacionesGravadas, factura.Detalles.MontosGlobales.SumatoriaIGV, factura.Detalles.MontosGlobales.ImporteTotalVenta };

                for (int i = 0; i < titulos.Length; i++)
                {
                    PdfPCell cell = new PdfPCell();
                    if (i == 0)
                    {
                        cell = PintarCeldas(BaseColor.BLUE, 0);
                    }
                    else if (i == titulos.Length - 1)
                    {
                        cell = PintarCeldas(BaseColor.BLUE, 1);
                    }
                    else
                    {
                        cell = PintarCeldas(BaseColor.BLUE, 99);
                    }
                    cell.FixedHeight = 16;
                    cell.BorderColorTop = BaseColor.GRAY;
                    var title = new Chunk(titulos[i], FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.WHITE));
                    Phrase phrase = new Phrase(title);
                    cell.Phrase = phrase;
                    tablaItems.AddCell(cell);
                }


                int numeroRegistros = factura.Detalles.ListaItems.Count;
                int numFilaTabla = getNumeroFilasTabla(numeroRegistros);


                for (int i = 0; i < numFilaTabla; i++)
                {
                    ItemFactura itemFac;
                    string[] detalles = null;
                    if (i < numeroRegistros)
                    {
                        itemFac = factura.Detalles.ListaItems[i];
                        detalles = new string[] {   itemFac.UnidadCantidad.ToString(),
                                                    itemFac.UnidadTipo,
                                                    itemFac.CodigoProducto,
                                                    itemFac.DescripcionDetallada,
                                                    itemFac.ValorUnitario.ToString(),
                                                    itemFac.ItemValorVenta.ToString()};
                    }

                    if (i == 32)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            for (int k = 0; k < titulos.Length; k++)
                            {
                                PdfPCell cellNull = PintarCeldas(BaseColor.WHITE, 2);
                                tablaItems.AddCell(cellNull);
                            }
                        }
                    }

                    if (i % 2 == 0)
                    {
                        for (int j = 0; j < titulos.Length; j++)
                        {
                            PdfPCell cell = new PdfPCell();
                            if (j == 0)
                            {
                                cell = PintarCeldas(BaseColor.LIGHT_GRAY, 0);
                            }
                            else if (j == titulos.Length - 1)
                            {
                                cell = PintarCeldas(BaseColor.LIGHT_GRAY, 1);
                            }
                            else
                            {
                                cell = PintarCeldas(BaseColor.LIGHT_GRAY, 99);
                            }

                            if (i < numeroRegistros)
                            {
                                var reg = new Chunk(detalles[j] + (i + 1), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.BLACK));
                                cell.Phrase = new Phrase(reg);
                            }
                            tablaItems.AddCell(cell);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < titulos.Length; j++)
                        {
                            PdfPCell cell = new PdfPCell();
                            if (j == 0)
                            {
                                cell = PintarCeldas(BaseColor.WHITE, 0);
                            }
                            else if (j == titulos.Length - 1)
                            {
                                cell = PintarCeldas(BaseColor.WHITE, 1);
                            }
                            else
                            {
                                cell = PintarCeldas(BaseColor.WHITE, 99);
                            }

                            if (i < numeroRegistros)
                            {
                                var reg = new Chunk(detalles[j] + (i + 1), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.BLACK));
                                cell.Phrase = new Phrase(reg);
                            }
                            tablaItems.AddCell(cell);
                        }
                    }
                }

                // Detalles Montos Generales
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < titulos.Length; j++)
                    {
                        PdfPCell cell = new PdfPCell();

                        if (j == 0)
                        {
                            cell = PintarCeldas(BaseColor.WHITE, 0);
                        }
                        else if (j == titulos.Length - 1)
                        {
                            cell = PintarCeldas(BaseColor.WHITE, 1);
                        }
                        else
                        {
                            cell = PintarCeldas(BaseColor.WHITE, 99);
                        }

                        if (i == 3)
                        {
                            cell.BorderColorBottom = BaseColor.DARK_GRAY;
                        }

                        if (j == 3)
                        {
                            cell.HorizontalAlignment = 2;
                            var reg = new Chunk(montosDesc[i], FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.BLACK));
                            cell.Phrase = new Phrase(reg);
                        }
                        else if (j == 4)
                        {
                            cell.HorizontalAlignment = 1;
                            var reg = new Chunk("S/.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.BLACK));
                            cell.Phrase = new Phrase(reg);
                        }
                        else if (j == 5)
                        {
                            var reg = new Chunk(montosVal[i].ToString("0.00"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.BLACK));
                            cell.Phrase = new Phrase(reg);
                        }
                        tablaItems.AddCell(cell);
                    }
                }
                doc.Add(tablaItems);

                // Monto Letras
                Paragraph pMontoLetras = new Paragraph();
                pMontoLetras.Alignment = 1;
                pMontoLetras.Add(new Chunk("IMPORTE EN LETRAS : ", negritaNegro));
                pMontoLetras.Add(new Chunk("AQUI TIENE QUE IR EL IMPORTE EN LETRAS", estandarNegro));
                doc.Add(pMontoLetras);


                // Linea Particional
                doc.Add(new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 81f, BaseColor.BLACK, 1, 1))));
                doc.Add(Chunk.NEWLINE);

                // Codigo QR
                string QRb64 = GetBase64QR();
                var imageData = Convert.FromBase64String(QRb64);
                var image = iTextSharp.text.Image.GetInstance(imageData);
                image.ScalePercent(40);
                image.Alignment = 2;

                PdfPTable tablaQR = new PdfPTable(3);
                tablaQR.TotalWidth = 475f;
                tablaQR.LockedWidth = true;
                tablaQR.SetWidths(new float[] { 2.2f, 0.025f, 0.65f });

                PdfPCell cellQR1 = new PdfPCell();
                Phrase cellQRPhrase = new Phrase();
                cellQRPhrase.Font = FontFactory.GetFont(FontFactory.TIMES, 7);
                cellQRPhrase.Add(" \n Representación impresa de la FACTURA ELECTRÓNICA, visita www.enchufateFact.com/20000000001 \n\n");
                cellQRPhrase.Add(" Autorizado mediante Resolución de Intendencia No.034-005-0005315");
                cellQR1.Phrase = cellQRPhrase;

                PdfPCell cellQR2 = new PdfPCell();
                cellQR2.UseVariableBorders = true;
                cellQR2.BorderColorTop = BaseColor.WHITE;
                cellQR2.BorderColorBottom = BaseColor.WHITE;

                PdfPCell cellQR3 = new PdfPCell(image);
                cellQR3.HorizontalAlignment = 2;
                cellQR3.PaddingTop = 6;
                cellQR3.FixedHeight = 110;
                cellQR3.PaddingRight = 2.8f;

                tablaQR.AddCell(cellQR1);
                tablaQR.AddCell(cellQR2);
                tablaQR.AddCell(cellQR3);

                doc.Add(tablaQR);


                // Footer
                PdfContentByte CBFooter = pw.DirectContent;
                PdfTemplate PTFooter = CBFooter.CreateTemplate(150f, 50f);
                pw.PageEvent = new PDFFooter();

                #endregion

                doc.Close();
                pw.Close();
                ms.Close();


                string nombreArchivo = "Factura" + factura.Detalles.DocumentoNumero + ".pdf";
                string nombreContainer = "facturas-" + factura.Emisor.DocumentoNumero;

                BlobUploadModel bum = new BlobUploadModel();
                MemoryPostedFile memoryPostedFile = new MemoryPostedFile(ms.ToArray(), nombreArchivo, "application/pdf");
                bum = await bum.UploadFileAsync(memoryPostedFile, nombreContainer);

                return bum;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int getNumeroFilasTabla(int numRegistros)
        {
            if (numRegistros <= 10)
            {
                return 10;
            }
            else if (numRegistros % 10 == 0)
            {
                return numRegistros;
            }
            else if ((numRegistros - 1) % 10 == 0 || (numRegistros - 6) % 10 == 0)
            {
                if (numRegistros == 31) return numRegistros;
                return numRegistros + 4;
            }
            else if ((numRegistros - 2) % 10 == 0 || (numRegistros - 7) % 10 == 0)
            {
                if (numRegistros == 32) return numRegistros;
                return numRegistros + 3;
            }
            else if ((numRegistros - 3) % 10 == 0 || (numRegistros - 8) % 10 == 0)
            {
                return numRegistros + 2;
            }
            else if ((numRegistros - 4) % 10 == 0 || (numRegistros - 9) % 10 == 0)
            {
                return numRegistros + 1;
            }
            else if ((numRegistros - 5) % 10 == 0)
            {
                return numRegistros;
            }
            else
            {
                return 1;
            }
        }

        public PdfPCell PintarCeldas(BaseColor color, int ordenInicFin)
        {
            PdfPCell cell = new PdfPCell();
            cell.BackgroundColor = color;
            cell.UseVariableBorders = true;
            cell.BorderColor = color;

            if (ordenInicFin == 0)
            {
                cell.BorderColorLeft = BaseColor.DARK_GRAY;
            }
            else if (ordenInicFin == 1)
            {
                cell.BorderColorRight = BaseColor.DARK_GRAY;
            }

            cell.FixedHeight = 14;
            cell.HorizontalAlignment = 1;

            return cell;
        }

        public string GetBase64QR()
        {
            String codigoaQR = "20100177774|01|F123|12345678|10.50|15.80|20180205|";

            using (MemoryStream ms = new MemoryStream())
            {

                BarcodeWriter br = new BarcodeWriter();
                br.Format = BarcodeFormat.QR_CODE;
                Bitmap bm = new Bitmap(br.Write(codigoaQR), 250, 250);

                bm.Save(ms, ImageFormat.Png);

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public class PDFFooter : PdfPageEventHelper
    {
        // write on top of document
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            tabFot.SpacingAfter = 10F;
            PdfPCell cell;
            tabFot.TotalWidth = 300F;
            cell = new PdfPCell(new Phrase(""));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            DateTime horario = DateTime.Now;
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            PdfPCell cell;
            tabFot.TotalWidth = 300F;


            string QRb64 = "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";
            var imageData = Convert.FromBase64String(QRb64);
            var image = iTextSharp.text.Image.GetInstance(imageData);
            image.ScalePercent(12);
            image.Alignment = 2;

            cell = new PdfPCell(image);
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }

    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public MemoryPostedFile(byte[] fileBytes, string fileName , string contentType)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
            this.ContentType = contentType;
            this.InputStream = new MemoryStream(fileBytes);
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName { get; }

        public override Stream InputStream { get; }

        public override string ContentType { get; }
    }
}