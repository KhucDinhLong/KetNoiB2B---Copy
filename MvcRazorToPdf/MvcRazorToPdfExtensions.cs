using System;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace MvcRazorToPdf
{
    public static class MvcRazorToPdfExtensions
    {
        public static byte[] GeneratePdf(this ControllerContext context, object model = null, string viewName = null, Action<PdfWriter, Document> configureSettings = null, bool isA4Rotate = false)
        {
            return new MvcRazorToPdf().GeneratePdfOutput(context, model, viewName, configureSettings, isA4Rotate);
        }
    }
}