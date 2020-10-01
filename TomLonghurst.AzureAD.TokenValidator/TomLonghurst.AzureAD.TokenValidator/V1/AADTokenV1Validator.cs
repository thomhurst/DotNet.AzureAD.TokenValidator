namespace TomLonghurst.AzureAD.TokenValidator.V1
{
    public class AADTokenV1Validator : AADTokenBaseValidator, IAADTokenV1Validator
    {
        public AADTokenV1Validator(IAADSettings aadSettings) : base(aadSettings, "/.well-known/openid-configuration")
        {
        }
    }
}