using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace CloudChellaHwaiEn
{
    [DynamoDBTable("label")]
    public class Label
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("profile")]
        public string Profile { get; set; }

        [DynamoDBProperty("urls")]
        public List<string> Urls { get; set; }

        [DynamoDBProperty("contact_info")]
        public string ContactInfo { get; set; }
    }
}
