using StudentAdminSystem.Models;

namespace StudentAdminSystem.Services
{
    public interface IAuthService
    {
        Task RegisterStudentAsync(string fullName, string email, string password, string phoneNumber, string address);
        Task LoginStudentAsync(string email, string password);
        Task LoginAdminAsync(string email, string password);
        Task EmailExistsAsync(string email);
    }
}

