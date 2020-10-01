using System.Security.Claims;
using System.Threading.Tasks;
using TomLonghurst.AzureAD.TokenValidator.V1;
using TomLonghurst.AzureAD.TokenValidator.V2;

namespace TomLonghurst.AzureAD.TokenValidator
{
    public class AADTokenValidator : IAADTokenValidator
    {
        private readonly IAADTokenV1Validator _aadTokenV1Validator;
        private readonly IAADTokenV2Validator _aadTokenV2Validator;

        public AADTokenValidator(IAADTokenV1Validator aadTokenV1Validator,
            IAADTokenV2Validator aadTokenV2Validator)
        {
            _aadTokenV1Validator = aadTokenV1Validator;
            _aadTokenV2Validator = aadTokenV2Validator;
        }
        
        public async Task<ClaimsPrincipal> ValidateTokenAsync(string authorizationHeader)
        {
            return await _aadTokenV1Validator.ValidateTokenAsync(authorizationHeader) ?? await _aadTokenV2Validator.ValidateTokenAsync(authorizationHeader);
        }
    }
}