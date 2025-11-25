using Microsoft.AspNetCore.Http;

namespace StudentAdminSystem.Helpers
{
    public static class SessionHelper
    {
        private const string UserIdKey = "UserId";
        private const string EmailKey = "Email";
        private const string IsAdminKey = "IsAdmin";
        private const string FullNameKey = "FullName";

        public static void SetStudentSession(ISession session, int studentId, string email, string fullName)
        {
            session.SetInt32(UserIdKey, studentId);
            session.SetString(EmailKey, email);
            session.SetString(FullNameKey, fullName);
            session.SetString(IsAdminKey, "false");
        }

        public static void SetAdminSession(ISession session, int adminId, string email, string adminName)
        {
            session.SetInt32(UserIdKey, adminId);
            session.SetString(EmailKey, email);
            session.SetString(FullNameKey, adminName);
            session.SetString(IsAdminKey, "true");
        }

        public static int? GetUserId(ISession session)
        {
            return session.GetInt32(UserIdKey);
        }

        public static string GetEmail(ISession session)
        {
            return session.GetString(EmailKey);
        }

        public static string GetFullName(ISession session)
        {
            return session.GetString(FullNameKey);
        }

        public static bool IsLoggedIn(ISession session)
        {
            return session.GetInt32(UserIdKey).HasValue;
        }

        public static bool IsAdmin(ISession session)
        {
            var isAdmin = session.GetString(IsAdminKey);
            return isAdmin == "true";
        }

        public static void ClearSession(ISession session)
        {
            session.Clear();
        }
    }
}