using ImageViewer.Extensions;
using ImageViewer.Extensions.Helpers;
using ImageViewer.Extensions.Providers;
using ImageViewer.Extensions.Results;
using ImageViewer.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImageViewer.Controllers
{
    [RoutePrefix("imageviewer-api/file-processor")]
    public class FileProcessorController : ApiController
    {
        [Route("process-image")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPdf()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                    return BadRequest("Unsupported media type.");

                var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());
                if (!provider.Files.Any())
                    return BadRequest("You didn't upload any image.");

                var originalFile = provider.Files[0];
                if (!originalFile.Headers.ContentType.ToString().StartsWith("image"))
                    return BadRequest("You must upload an image.");

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(Constants.TEMP_SAVE_DIRECTORY)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(Constants.TEMP_SAVE_DIRECTORY));

                var originalFileName = string.Join(string.Empty, originalFile.Headers.ContentDisposition.FileName.Split(Path.GetInvalidFileNameChars()));
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(originalFileName);

                var pdfPath = $"{Constants.TEMP_SAVE_DIRECTORY}/{fileName}.pdf";
                using (var inputStream = await originalFile.ReadAsStreamAsync())
                {
                    ImageResizer.GetImageSize(inputStream, out int width, out int height);
                    if (width > 700 || height > 700)
                    {
                        var resizedImageStream = ImageResizer.ResizeImage(inputStream, 600, 100L);
                        PdfHelper.SaveImageAsPdf(resizedImageStream, pdfPath);
                        if (resizedImageStream != null)
                            resizedImageStream.Dispose();
                    }
                    else
                    {
                        PdfHelper.SaveImageAsPdf(inputStream, pdfPath);
                    }

                    // Save original file
                    using (var image = Image.FromStream(inputStream))
                        image.Save(HttpContext.Current.Server.MapPath($"{Constants.TEMP_SAVE_DIRECTORY}/{fileName}{extension}"));
                }

                return Ok(new { url = $"{Constants.VIRTUAL_SAVE_DIRECTORY}/{fileName}.pdf", fileName = $"{fileName}.pdf", folderPath = fileName });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("download")]
        [HttpGet]
        public IHttpActionResult Download(string path)
        {
            var downloadFileInfo = new DownloadFileViewModel(path);
            return new FileActionResult(downloadFileInfo);
        }
    }
}
