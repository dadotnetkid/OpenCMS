
function initWebViewer() {
    const viewerElement = document.getElementById('viewer');
    WebViewer({
        path: 'pdf-viewer/lib',
        initialDoc: 'https://localhost:44380/PdfFileViewer', // replace with your own PDF file
    }, viewerElement).then((instance) => {
        // call apis here
    })
}
function ShowPdfViewer(targetElementId,pdfSource) {
    const viewerElement = document.getElementById(targetElementId);
    WebViewer({
        path: 'pdf-viewer/lib',
        initialDoc: pdfSource, // replace with your own PDF file
    }, viewerElement).then((instance) => {
        // call apis here
    })
}