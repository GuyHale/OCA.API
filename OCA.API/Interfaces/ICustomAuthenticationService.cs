namespace OCA.API.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<bool> VerifyApiKey(string apiKeyString);
        Task<ICustomResponse> CreateApiKey(string apiKey);
    }
}
