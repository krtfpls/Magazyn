using System.Security.Claims;
using Entities.interfaces;

namespace API.Services
{
    public class Security
    {
         public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public string GetUserId(){
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        //    public string GetEmail()
        // {
        //     return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        // }
    }
    }
}