using Amazon.DynamoDBv2.DataModel;

namespace OCA.API.Models
{
    [DynamoDBTable("Cryptocurrencies")]
    public class Cryptocurrency
    {
        [DynamoDBHashKey("Rank")]
        public short Rank { get; set; }

        [DynamoDBProperty("Name")]
        public string? Name { get; set; }

        [DynamoDBProperty("Abbreviation")]
        public string? Abbreviation { get; set; }

        [DynamoDBProperty("USDValuation")]
        public string? Valuation { get; set; }

        [DynamoDBProperty("MarketCap")]
        public string? MarketCap { get; set; }

        [DynamoDBProperty("Description")]
        public string? Description { get; set; }

    }
}
