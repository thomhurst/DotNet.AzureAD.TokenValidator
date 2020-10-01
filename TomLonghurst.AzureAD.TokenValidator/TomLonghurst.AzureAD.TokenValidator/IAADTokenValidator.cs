using System.Security.Claims;
using System.Threading.Tasks;

namespace TomLonghurst.AzureAD.TokenValidator
{
    public interface IAADTokenValidator
    {
        Task<ClaimsPrincipal> ValidateTokenAsync(string authorizationHeader);
    }
}