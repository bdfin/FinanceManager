using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManager.Helpers
{
    public static class UserHelpers
    {
        public static string GetUserName(AuthenticationState authState)
        {
            return authState.User.Claims.FirstOrDefault(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;
        }

        public static string GetUserId(AuthenticationState authState)
        {
            return authState.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
        }
    }
}
