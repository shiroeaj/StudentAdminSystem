using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using StudentAdminSystem.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdminSystem.Services
{
    public class PdfService : IPdfService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<PdfService> _logger;

        // Dependencies are injected via the constructor, which the DI container (Program.cs) handles.
        public PdfService(IWebHostEnvironment webHostEnvironment, ILogger<PdfService> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        /// <summary>
        /// Simulates the generation of a PDF document from a Receipt model, returning the raw byte array.
        /// In a real application, this would use a library (e.g., QuestPDF, iTextSharp) to convert HTML/Razor
        /// templates or a C# document structure into PDF bytes.
        /// </summary>
        /// <param name="receipt">The receipt data to be rendered.</param>
        /// <returns>A byte array representing the PDF file.</returns>
        public Task<byte[]> GenerateReceiptPdfAsync(Receipt receipt)
        {
            try
            {
                // 1. Generate the HTML content (often the first step in PDF generation)
                string htmlContent = GenerateReceiptHtml(receipt);

                _logger.LogInformation($"Successfully generated HTML content for Receipt ID: {receipt.Id}");

                // 2. Simulate PDF creation (A placeholder for the real PDF library call)
                // We'll return a simple byte array representing a minimal PDF header/content.
                // Actual PDF bytes would be much larger and more complex.
                byte[] mockPdfBytes = Encoding.UTF8.GetBytes($"%PDF-1.4\n%Receipt ID: {receipt.Id}\n{htmlContent}");

                return Task.FromResult(mockPdfBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error generating PDF for Receipt ID: {receipt.Id}");
                throw;
            }
        }

        /// <summary>
        /// Generates the PDF content and saves it to the wwwroot/pdfs/ directory.
        /// </summary>
        /// <param name="receipt">The receipt data.</param>
        /// <returns>The full path to the saved PDF file.</returns>
        public async Task<string> SaveReceiptPdfToDiskAsync(Receipt receipt)
        {
            // 1. Get the PDF content bytes
            byte[] pdfBytes = await GenerateReceiptPdfAsync(receipt);

            // 2. Define the path (e.g., wwwroot/pdfs/12345-receipt.pdf)
            string receiptsDir = Path.Combine(_webHostEnvironment.WebRootPath, "pdfs");

            // Ensure the directory exists
            if (!Directory.Exists(receiptsDir))
            {
                Directory.CreateDirectory(receiptsDir);
            }

            string fileName = $"receipt-{receipt.Id}.pdf";
            string filePath = Path.Combine(receiptsDir, fileName);

            // 3. Write the bytes to the file system
            await File.WriteAllBytesAsync(filePath, pdfBytes);

            _logger.LogInformation($"Receipt PDF saved to disk: {filePath}");

            return filePath;
        }

        /// <summary>
        /// Helper method to create a basic HTML structure for the receipt.
        /// </summary>
        private string GenerateReceiptHtml(Receipt receipt)
        {
            // Assuming the Receipt model has properties like Id, DateIssued, and TotalAmount
            // This HTML structure is what a PDF generator would typically consume.
            return $@"
<html>
<head><title>Receipt {receipt.Id}</title>
<style>
    body {{ font-family: Arial, sans-serif; margin: 50px; }}
    h1 {{ color: #333; }}
    .receipt-details {{ border: 1px solid #ccc; padding: 20px; }}
</style>
</head>
<body>
    <h1>Official Receipt</h1>
    <div class='receipt-details'>
        <p><strong>Receipt ID:</strong> {receipt.Id}</p>
        <p><strong>Date Issued:</strong> {receipt.DateIssued:yyyy-MM-dd HH:mm:ss}</p>
        <p><strong>Total Amount:</strong> ${receipt.TotalAmount:N2}</p>
        <p>Thank you for your business!</p>
    </div>
</body>
</html>";
        }
    }
}