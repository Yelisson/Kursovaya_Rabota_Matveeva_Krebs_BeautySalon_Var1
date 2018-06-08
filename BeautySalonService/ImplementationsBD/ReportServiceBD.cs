using BeautySalonModel;
using BeautySalonService.BindingModel;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ImplementationsBD
{
    public class ReportServiceBD : IReportService
    {
        private AbstractDataBaseContext context;

        public ReportServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public void SaveServicePrice(ReportBindingModel model)
        {
            if (File.Exists(model.FileName))
            {
                File.Delete(model.FileName);
            }

            var winword = new Microsoft.Office.Interop.Word.Application();
            try
            {
                object missing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Document document =
                    winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;
                range.Text = "Прайс услуг";
                var font = range.Font;
                font.Size = 16;
                font.Name = "Times New Roman";
                font.Bold = 1;
                var paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;
                range.InsertParagraphAfter();

                var products = context.Services.ToList();
                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, products.Count, 2, ref missing, ref missing);

                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";

                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;

                for (int i = 0; i < products.Count; ++i)
                {
                    table.Cell(i + 1, 1).Range.Text = products[i].serviceName;
                    table.Cell(i + 1, 2).Range.Text = products[i].price.ToString();
                }
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;

                range.Text = "Дата: " + DateTime.Now.ToLongDateString();

                font = range.Font;
                font.Size = 12;
                font.Name = "Times New Roman";

                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 10;

                range.InsertParagraphAfter();
                object fileFormat = WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(model.FileName, ref fileFormat, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing);
                document.Close(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                winword.Quit();
            }
        }

        public List<DeliverysLoadViewModel> GetDeliverysLoad()
        {
            return context.Deliverys
                            .ToList()
                            .GroupJoin(
                                    context.DeliveryResources
                                                .ToList(),
                                    delivery => delivery,
                                    deliveryResource => deliveryResource.delivery,
                                    (delivery, deliveryResourceList) =>
            new DeliverysLoadViewModel
            {
                deliveryName = delivery.name,
                //TotalCount = deliveryResourceList.Sum(r => r.count),
                Resources = deliveryResourceList.Select(r => new Tuple<string, int>(r.resource?.resourceName, r.count))
            })
                            .ToList();
        }

        public void SaveDeliverysLoad(ReportBindingModel model)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                if (File.Exists(model.FileName))
                {
                    excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = 1;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs(model.FileName, XlFileFormat.xlExcel8, Type.Missing,
                        Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                Sheets excelsheets = excel.Workbooks[1].Worksheets;
                var excelworksheet = (Worksheet)excelsheets.get_Item(1);
                excelworksheet.Cells.Clear();
                excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                excelworksheet.PageSetup.CenterHorizontally = true;
                excelworksheet.PageSetup.CenterVertically = true;
                Microsoft.Office.Interop.Excel.Range excelcells = excelworksheet.get_Range("A1", "C1");
                excelcells.Merge(Type.Missing);
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Доставки";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 14;

                excelcells = excelworksheet.get_Range("A2", "C2");
                excelcells.Merge(Type.Missing);
                excelcells.Value2 = "на" + DateTime.Now.ToShortDateString();
                excelcells.RowHeight = 20;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;

                var dict = GetDeliverysLoad();
                if (dict != null)
                {
                    excelcells = excelworksheet.get_Range("C1", "C1");
                    foreach (var elem in dict)
                    {
                        excelcells = excelcells.get_Offset(2, -2);
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = elem.deliveryName;
                        excelcells = excelcells.get_Offset(1, 1);
                        if (elem.Resources.Count() > 0)
                        {
                            var excelBorder =
                                excelworksheet.get_Range(excelcells,
                                            excelcells.get_Offset(elem.Resources.Count() - 1, 1));
                            excelBorder.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                            excelBorder.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                            excelBorder.HorizontalAlignment = Constants.xlCenter;
                            excelBorder.VerticalAlignment = Constants.xlCenter;
                            excelBorder.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                                    Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                                    Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                    1);

                            foreach (var listElem in elem.Resources)
                            {
                                excelcells.Value2 = listElem.Item1;
                                excelcells.ColumnWidth = 10;
                                excelcells.get_Offset(0, 1).Value2 = listElem.Item2;
                                excelcells = excelcells.get_Offset(1, 0);
                            }
                        }
                        /*
                        excelcells = excelcells.get_Offset(0, -1);
                        excelcells.Value2 = "Итого";
                        excelcells.Font.Bold = true;
                        excelcells = excelcells.get_Offset(0, 2);
                        excelcells.Value2 = elem.TotalCount;
                        excelcells.Font.Bold = true;
                        */
                    }
                }
                excel.Workbooks[1].Save();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                excel.Quit();
            }
        }
        public string a(Queue<String> q) {
            return q.Dequeue();
        }

        public List<ClientOrdersModel> GetClientOrders(ReportBindingModel model)
        {
            Queue<String> servs = new Queue<string>();
            Queue<String> ress = new Queue<String>();
            
            List<ClientOrdersModel> com = context.Orders
                            .Where(rec => rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                            .Select(rec => new ClientOrdersModel
                            {
                                serviceList = rec.orderServices.Select(os => new ServiceViewModel
                                {
                                    serviceName = os.service.serviceName,
                                    ServiceResources = os.service.serviceResources.Select(res => new ServiceResourceViewModel
                                    {
                                        resourceName = res.resource.resourceName
                                    }).ToList()
                                }).ToList()                              
                            })
                            .ToList();
            foreach (var or in com) {
                String serv = "", res = "";
                foreach (var s in or.serviceList) {
                    serv += s.serviceName + "; ";
                    foreach(var r in s.ServiceResources)
                    {
                        res += "; ";
                    }
                }
                servs.Enqueue(serv);
                ress.Enqueue(res);
            }

            com = context.Orders
                            .Where(rec => rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                            .Select(rec => new ClientOrdersModel
                            {
                                clientFirstName = rec.client.clientFirstName,
                                clientSecondName = rec.client.clientSecondName,
                                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                            SqlFunctions.DateName("yyyy", rec.DateCreate),
                               // serviceListStr = servs.Dequeue(),
                                //resourceListStr = ress.Dequeue(),
                                serviceList = rec.orderServices.Select(os => new ServiceViewModel {
                                    id = os.serviceId,
                                    price = os.service.price,
                                    serviceName = os.service.serviceName,
                                    ServiceResources = os.service.serviceResources.Select(res => new ServiceResourceViewModel {
                                        id = res.resource.id,
                                        resourceName = res.resource.resourceName
                                    }).ToList()
                                }).ToList(),
                                status = rec.status.ToString(),

                                
                                //resourceList = rec.service.serviceResources.Select(res => new ResourceViewModel
                                //{                                 
                                //    resourceName = res.resource.resourceName                                    
                                //}).ToList()
                                

                            })
                            .ToList();
            foreach (var o in com) {
                o.serviceListStr = servs.Dequeue();
                o.resourceListStr = ress.Dequeue();
            }
            return com;
        }

        public void SaveClientOrders(ReportBindingModel model)
        {
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            var phraseTitle = new Phrase("Заказы клиентов",
                new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString() +
                                    " по " + model.DateTo.Value.ToShortDateString(),
                                    new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            PdfPTable table = new PdfPTable(6)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160, 100, 100, 100 });
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("Имя клиента", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Фамилия клиента", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата создания", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Услуга", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Статус", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Ресурсы", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            var list = GetClientOrders(model);
            var fontForCells = new iTextSharp.text.Font(baseFont, 10);
            for (int i = 0; i < list.Count; i++)
            {
                cell = new PdfPCell(new Phrase(list[i].clientFirstName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].clientSecondName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].DateCreate, fontForCells));
                table.AddCell(cell);
                 cell = new PdfPCell(new Phrase(list[i].serviceListStr, fontForCells));
                table.AddCell(cell);
                /*
                String res = "";
                foreach (var resource in list[i].serviceList)
                {
                    if (context.Services.Where(s => s.id == resource.id).Select(s => new ServiceViewModel { }).ToList().Count > 0)
                        res += context.Services.Where(s => s.id == resource.id).Select(s => new ServiceViewModel
                        {
                            serviceName = s.serviceName
                        }).ToList().ElementAt(0).serviceName + "; ";
                    //res += resource.serviceName + "; ";


                }
                cell = new PdfPCell(new Phrase(res, fontForCells));
                table.AddCell(cell);
                */
                cell = new PdfPCell(new Phrase(list[i].status, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].resourceListStr, fontForCells));
                table.AddCell(cell);
                /*
                String str = "";
                foreach(var s in list[i].serviceList)
                {
                    foreach (var resource in s.ServiceResources)
                    {
                        str+= resource.resourceName + "; ";
                        if (context.Services.Where(ser => ser.id == resource.id).Select(ser => new ServiceViewModel { }).ToList().Count > 0)
                            foreach (var reso in context.Services.Where(ser => ser.id == resource.id).Select(ser => new ServiceViewModel
                            {
                                ServiceResources = ser.serviceResources.Select(re => new ServiceResourceViewModel
                                {
                                    resourceName = re.resource.resourceName
                                }).ToList()
                            }).ToList().ElementAt(0).ServiceResources) {
                                str += reso.resourceName + "; ";
                            }
                    }

                }
                
                cell = new PdfPCell(new Phrase(str, fontForCells));
                table.AddCell(cell);
                */
            }


            doc.Add(table);

            doc.Close();
        }


        public List<DeliveryViewModel> GetAdminDeliverys(ReportBindingModel model)
        {
            return context.Deliverys
                            .Where(rec => rec.Date >= model.DateFrom && rec.Date <= model.DateTo)
                            .Select(rec => new DeliveryViewModel
                            {
                                id = rec.id,
                                name = rec.name,
                                count=rec.deliveryResources.Count,
                                DateNew = SqlFunctions.DateName("dd", rec.Date) + " " +
                                            SqlFunctions.DateName("mm", rec.Date) + " " +
                                            SqlFunctions.DateName("yyyy", rec.Date),
                                deliveryResource = rec.deliveryResources.Select(res => new DeliveryResourceViewModel
                                {
                                    id = res.id,
                                    resourceName = res.resource.resourceName,
                                    count = res.count,
                                    price = res.resource.price
                            }).ToList()


                            })
                            .ToList();
        }

        public void SaveAdminDeliverys(ReportBindingModel model)
        {
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            var phraseTitle = new Phrase("Заявки",
                new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString() +
                                    " по " + model.DateTo.Value.ToShortDateString(),
                                    new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            PdfPTable table = new PdfPTable(5)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160,100,100});
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("ID", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Название доставки", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата создания", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });         
            table.AddCell(new PdfPCell(new Phrase("Ресурсы", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            var list = GetAdminDeliverys(model);
            var fontForCells = new iTextSharp.text.Font(baseFont, 10);
            for (int i = 0; i < list.Count; i++)
            {
                cell = new PdfPCell(new Phrase(""+list[i].id, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].name, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(""+list[i].DateNew, fontForCells));
                table.AddCell(cell);
                String res = "";

                List<ResourceViewModel> rs = new List<ResourceViewModel>();
                foreach (var resource in list[i].deliveryResource)
                {
                    res += resource.resourceName;
                    //rs = new List<ResourceViewModel>(context.Resources.Where(r => r.id == resource.id).Select(re => new ResourceViewModel { resourceName = re.resourceName}).ToList());
                    //foreach (var elem in rs) {
                    //    res += elem.resourceName ;
                    //}

                }
                cell = new PdfPCell(new Phrase(res, fontForCells));
                table.AddCell(cell);
                if (list[i].deliveryResource.Count > 0)
                    cell = new PdfPCell(new Phrase("" + list[i].deliveryResource.ElementAt(0).count, fontForCells));
                else { cell = new PdfPCell(new Phrase("0", fontForCells)); }
                table.AddCell(cell);

            }

            doc.Add(table);

            doc.Close();
        }
    }
}
