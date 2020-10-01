namespace TomLonghurst.AzureAD.TokenValidator.V2
{
    public class AADTokenV2Validator : AADTokenBaseValidator, IAADTokenV2Validator
    {
        public AADTokenV2Validator(IAADSettings aadSettings) : base(aadSettings, "/v2.0/.well-known/openid-configuration")
        {
        }
    }
}