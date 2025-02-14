using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using HRMS.Application.Configurations;
using HRMS.Application.Interfaces.Services.Identity;
using HRMS.Domain.Entities.Identity;
using HRMS.Shared.Wrapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using HRMS.Shared.Utilities.Responses.Identity;
using HRMS.Shared.Utilities.Requests.Identity;

namespace HRMS.Infrastructure.Services.Identity
{
    public class IdentityService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppConfiguration _appConfig;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<IdentityService> _localizer;
        private readonly ILogger<IdentityService> _logger;
        public IdentityService(
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            IOptions<AppConfiguration> appConfig, SignInManager<ApplicationUser> signInManager,
            IStringLocalizer<IdentityService> localizer, ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appConfig = appConfig.Value;
            _signInManager = signInManager;
            _localizer = localizer;
            _logger = logger;
        }

        public async Task<Result<TokenResponse>> LoginAsync(TokenRequest model)
        {
            _logger.LogInformation("Login Process Started for {user}.", model.UserName);
            ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                _logger.LogError("Login Failed due to User {user} Not Found.", model.UserName);
                return await Result<TokenResponse>.FailAsync(_localizer["User Not Found."]);
            }
            if (!user.IsActive)
            {
                _logger.LogError("User {user} Not Active. Please contact the administrator.", model.UserName);
                return await Result<TokenResponse>.FailAsync(_localizer["User Not Active. Please contact the administrator."]);
            }
            if (!user.EmailConfirmed)
            {
                _logger.LogError("E-Mail not confirmed.");
                return await Result<TokenResponse>.FailAsync(_localizer["E-Mail not confirmed."]);
            }
            SignInResult res = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, true);
            if (res.Succeeded)
            {
                _logger.LogInformation("User Login Successed for {user}.", model.UserName);
                user.RefreshToken = GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                _ = await _userManager.UpdateAsync(user);
                _logger.LogInformation("Generating Token Started.");
                string token = await GenerateJwtAsync(user);
                _logger.LogInformation("Token Generation Successfully. Token : {token}.", token);
                string img = "";
                if (!string.IsNullOrWhiteSpace(user.ProfilePictureDataUrl))
                {
                    byte[] bytes = await File.ReadAllBytesAsync(user.ProfilePictureDataUrl!);
                    img = Convert.ToBase64String(bytes);
                }
                TokenResponse response = new() { Token = token, RefreshToken = user.RefreshToken, UserImageURL = img };
                _logger.LogInformation("Token Response : {response}.", Newtonsoft.Json.JsonConvert.SerializeObject(response));
                return await Result<TokenResponse>.SuccessAsync(response);
            }
            else
            {

                if (res.IsLockedOut)
                {
                    _logger.LogError("User Account Locked.");
                    return await Result<TokenResponse>.FailAsync(_localizer["User Account Locked."]);
                }
                else
                {
                    _logger.LogError("Invalid Credentials.");
                    return await Result<TokenResponse>.FailAsync(_localizer["Invalid Credentials."]);
                }

            }


        }

        public async Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model)
        {
            _logger.LogInformation($"RefreshToken request Started.");
            if (model is null)
            {
                _logger.LogError("Invalid Client Token.");
                return await Result<TokenResponse>.FailAsync(_localizer["Invalid Client Token."]);
            }
            ClaimsPrincipal userPrincipal = GetPrincipalFromExpiredToken(model.Token);
            string userEmail = userPrincipal.FindFirstValue(ClaimTypes.Email);
            ApplicationUser user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                _logger.LogError("Invalid Client Token.");
                return await Result<TokenResponse>.FailAsync(_localizer["User Not Found."]);
            }

            if (user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _logger.LogError("Invalid Client Token.");
                return await Result<TokenResponse>.FailAsync(_localizer["Invalid Client Token."]);
            }
            _logger.LogInformation("Token Generation Started");
            string token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));
            user.RefreshToken = GenerateRefreshToken();
            _ = await _userManager.UpdateAsync(user);

            TokenResponse response = new() { Token = token, RefreshToken = user.RefreshToken, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime ?? DateTime.UtcNow };
            _logger.LogInformation("Refresh Token Response {response}", Newtonsoft.Json.JsonConvert.SerializeObject(response));
            return await Result<TokenResponse>.SuccessAsync(response);
        }

        private async Task<string> GenerateJwtAsync(ApplicationUser user)
        {
            string token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));
            return token;
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim> roleClaims = new();
            List<Claim> permissionClaims = new();
            foreach (string? role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
                ApplicationRole thisRole = await _roleManager.FindByNameAsync(role);
                IList<Claim> allPermissionsForThisRoles = await _roleManager.GetClaimsAsync(thisRole);
                permissionClaims.AddRange(allPermissionsForThisRoles);
            }

            IEnumerable<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Surname, $"{user.Name}"),
                new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
                new(ClaimTypes.Uri,user.ProfilePictureDataUrl ?? string.Empty)
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);

            return claims;
        }

        private string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            JwtSecurityToken token = new(
               claims: claims,
               expires: DateTime.UtcNow.AddDays(2),
               signingCredentials: signingCredentials);
            JwtSecurityTokenHandler tokenHandler = new();
            string encryptedToken = tokenHandler.WriteToken(token);
            return encryptedToken;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false
            };
            JwtSecurityTokenHandler tokenHandler = new();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken? securityToken);
            return securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase)
                ? throw new SecurityTokenException(_localizer["Invalid token"])
                : principal;
        }

        private SigningCredentials GetSigningCredentials()
        {
            byte[] secret = Encoding.UTF8.GetBytes(string.IsNullOrWhiteSpace(_appConfig.Secret) ? "xCGv6yqV+&EpVTXtd2S!@&9zprR28AJ$C+vSFz9+yj0kp@fT*DQCw7Q=&P30Xm4!o@eeo!8XZHrO$9xCrteB%t1MeOV2raxZQm8%" : _appConfig.Secret);
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha512);
        }
    }
}
