using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using OCA.API.Helpers;
using OCA.API.Interfaces;
using OCA.API.Models;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace OCA.API.Services
{
    public class CryptocurrencyRetrieval : ICryptocurrencyRetrieval
    {
        private readonly IDynamoDBContext _dynamoDBContext;
        private readonly ILogger<CryptocurrencyRetrieval> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDapperWrapper _dapperWrapper;
        private readonly string _connectionString;

        public CryptocurrencyRetrieval(IDynamoDBContext dynamoDBContext, ILogger<CryptocurrencyRetrieval> logger, IConfiguration configuration, IDapperWrapper dapperWrapper)
        {
            _dynamoDBContext = dynamoDBContext;
            _logger = logger;
            _configuration = configuration;
            _dapperWrapper = dapperWrapper;
            _connectionString = _configuration.GetConnectionString("OpenCrypt") ?? string.Empty;
        }

        public async Task<IEnumerable<Cryptocurrency>> GetAll()
        {
            using IDbConnection connection = DbConnectionFactory.CreateConnection(_connectionString);
            return await _dapperWrapper.QueryAsync<Cryptocurrency>(connection, "GetCryptocurrencies", commandType: CommandType.StoredProcedure);

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
