using System.Configuration;

namespace ImageViewer
{
    public static class Constants
    {
        public static string TEMP_DIRECTORY => ConfigurationManager.AppSettings["TempDirectory"];
    }

    public static class EmbededResource
    {
        public const string IndexJs = "ImageViewer.Scripts.index.js";
        public const string RequireJs = "ImageViewer.Scripts.require.js";
        public const string PdfJs = "ImageViewer.Scripts.pdfjs.build.pdf.js";
        public const string ViewerJsSrc = "ImageViewer.Scripts.pdfjs.web.viewer.js";
    }

    public static class EmbededResourceType
    {
        public const string TextOrJs = "text/js";
    }
}
