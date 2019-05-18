using Amazon.DynamoDBv2.DataModel;

namespace CloudChellaHwaiEn
{
    public class Group
    {
        [DynamoDBProperty("id")]
        public string Id { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("active")]
        public bool IsActive { get; set; }
    }
}
