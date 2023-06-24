using OCA.API.Models;
using OCA.API.Models.Responses;

namespace OCA.API.Interfaces
{
    public interface ICryptocurrencyApi
    {
        Task<ApiResponse<IEnumerable<Cryptocurrency>>> GetAll(string apiKey);
    }
}
