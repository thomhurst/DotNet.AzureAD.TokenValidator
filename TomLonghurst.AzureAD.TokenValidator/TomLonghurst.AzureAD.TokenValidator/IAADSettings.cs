namespace TomLonghurst.AzureAD.TokenValidator
{
    public interface IAADSettings
    {
        string TenantId { get; set; }
        string Audience { get; set; }
        string Instance { get; set; }
    }
}