using GlamoraHairdresser.Data.Entities;

namespace GlamoraHairdresser.Services.Auth
{
    public static class SessionManager
    {
        /// <summary>
        /// Holds the currently logged-in user (Admin, Customer, Worker)
        /// </summary>
        public static User? CurrentUser { get; private set; }

        /// <summary>
        /// Sets the logged-in user
        /// </summary>
        public static void SetUser(User user)
        {
            CurrentUser = user;
        }

        /// <summary>
        /// Clears the session on logout
        /// </summary>
        public static void Clear()
        {
            CurrentUser = null;
        }
    }
}
