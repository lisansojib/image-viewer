using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace ImageViewer
{
    [DefaultProperty("FilePath")]
    [ToolboxData("<{0}:DocumentViewer runat=server></{0}:DocumentViewer>")]
    public class DocumentViewer : WebControl
    {
        public DocumentViewer()
        {
            IsMinified = true;
        }

        private string filePath;

        [Category("Source File")]
        [Browsable(true)]
        [Description("Set path to source file.")]
        [UrlProperty, Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = string.IsNullOrEmpty(value) ? string.Empty : value;
            }
        }

        [Category("PDF viewer mode")]
        [Browsable(true)]
        [Description("Set if PDF viwer is remote or locally hosted.")]
        [UrlProperty, Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public bool IsRemote { get; set; }

        [Category("PDF viewer file minification")]
        [Browsable(true)]
        [Description("Set if PDF viwer files is minified or not.")]
        [UrlProperty, Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public bool IsMinified { get; set; }

        public override void RenderControl(HtmlTextWriter writer)
        {
            try
            {
                var markup = IsRemote
                    ? BuildControl(ResolveUrl(FilePath), HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)).ToString()
                    : BuildControl(ResolveUrl(FilePath), HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), ResolveUrl("~/")).ToString();

                writer.Write(markup);
            }
            catch
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Cannot display document viewer");
                writer.RenderEndTag();
            }
        }

        public StringBuilder BuildControl(string fileVirtualPath, string appDomain, string appRootUrl)
        {
            try
            {
                var indexJsSrc = HttpUtility.UrlEncode(Page.ClientScript.GetWebResourceUrl(this.GetType(), EmbededResource.IndexJs));
                var pdfJsSrc = HttpUtility.UrlEncode(Page.ClientScript.GetWebResourceUrl(this.GetType(), EmbededResource.PdfJs));
                var viewerJsSrc = HttpUtility.UrlEncode(Page.ClientScript.GetWebResourceUrl(this.GetType(), EmbededResource.ViewerJsSrc));

                var frameSource = string.Format("{0}{1}Scripts/pdf.js/web/viewer.html?file={0}{2}&pdfJsSrc={3}&viewerJsSrc={4}&indexJsSrc={5}"
                    , appDomain, appRootUrl, fileVirtualPath, pdfJsSrc, viewerJsSrc, indexJsSrc);                

                StringBuilder sb = new StringBuilder();
                sb.Append("<iframe ");
                if (!string.IsNullOrEmpty(ID))
                    sb.Append("id=" + ClientID + " ");
                sb.Append("src=" + frameSource + " ");
                sb.Append("width=" + Width.ToString() + " ");
                sb.Append("height=" + Height.ToString() + ">");
                sb.Append("</iframe>");
                return sb;
            }
            catch
            {
                return new StringBuilder("Cannot display document viewer");
            }
        }

        public StringBuilder BuildControl(string fileVirtualPath, string appDomain)
        {
            try
            {
                var folderPath = IsMinified ? "minified" : "generic";
                var frameSource = $"https://lisansojib.github.io/pdfjs-dist/{folderPath}/web/viewer.html";

                var fileUrl = HttpUtility.UrlEncode($"{appDomain}{fileVirtualPath}");
                if (!string.IsNullOrEmpty(fileUrl))
                    frameSource += $"?file={fileUrl}";

                var baseUrl = HttpUtility.UrlEncode($"{appDomain}");
                frameSource += $"&baseUrl={baseUrl}";

                StringBuilder sb = new StringBuilder();
                sb.Append("<iframe ");
                if (!string.IsNullOrEmpty(ID))
                    sb.Append("id=" + ClientID + " ");
                sb.Append("src=" + frameSource + " ");
                sb.Append("width=" + Width.ToString() + " ");
                sb.Append("height=" + Height.ToString() + ">");
                sb.Append("</iframe>");
                return sb;
            }
            catch
            {
                return new StringBuilder("Cannot display document viewer");
            }
        }
    }
}
