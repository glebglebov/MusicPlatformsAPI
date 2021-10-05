using KMChartsUpdater.BLL.DTO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;

namespace KMChartsUpdater.BLL.Utils.Extensions
{
    public static class XGraphicsExtensions
    {
        private static readonly XFont _underlinedFont = new XFont("OpenSans", 14, XFontStyle.Underline);

        public static void DrawLink(this XGraphics gfx, string linkText, string link, double x, double y, double height,
            double width)
        {
            var page = gfx.PdfPage;

            XTextFormatter tf = new XTextFormatter(gfx);

            var rect = new XRect(x, y, width, height);
            tf.DrawString(linkText, _underlinedFont, XBrushes.Blue, rect, XStringFormats.TopLeft);
        }
    }
}
