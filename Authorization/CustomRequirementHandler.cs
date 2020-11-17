using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RelibreApi.Services;
using RelibreApi.Utils;

namespace RelibreApi
{
    public class CustomRequirementHandler : AuthorizationHandler<CustomRequirement>
    {
        private readonly HttpContext _httpContext;
        private readonly IUser _userMananger;

        public CustomRequirementHandler(
                IHttpContextAccessor httpContextAccessor,
                IUser userMananger
            )
        {
            _httpContext = httpContextAccessor.HttpContext;
            _userMananger = userMananger;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomRequirement requirement)
        {
            // captura login de usuario logado
            var login = Util
                .GetClaim(_httpContext,
                    Constants.UserClaimIdentifier);

            // busca dados do usuario logado
            var user = _userMananger
                .GetByLogin(login).Result;

            if (user.Profile.Name.Trim().ToLower().Equals(
                    requirement.RequiredPermission.ToLower()))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}