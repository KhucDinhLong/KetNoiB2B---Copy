using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SETA.Core.Helper.Session;

namespace SETA.Core.Session
{
    /// <summary>
    /// Class Session
    /// </summary>
    [Serializable]
    public class SETASession
    {
        public static readonly string user = "user";

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>Entity.Admin.User.</returns>
        public static UserSession GetUser()
        {

            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null &&
                HttpContext.Current.Session.Count > 0 &&
                HttpContext.Current.Session[SETASession.user] != null)
            {
                return HttpContext.Current.Session[SETASession.user] as UserSession;
            }
            //TODO: test data
            return new UserSession();
        }

        /// <summary>
        /// Sets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool SetUser(UserSession user)
        {
            HttpContext.Current.Session.Remove(SETASession.user);
            user.SessionId = HttpContext.Current.Session.SessionID;
            HttpContext.Current.Session.Add(SETASession.user, user);
            return true;
        }


        /// <summary>
        /// Clears the session.
        /// </summary>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}
