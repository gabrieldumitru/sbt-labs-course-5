using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using sbt_labs_course_5.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Helper.Auth
{
    public static class SessionTokenValidation
    {
        public static async Task ValidateSessionToken(TokenValidatedContext context)
        {
            var repository = context.HttpContext.RequestServices.GetRequiredService<IRepositoryWrapper>();
            if (context.Principal.HasClaim(c => c.Type.Equals(JwtRegisteredClaimNames.Jti, StringComparison.OrdinalIgnoreCase)))
            {
                var jti = context.Principal.Claims.FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Jti, StringComparison.OrdinalIgnoreCase)).Value;
                // Check jti is still in store, and that the expiration of the jti is ok
                var tokenInStore = await repository.SessionToken.GetByJTI(jti);
                if (tokenInStore != null && tokenInStore.ExpirationDate > DateTime.UtcNow)
                {
                    return;
                }
            }

            context.Fail("");
        }
    }
}
