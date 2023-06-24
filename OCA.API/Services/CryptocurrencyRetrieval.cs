using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using OCA.API.Interfaces;
using OCA.API.Models;

namespace OCA.API.Services
{
    public class CryptocurrencyRetrieval : ICryptocurrencyRetrieval
    {
        private readonly IDynamoDBContext _dynamoDBContext;
        private readonly ILogger<CryptocurrencyRetrieval> _logger;

        public CryptocurrencyRetrieval(IDynamoDBContext dynamoDBContext, ILogger<CryptocurrencyRetrieval> logger)
        {
            _dynamoDBContext = dynamoDBContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Cryptocurrency>> Get()
        {
            try
            {
                List<ScanCondition> scanConditions = new List<ScanCondition>();
                return await _dynamoDBContext.ScanAsync<Cryptocurrency>(scanConditions).GetRemainingAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
                throw new AmazonDynamoDBException(ex.Message);
            }           
        }
        
        public async Task<Cryptocurrency> Get(int rank)
        {
            try
            {
                List<ScanCondition> scanConditions = new List<ScanCondition>();
                return await _dynamoDBContext.LoadAsync<Cryptocurrency>(rank);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName}", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
                throw new AmazonDynamoDBException(ex.Message);
            }
        }
    }
}
