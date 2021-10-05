using System.Collections.Generic;
using System.IO;
using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Utils.Extensions;
using KMChartsUpdater.DAL.Entities;
using Newtonsoft.Json;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace KMChartsUpdater.BLL.Infrastructure
{
    public class PdfReport
    {
        private readonly PdfDocument _document = new PdfDocument();

        private readonly Report _report;

        public PdfReport(Report report)
        {
            _report = report;
        }

        public void CreateAndSave(string filename)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path = currentDirectory + "/Uploads/Reports/" + filename;

            const double padding = 20;
            const double fontSize = 14;
            const double lineHeight = 20;
            const double coverHeight = 50;

            var gfx = CreateNewPage();

            var regularFont = new XFont("OpenSans", fontSize, XFontStyle.Regular);
            var blackLine = new XPen(XColors.Black);

            double leftColumnX = padding + 65;
            double rightColumnX = gfx.PdfPage.Width * 0.6;

            double rightColumnWidth = gfx.PdfPage.Width - rightColumnX;

            //int maxLength = (int)(rightColumnWidth / fontSize);

            double maxY = gfx.PdfPage.Height - 110;

            double y = 110;
            double linkY = gfx.PdfPage.Height - y;

            var playlists = GetReportPlaylists(_report);

            foreach (var playlist in playlists)
            {
                //double stringWidth = playlist.Link.Length * fontSize;
                //double linesCount = Math.Ceiling(stringWidth / rightColumnWidth);
                //double linkHeight = linesCount * lineHeight;

                double height = coverHeight;

                if (y + height + 5 > maxY)
                {
                    gfx = CreateNewPage();
                    y = 110;
                    linkY = gfx.PdfPage.Height - y;
                }

                gfx.DrawLine(blackLine, padding, y, gfx.PdfPage.Width - padding, y);

                y += 5;
                linkY -= 5;

                if (playlist.Cover != null)
                {
                    string coverFullPath = currentDirectory + playlist.Cover;
                    XImage image = XImage.FromFile(coverFullPath);
                    gfx.DrawImage(image, padding, y, coverHeight, coverHeight);
                }

                double textY = y + coverHeight / 2;
                gfx.DrawString(playlist.Name + " (#" + playlist.Position + ")", regularFont, XBrushes.Black, leftColumnX, textY);

                //string linkText = AddLineBreaks(playlist.Link, maxLength);
                gfx.DrawLink("Перейти в плейлист", playlist.Link, rightColumnX, textY - fontSize, lineHeight, rightColumnWidth);

                linkY -= height - fontSize;

                var linkRect = new XRect(rightColumnX, linkY, rightColumnWidth, lineHeight);
                gfx.PdfPage.AddWebLink(new PdfRectangle(linkRect), playlist.Link);

                y += height + 5;
                linkY -= 5 + fontSize;
            }

            _document.Save(path);
        }

        private XGraphics CreateNewPage()
        {
            var page = _document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            DrawTitle(gfx);
            DrawFooter(gfx);

            return gfx;
        }

        private List<PlaylistDto> GetReportPlaylists(Report report)
        {
            var playlists = JsonConvert.DeserializeObject<List<PlaylistDto>>(report.Playlists);

            return playlists;
        }

        private void DrawTitle(XGraphics gfx)
        {
            var font = new XFont("OpenSans", 18, XFontStyle.Bold);

            string title = _report.AudioTask.Audio.Artist + " - " + _report.AudioTask.Audio.Title;

            gfx.DrawString(title, font, XBrushes.Black, new XRect(0, 30, gfx.PdfPage.Width, gfx.PdfPage.Height),
                XStringFormats.TopCenter);

            gfx.DrawString(_report.Name, font, XBrushes.Black, new XRect(0, 55, gfx.PdfPage.Width, gfx.PdfPage.Height),
                XStringFormats.TopCenter);
        }

        private void DrawFooter(XGraphics gfx)
        {
            string logoPath = Directory.GetCurrentDirectory() + "/Uploads/Static/logo-king.png";

            double logoHeight = 70;
            double logoWidth = 60;

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
