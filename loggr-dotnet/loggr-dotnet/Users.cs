using System;
using System.Collections.Generic;
using System.Text;

namespace Loggr
{
    public static class Users
    {
        /// <summary>
        /// Tracks a user's activity
        /// </summary>
        /// <param name="username">Username of user to track</param>
        public static void TrackUser(string username)
        {
            TrackUser(username, "", "");
        }

        /// <summary>
        /// Tracks a user's activity
        /// </summary>
        /// <param name="username">Username of user to track</param>
        /// <param name="email">Email address of user to track</param>
        public static void TrackUser(string username, string email)
        {
            TrackUser(username, email, "");
        }

        /// <summary>
        /// Tracks a user's activity
        /// </summary>
        /// <param name="username">Username of user to track</param>
        /// <param name="email">Email address of user to track</param>
        /// <param name="page">Page being viewed by user</param>
        public static void TrackUser(string username, string email, string page)
        {
            TrackUser(username, email, page, true);
        }

        /// <summary>
        /// Tracks a user's activity
        /// </summary>
        /// <param name="username">Username of user to track</param>
        /// <param name="email">Email address of user to track</param>
        /// <param name="page">Page being viewed by user</param>
        /// <param name="async">A bool that specifies how user tracking should be sent to Loggr. Typically an application will post asynchronously for best performance, but sometimes it needs to be posted synchronously if the application needs to block until the post has completed</param>
        public static void TrackUser(string username, string email, string page, bool async)
        {
            LogClient client = new LogClient();
            client.TrackUser(username, email, page, async);
        }
    }
}
