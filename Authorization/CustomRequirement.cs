using Microsoft.AspNetCore.Authorization;

namespace RelibreApi
{
    public class CustomRequirement : IAuthorizationRequirement
    {
        public string RequiredPermission { get; }

        public CustomRequirement(string requiredPermission)
        {
            this.RequiredPermission = requiredPermission;
        }
    }
}