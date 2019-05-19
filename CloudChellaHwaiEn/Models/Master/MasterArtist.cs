using Amazon.DynamoDBv2.DataModel;

namespace CloudChellaHwaiEn
{
    public class MasterArtist
    {
        [DynamoDBProperty("id")]
        public string Id { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }
    }
}
