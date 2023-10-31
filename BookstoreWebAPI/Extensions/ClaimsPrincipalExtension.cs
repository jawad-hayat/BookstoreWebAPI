using System.Linq;
using System.Security.Claims;

namespace BookstoreWebAPI.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string RetriveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}