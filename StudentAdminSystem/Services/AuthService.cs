using StudentAdminSystem.Data;
using StudentAdminSystem.Helpers;
using StudentAdminSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; // Ensure this is included

namespace StudentAdminSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly MySqlDbContext _context;

        public AuthService(MySqlDbContext context)
        {
            _context = context;
        }

        // FIX: Changed return type from 'Task' to 'Task<Student>'
        public async Task<Student> RegisterStudentAsync(string fullName, string email, string password, string phoneNumber, string address)
        {
            var student = new Student
            {
                FullName = fullName,
                Email = email,
                PasswordHash = PasswordHelper.HashPassword(password),
                PhoneNumber = phoneNumber,
                Address = address,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        // FIX: Changed return type from 'Task' to 'Task<Student?>'
        // Using nullable reference type Student? as it returns null on failure.
        public async Task<Student?> LoginStudentAsync(string email, string password)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == email);

            if (student == null)
                return null;

            if (!PasswordHelper.VerifyPassword(password, student.PasswordHash))
                return null;

            student.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();
            return student;
        }

        // FIX: Changed return type from 'Task' to 'Task<Admin?>'
        // Using nullable reference type Admin? as it returns null on failure.
        public async Task<Admin?> LoginAdminAsync(string email, string password)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);

            if (admin == null)
                return null;

            if (!PasswordHelper.VerifyPassword(password, admin.PasswordHash))
                return null;

            admin.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();
            return admin;
        }

        // FIX: Changed return type from 'Task' to 'Task<bool>'
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Students.AnyAsync(s => s.Email == email) ||
                       await _context.Admins.AnyAsync(a => a.Email == email);
        }

        Task IAuthService.RegisterStudentAsync(string fullName, string email, string password, string phoneNumber, string address)
        {
            return RegisterStudentAsync(fullName, email, password, phoneNumber, address);
        }

        Task IAuthService.LoginStudentAsync(string email, string password)
        {
            return LoginStudentAsync(email, password);
        }

        Task IAuthService.LoginAdminAsync(string email, string password)
        {
            return LoginAdminAsync(email, password);
        }

        Task IAuthService.EmailExistsAsync(string email)
        {
            return EmailExistsAsync(email);
        }
    }
}