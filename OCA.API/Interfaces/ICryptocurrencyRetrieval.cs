using OCA.API.Models;

namespace OCA.API.Interfaces
{
    public interface ICryptocurrencyRetrieval
    {
        Task<IEnumerable<Cryptocurrency>> GetAll();
        Task<IEnumerable<Cryptocurrency>> Get();
        Task<Cryptocurrency> Get(int rank);
    }
}
