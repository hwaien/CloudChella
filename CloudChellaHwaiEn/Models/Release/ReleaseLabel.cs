using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace CloudChellaHwaiEn
{
    public class ReleaseLabel
    {
        [DynamoDBProperty("id")]
        public string Id { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("entity_type_name")]
        public string EntityType { get; set; }
    }
}
