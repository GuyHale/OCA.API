using Amazon.DynamoDBv2.DataModel;

namespace OCA.API.Models
{
    [DynamoDBTable("ApiKeys")]
    public class ApiKey
    {
        [DynamoDBHashKey("Key")]
        public string Key { get; set; } = string.Empty;
    }
}
