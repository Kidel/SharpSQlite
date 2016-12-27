using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSQlite.Util
{
    public static class UserSessionManager
    {
        public static void Set(HttpContext httpContext, int userId)
        {
            httpContext.Session.Set(SessionKeys.user, BitConverter.GetBytes(userId));
        }

        public static void Remove(HttpContext httpContext)
        {
            httpContext.Session.Remove(SessionKeys.user);
        }

        public static bool IsLogged(HttpContext httpContext)
        {
            byte[] temp;
            return httpContext.Session.TryGetValue(SessionKeys.user, out temp);
        }

        public static int Get(HttpContext httpContext)
        {
            byte[] userIdBytes;
            httpContext.Session.TryGetValue(SessionKeys.user, out userIdBytes);
            return BitConverter.ToInt32(userIdBytes, 0);
        }
    }
}
