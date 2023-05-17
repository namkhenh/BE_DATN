using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Portal.Application.Interfaces;
using Portal.Application.Interfaces.Permission;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Portal.Infrastructure
{
    public class Root
    {
        protected const string Module = "Portal";
        protected const string Rpc = "rpc/";
        protected const string Rest = "rest/";
    }
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement()
        {

        }
    }
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentContext _currentContext;
        public PermissionHandler(ICurrentContext currentContext, IHttpContextAccessor httpContextAccessor, IPermissionService permissionService)
        {
            _permissionService = permissionService;
            _currentContext = currentContext;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var HttpContext = _httpContextAccessor.HttpContext;
            var token = HttpContext.Request.Cookies["Token"];
            if (token != null && token != "")
            {
                _currentContext.Token = token;
                string url = HttpContext.Request.Path.Value.ToLower().Substring(1);
                var urls = url.Split("rpc");
                url = "rpc" + urls[1];
                var userId = GetUserIdFromToken(HttpContext.Request.Cookies["Token"]);
                if (_currentContext.Token != null && userId != null)
                {
                    var result = await _permissionService.CheckPermissionByUserIdAndUrl(userId, url);
                    if (result == true)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
                else
                {
                    context.Fail();
                }

            }
            else
            {
                context.Fail();
            }

        }
       
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        public string GetUserIdFromToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
            else
            {
                return null;
            }
        }

    }

}
