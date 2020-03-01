using System.Web.Mvc;
using System.Web;
using System;
using System.Web.UI;

namespace ImageViewer.Controllers
{
    public class DocumentViewerController : Controller
    {
        public DocumentViewerController()
        {
        }

        public PartialViewResult Index(string filePath, int width = 1000, int height = 800)
        {
            ViewBag.Width = width;
            ViewBag.Height = height;
            ViewBag.Framesource = string.Format("{0}{1}Scripts/pdf.js/web/viewer.html?file={0}{1}", BaseUrl, filePath);
            return PartialView("_Index");
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
