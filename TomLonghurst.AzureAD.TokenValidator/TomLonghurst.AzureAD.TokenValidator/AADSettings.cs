namespace TomLonghurst.AzureAD.TokenValidator
{
    public class AADSettings : IAADSettings
    {
        public string TenantId { get; set; }
        public string Audience { get; set; }
        public string Instance { get; set; }
    }
}