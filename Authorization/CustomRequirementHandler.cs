using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace RelibreApi
{
    public class CustomRequirementHandler : AuthorizationHandler<CustomRequirement>
    {
        private readonly HttpContext _httpContext;

        public CustomRequirementHandler(
                IHttpContextAccessor httpContextAccessor
                )
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        private string GetClaim(string value) =>
            _httpContext.User.Claims.First(x => x.Type.Equals(value)).Value;

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomRequirement requirement)
        {
            

            return Task.CompletedTask;
        }
    }
}