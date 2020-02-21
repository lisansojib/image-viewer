var pdfDoc = null,
    pageNum = 1,
    pageRendering = false,
    pageNumPending = null,
    scale = 0.8,
    canvas = document.getElementById('the-canvas'),
    ctx = canvas.getContext('2d');

$(function () {
    loadPdfViewer("./scripts/pdfjs-dist/example.pdf");

    $("#btnOpenFile").click(function (e) {
        e.preventDefault();
        $("#openFile").trigger("click");
    });

    $("#openFile").on('change', function (evt) {
        debugger;
        evt.preventDefault();
        var file = this.files[0];

        if (file && file.type.includes("image")) {
            processImageFiles(file);
        }
        else if (URL.createObjectURL) {
            let url = URL.createObjectURL(file);
            if (file.name) {
                url = { url, originalUrl: file.name };
            }
            loadPdfViewer(url);
        } else {
            //PDFViewerApplication.setTitleUsingUrl(file.name);
            //// Read the local file into a Uint8Array.
            //var fileReader = new FileReader();
            //fileReader.onload = function webViewerChangeFileReaderOnload(evt) {
            //    const buffer = evt.target.result;
            //    PDFViewerApplication.open(new Uint8Array(buffer));
            //};
            //fileReader.readAsArrayBuffer(file);
        }
    });
})

/**
 * Get page info from document, resize canvas accordingly, and render page.
 * @param num Page number.
 */
function renderPage(num) {
    pageRendering = true;
    // Using promise to fetch the page
    pdfDoc.getPage(num).then(function (page) {
        var viewport = page.getViewport({ scale: scale, });
        canvas.height = viewport.height;
        canvas.width = viewport.width;

        // Render PDF page into canvas context
        var renderContext = {
            canvasContext: ctx,
            viewport: viewport,
        };
        var renderTask = page.render(renderContext);

        // Wait for rendering to finish
        renderTask.promise.then(function () {
            pageRendering = false;
            if (pageNumPending !== null) {
                // New page rendering is pending
                renderPage(pageNumPending);
                pageNumPending = null;
            }
        });
    });

    // Update page counters
    document.getElementById('page_num').textContent = num;
}

/**
 * If another page rendering in progress, waits until the rendering is
 * finised. Otherwise, executes rendering immediately.
 */
function queueRenderPage(num) {
    if (pageRendering) {
        pageNumPending = num;
    } else {
        renderPage(num);
    }
}

/**
 * Displays previous page.
 */
function onPrevPage() {
    if (pageNum <= 1) {
        return;
    }
    pageNum--;
    queueRenderPage(pageNum);
}
document.getElementById('prev').addEventListener('click', onPrevPage);

/**
 * Displays next page.
 */
function onNextPage() {
    if (pageNum >= pdfDoc.numPages) {
        return;
    }
    pageNum++;
    queueRenderPage(pageNum);
}
document.getElementById('next').addEventListener('click', onNextPage);

function loadPdfViewer(url) {
    /**
     * Asynchronously downloads PDF.
     */
    var loadingTask = pdfjsLib.getDocument(url);
    loadingTask.promise.then(function (pdfDoc_) {
        pdfDoc = pdfDoc_;
        document.getElementById('page_count').textContent = pdfDoc.numPages;

        // Initial/first page rendering
        renderPage(pageNum);
    });
}

/**
 * Process if uploaded file is image
 * @param {Image} - Uploaded Image 
 */
function processImageFiles(file) {
    const uri = "/imageviewer-api/file-processor/process-image";
    const xhr = new XMLHttpRequest();
    const fd = new FormData();

    xhr.open("POST", uri, true);
    xhr.responseType = "json";
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            let baseUrl = window.location.origin + '/' + window.location.pathname.split('/')[1];
            let url = { url: baseUrl + xhr.response.url, originalUrl: xhr.response.fileName };
            loadPdfViewer(url);
        }
    };
    fd.append('UploadImage', file);
    // Initiate a multipart/form-data upload
    xhr.send(fd);
}