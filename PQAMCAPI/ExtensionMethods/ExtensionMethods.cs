using API.Classes;
using Newtonsoft.Json;
using PQAMCAPI.Interfaces;
using PQAMCClasses;
using System.Security.Claims;
using static PQAMCClasses.Globals;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static int GetAccountCategoryId(this IUserContextService userContextService)
        {
            var Context = userContextService.CurrentContext() ?? throw new MyAPIException("Bad request. Invalid Context");

            Claim? UserClaim = Context.User.FindFirst("AccountCategoryID");

            if (UserClaim == null)
                throw new MyAPIException("Invalid Session. Account Category ID not found");
            else
                return int.Parse(UserClaim.Value);
        }

        public static SessionSecurityKeys GetSessionSecurityKeys(this IUserContextService userContextService)
        {
            var Context = userContextService.CurrentContext() ?? throw new MyAPIException("Bad request. Invalid Context");

            Claim? UserClaim = Context.User.FindFirst(SessionKeys.SecurityKeys);

            if (UserClaim == null)
                throw new MyAPIException("Invalid Session. Necessary Keys not found");
            else
                return JsonConvert.DeserializeObject<SessionSecurityKeys>(UserClaim.Value.ToString());
        }
    }
}
