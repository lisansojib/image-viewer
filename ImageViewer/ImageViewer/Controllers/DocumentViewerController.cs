using System.Web.Mvc;
using System.Web;

namespace ImageViewer.Controllers
{
    public class DocumentViewerController : Controller
    {
        public DocumentViewerController()
        {
        }

        public PartialViewResult Index(string FilePath, int Width = 1000, int Height = 800)
        {
            ViewBag.Width = Width;
            ViewBag.Height = Height;
            ViewBag.Framesource = string.Format("{0}{1}Scripts/pdf.js/web/viewer.html?file={0}{1}", BaseUrl, FilePath);
            //var content = $"<iframe src=\"{frameSource}\" width=\"{Width}\" height=\"{Height}\"></iframe>";
            return PartialView("~/Views/DocumentViewer/_Index.cshtml");
        }

        #region Helpers
        private string _baseUrl;
        public string BaseUrl
        {
            get
            {
                var request = HttpContext.Request;
                var appUrl = HttpRuntime.AppDomainAppVirtualPath;

                if (appUrl != "/")
                    appUrl = "/" + appUrl;

                _baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

                return _baseUrl;
            }
            set
            {
                _baseUrl = value;
            }
        }
        #endregion
    }
}
