using Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Util;

namespace TravelAgency.Helper
{
    public interface IJwtUserInfo
    {
        long GetUserId();
    }
    public class JwtUserInfo : IJwtUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtUserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return long.TryParse(userId, out var id) ? id : throw new CustomValidationException("Invalid token.");
        }
    }
}
