using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace CloudChellaHwaiEn
{
    public class ReleaseFormat
    {
        [DynamoDBProperty("descriptions")]
        public List<string> Descriptions { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }
    }
}
