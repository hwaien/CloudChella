using Amazon.DynamoDBv2.DataModel;

namespace CloudChellaHwaiEn
{
    public class Alias
    {
        [DynamoDBProperty("id")]
        public string Id { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }
    }
}
