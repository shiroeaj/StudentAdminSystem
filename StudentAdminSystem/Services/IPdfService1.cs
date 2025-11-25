using System.Threading.Tasks;
using StudentAdminSystem.Models;

namespace StudentAdminSystem.Services
{
    // Fixes the interface mismatch errors by updating return types
    public interface IPdfService1
    {
        // Must return the byte array of the generated PDF
        Task<byte[]> GenerateReceiptPdfAsync(Receipt receipt);

        // Must return the string path to the saved PDF file
        // NOTE: The implementation method was renamed to SaveReceiptPdfToDiskAsync
        // To match the new method name in the implementation, we should update this interface method name.
        Task<string> SaveReceiptPdfToDiskAsync(Receipt receipt);
    }
}