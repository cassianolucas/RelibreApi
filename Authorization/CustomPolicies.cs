using Microsoft.AspNetCore.Authorization;

namespace RelibreApi
{
    public class CustomPolicies : AuthorizationOptions
    {
        public CustomPolicies(AuthorizationOptions x)
        {
            x.AddPolicy("PJ", p =>
                p.Requirements.Add(new CustomRequirement("PJ")));
        }
    }
}