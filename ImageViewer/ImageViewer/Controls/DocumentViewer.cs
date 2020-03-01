using ImageViewer.Extensions;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
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
        private string filePath;
        private string tempDirectoryPath;

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

        [Category("Temporary Directory Path")]
        [Browsable(true)]
        [Description("Set path to the directory where the files will be converted.")]
        [UrlProperty, Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string TempDirectoryPath
        {
            get
            {
                return string.IsNullOrEmpty(tempDirectoryPath) ? "~/Temp" : tempDirectoryPath;
            }
            set
            {
                tempDirectoryPath = string.IsNullOrEmpty(value) ? string.Empty : value;
            }
        }
        
        public override void RenderControl(HtmlTextWriter writer)
        {
            try
            {
                writer.Write(BuildControl(HttpContext.Current.Server.MapPath(FilePath), ResolveUrl(FilePath), HttpContext.Current.Server.MapPath(TempDirectoryPath),
                    ResolveUrl(TempDirectoryPath), HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), ResolveUrl("~/")).ToString());
            }
            catch
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Cannot display document viewer");
                writer.RenderEndTag();
            }
        }

        public StringBuilder BuildControl(string filePhysicalPath, string fileVirtualPath, string tempDirectoryPhysicalPath,
            string tempDirectoryVirtualPath, string appDomain, string appRootUrl)
        {
            try
            {
                string fileExtension = Path.GetExtension(fileVirtualPath);
                SupportedExtensions extension = (SupportedExtensions)Enum.Parse(typeof(SupportedExtensions), fileExtension.Replace(".", ""));
                
                var frameSource = string.Format("{0}{1}Scripts/pdf.js/web/viewer.html?file={0}{2}", appDomain, appRootUrl, fileVirtualPath);

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
