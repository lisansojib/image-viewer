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
            EnableFindButton = true;
            IsMinified = true;
            IsRemote = true;
            EnableOpenButton = true;
            EnablePrintButton = true;
            EnableDownloadButton = true;
            EnableRotateButton = true;
            EnableFindButton = true;
            EnableScaleSelectCombo = true;
            EnableZoomInZoomOutButton = true;
            EnableDisplayThumbnailList = true;
            EnableTextSelectionTool = true;
            ShowDocumentProperties = true;
        }

        #region Properties
        [Category("Source File")]
        [Browsable(true)]
        [Description("Set path to source file.")]
        [UrlProperty, Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string FilePath { get; set; }

        [Category("View Scale")]
        [Browsable(true)]
        [Description("Pdf Viewer scale on brower (i.e 1.25 for 125% zoom)")]
        public string ScaleSelect { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable Fullscreen button.")]
        public bool EnableFullScreenButton { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable Open button.")]
        public bool EnableOpenButton { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable Print button.")]
        public bool EnablePrintButton { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable Download button.")]
        public bool EnableDownloadButton { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable Rotate button.")]
        public bool EnableRotateButton { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable Find button.")]
        public bool EnableFindButton { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable View Scale Combo.")]
        public bool EnableScaleSelectCombo { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable Zoom In and Zoom Out button.")]
        public bool EnableZoomInZoomOutButton { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable Display Thumbnail List.")]
        public bool EnableDisplayThumbnailList { get; set; }

        [Category("Enable/Disable button")]
        [Browsable(true)]
        [Description("Enable/Disable Enable Text Selection Tool.")]
        public bool EnableTextSelectionTool { get; set; }

        [Category("Show/Hide button")]
        [Browsable(true)]
        [Description("Show/Hide Show Document Properties Tool.")]
        public bool ShowDocumentProperties { get; set; }

        [Category("Viewer Source")]
        [Browsable(true)]
        [Description("Set if PDF viwer is remote or locally hosted.")]
        public bool IsRemote { get; set; }

        [Category("Minification")]
        [Browsable(true)]
        [Description("Set if PDF viwer files is minified or not.")]
        public bool IsMinified { get; set; }
        #endregion

        #region Methods
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
                var frameSource = $"{appDomain}{appRootUrl}Scripts/pdf.js/web/viewer.html";

                var fileUrl = HttpUtility.UrlEncode($"{appDomain}{fileVirtualPath}");
                if (!string.IsNullOrEmpty(fileUrl))
                    frameSource += $"?file={fileUrl}";

                var baseUrl = HttpUtility.UrlEncode($"{appDomain}");
                frameSource += $"&baseUrl={baseUrl}";

                if (!string.IsNullOrEmpty(ScaleSelect))
                    frameSource += $"&scaleSelect={ScaleSelect}";

                frameSource += $"&showFullScreenButton={EnableFullScreenButton}";
                frameSource += $"&showopenbutton={EnableOpenButton}";
                frameSource += $"&showprintbutton={EnablePrintButton}";
                frameSource += $"&showdownloadbutton={EnableDownloadButton}";
                frameSource += $"&showrotatebuttons={EnableRotateButton}";
                frameSource += $"&showfindbutton={EnableFindButton}";
                frameSource += $"&showscaleSelect={EnableScaleSelectCombo}";
                frameSource += $"&showzoominzoomoutbuttons={EnableZoomInZoomOutButton}";
                frameSource += $"&showthumbnaillist={EnableDisplayThumbnailList}";
                frameSource += $"&showoutlinebutton={EnableTextSelectionTool}";
                frameSource += $"&showdocumentpropertiesbutton={ShowDocumentProperties}";

                //var indexJsSrc = HttpUtility.UrlEncode(Page.ClientScript.GetWebResourceUrl(this.GetType(), EmbededResource.IndexJs));
                //var pdfJsSrc = HttpUtility.UrlEncode(Page.ClientScript.GetWebResourceUrl(this.GetType(), EmbededResource.PdfJs));
                //var viewerJsSrc = HttpUtility.UrlEncode(Page.ClientScript.GetWebResourceUrl(this.GetType(), EmbededResource.ViewerJsSrc));

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

                if(!string.IsNullOrEmpty(ScaleSelect))
                    frameSource += $"&scaleSelect={ScaleSelect}";

                frameSource += $"&showFullScreenButton={EnableFullScreenButton}";
                frameSource += $"&showopenbutton={EnableOpenButton}";
                frameSource += $"&showprintbutton={EnablePrintButton}";
                frameSource += $"&showdownloadbutton={EnableDownloadButton}";
                frameSource += $"&showrotatebuttons={EnableRotateButton}";
                frameSource += $"&showfindbutton={EnableFindButton}";
                frameSource += $"&showscaleSelect={EnableScaleSelectCombo}";
                frameSource += $"&showzoominzoomoutbuttons={EnableZoomInZoomOutButton}";
                frameSource += $"&showthumbnaillist={EnableDisplayThumbnailList}";
                frameSource += $"&showoutlinebutton={EnableTextSelectionTool}";
                frameSource += $"&showdocumentpropertiesbutton={ShowDocumentProperties}";

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
        #endregion
    }
}
