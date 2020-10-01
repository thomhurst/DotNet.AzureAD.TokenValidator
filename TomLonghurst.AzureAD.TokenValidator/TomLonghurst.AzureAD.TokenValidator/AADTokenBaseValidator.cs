using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using TomLonghurst.AzureAD.TokenValidator.Extensions;

namespace TomLonghurst.AzureAD.TokenValidator
{
    public abstract class AADTokenBaseValidator
    {
        private readonly IAADSettings _aadSettings;

        private readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

        public AADTokenBaseValidator(IAADSettings aadSettings, string wellKnownOpenIdConfigurationPath)
        {
            _aadSettings = aadSettings;
            
            var wellKnownEndpoint = $"{aadSettings.Instance}{aadSettings.TenantId}{wellKnownOpenIdConfigurationPath}";
            
            _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                wellKnownEndpoint, new OpenIdConnectConfigurationRetriever());
        }

        public async Task<ClaimsPrincipal> ValidateTokenAsync(string authorizationHeader)
        {
            var accessToken = GetAccessToken(authorizationHeader);
            
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return null;
            }

            var openIdConfiguration = await GetWellKnownConfiguration();

            var tokenValidator = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                RequireSignedTokens = true,
                ValidAudience = _aadSettings.Audience,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKeys = openIdConfiguration.SigningKeys,
                ValidIssuer = openIdConfiguration.Issuer
            };

            try
            {
                return tokenValidator.ValidateToken(accessToken, validationParameters, out _);
            }
            catch (SecurityTokenValidationException)
            {
                return null;
            }
        }

        private static string GetAccessToken(string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return null;
            }

            if (!authorizationHeader.Contains("Bearer ", StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return authorizationHeader.Substring("Bearer ".Length);
        }

        private Task<OpenIdConnectConfiguration> GetWellKnownConfiguration()
        {
            return _configurationManager.GetConfigurationAsync();
        }
    }
}