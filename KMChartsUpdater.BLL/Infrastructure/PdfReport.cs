using System.IO;
using KMChartsUpdater.BLL.ReportGenerator;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace KMChartsUpdater.BLL.Infrastructure
{
    public class PdfReport
    {
        private readonly PdfDocument _document;
        private readonly ReportContent _content;

        private XGraphics _gfx;

        private const double _coverSize = 50;
        private const double _padding = 20;
        private const double _fontSize = 14;
        private const double _lineHeight = 20;

        private double _leftColumnX;
        private double _rightColumnX;
        private double _rightColumnWidth;

        private double _y;
        private double _maxY;

        private readonly XFont _regularFont = new XFont("OpenSans", 14, XFontStyle.Regular);
        private readonly XFont _underlinedFont = new XFont("OpenSans", 14, XFontStyle.Underline);

        private readonly  XPen _blackLine = new XPen(XColors.Black);

        public PdfReport(ReportContent content)
        {
            _content = content;

            _document = new PdfDocument();
        }

        public void CreateAndSave(string filename)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = currentDirectory + "/Uploads/Reports/" + filename;

            InitParams();

            foreach (var group in _content.Groups)
            {
                if (group.Elements.Count < 1)
                    continue;

                DrawGroup(group);
            }

            _document.Save(path);
        }

        private void InitParams()
        {
            _gfx = CreateNewPage();

            _y = 110;
            _maxY = _gfx.PdfPage.Height - _y;

            _leftColumnX = _padding + 65;
            _rightColumnX = _gfx.PdfPage.Width * 0.6;
            _rightColumnWidth = _gfx.PdfPage.Width - _rightColumnX;
        }

        private void DrawGroup(ReportGroup group)
        {
            if (_y + _lineHeight + _coverSize + 10 > _maxY)
            {
                _gfx = CreateNewPage();
                _y = 110;
            }

            _gfx.DrawString(group.Name, _regularFont, XBrushes.Black, new XRect(0, _y, _gfx.PdfPage.Width, _lineHeight),
                XStringFormats.TopCenter);

            _y += _lineHeight + 10;

            foreach (var element in group.Elements)
            {
                if (_y + _coverSize + 5 > _maxY)
                {
                    _gfx = CreateNewPage();
                    _y = 110;
                }

                DrawElement(element);

                _y += _coverSize + 5;
            }
        }

        private void DrawElement(ReportElement element)
        {
            _gfx.DrawLine(_blackLine, _padding, _y, _gfx.PdfPage.Width - _padding, _y);

            _y += 5;

            string coverFullPath = Directory.GetCurrentDirectory() + element.PlaylistCoverPath;
            XImage image = XImage.FromFile(coverFullPath);

            _gfx.DrawImage(image, _padding, _y, _coverSize, _coverSize);

            double titleY = _y + _coverSize / 2;

            var textRect = new XRect(_rightColumnX, titleY, _rightColumnWidth, _lineHeight);
            var linkRectInv = new XRect(_rightColumnX, titleY - 14, _rightColumnWidth, _lineHeight);
            var linkRect = _gfx.Transformer.WorldToDefaultPage(linkRectInv);

            string text = $"{element.PlaylistName} (#{element.TrackPosition})";

            _gfx.DrawString(text, _regularFont, XBrushes.Black, _leftColumnX, textRect.Y);
            _gfx.DrawString("Перейти в плейлист", _underlinedFont, XBrushes.Blue, _rightColumnX, textRect.Y);

            _gfx.PdfPage.AddWebLink(new PdfRectangle(linkRect), element.PlaylistLink);
        }

        private XGraphics CreateNewPage()
        {
            var page = _document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            DrawTitle(gfx);
            DrawFooter(gfx);

            return gfx;
        }

        private void DrawTitle(XGraphics gfx)
        {
            var font = new XFont("OpenSans", 18, XFontStyle.Bold);

            gfx.DrawString(_content.ReportTitle, font, XBrushes.Black, new XRect(0, 30, gfx.PdfPage.Width, gfx.PdfPage.Height),
                XStringFormats.TopCenter);

            gfx.DrawString(_content.ReportSubtitle, font, XBrushes.Black, new XRect(0, 55, gfx.PdfPage.Width, gfx.PdfPage.Height),
                XStringFormats.TopCenter);
        }

        private void DrawFooter(XGraphics gfx)
        {
            string logoPath = Directory.GetCurrentDirectory() + "/Uploads/Static/logo-king.png";

            double logoHeight = 70;
            double logoWidth = 63;

            XImage image = XImage.FromFile(logoPath);

            gfx.DrawImage(image, 20, gfx.PdfPage.Height - logoHeight - 20, logoWidth, logoHeight);
        }

        private string AddLineBreaks(string str, int lineLength)
        {
            string formatted = str;

            int i = lineLength;

            while (i < formatted.Length)
            {
                formatted = formatted.Insert(i, "\n");

                i += lineLength;
            }

            return formatted;
        }
    }
}
