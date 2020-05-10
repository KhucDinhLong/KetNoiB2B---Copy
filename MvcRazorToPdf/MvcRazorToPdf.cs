using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using iTextSharp.tool.xml;

namespace MvcRazorToPdf
{
    public class MvcRazorToPdf
    {
        public byte[] GeneratePdfOutput(ControllerContext context, object model = null, string viewName = null,
            Action<PdfWriter, Document> configureSettings = null, bool isA4Rotate = false)
        {
            if (viewName == null)
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            context.Controller.ViewData.Model = model;
            byte[] output;
            using (var document = new Document())
            {
                using (var workStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, workStream);
                    

                    writer.CloseStream = false;
                   
                    //HTMLWorker worker = new HTMLWorker(document);
                    if (configureSettings != null)
                    {
                        configureSettings(writer, document);
                    }
                    else
                    {
                        document.Open();
                        document.OpenDocument();
                        //sunv updated for a4 Rotate
                        document.SetPageSize(isA4Rotate
                            ? iTextSharp.text.PageSize.A4.Rotate()
                            : new Rectangle(500f, 500f, 90));
                        document.NewPage();
                    }
                    FontFactory.Register(context.HttpContext.Server.MapPath("~/fonts/ARIALUNI.TTF"), "Arial Unicode MS");// "D:\\Projects\\SETA\\teacherzone\\SETA.Web\\fonts\\ARIALUNI.TTF", "Arial Unicode MS");
                    StyleSheet styleSheet = new StyleSheet();
                    try
                    {
                        using (var reader = new StringReader(RenderRazorView(context, viewName)))
                        {
                            styleSheet.LoadTagStyle(HtmlTags.BODY, HtmlTags.FACE, "Arial Unicode MS");
                            styleSheet.LoadTagStyle(HtmlTags.BODY, HtmlTags.ENCODING, BaseFont.IDENTITY_H);
                            //worker.SetStyleSheet(styleSheet);
                            //worker.Open();
                            //worker.StartDocument();
                            //worker.NewPage();
                            //worker.Parse(reader);
                            //worker.EndDocument();
                            //worker.Close();

                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, reader);
                            document.Close();
                            output = workStream.ToArray();
                        }
                    }
                    catch (Exception ex)
                    {
                        //worker.EndDocument();
                        //worker.Close();
                        document.Close();
                        document.CloseDocument();
                        document.Dispose();
                        throw;
                    }
                    
                }
            }
            return output;
        }

        public string RenderRazorView(ControllerContext context, string viewName)
        {
            IView viewEngineResult = ViewEngines.Engines.FindView(context, viewName, null).View;
            var sb = new StringBuilder();


            using (TextWriter tr = new StringWriter(sb))
            {
                var viewContext = new ViewContext(context, viewEngineResult, context.Controller.ViewData,
                    context.Controller.TempData, tr);
                viewEngineResult.Render(viewContext, tr);
            }
            //var retValue = "";
            //for (var i = 0; i < 50; i++)
            //{
            //    retValue += sb.ToString();
            //}
            return sb.ToString();
            //return retValue;
        }
    }
}