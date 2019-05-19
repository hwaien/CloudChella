using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace CloudChellaHwaiEn
{
    [DynamoDBTable("artist")]
    public class Artist
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("profile")]
        public string Profile { get; set; }

        [DynamoDBProperty("urls")]
        public List<string> Urls { get; set; }

        [DynamoDBProperty("realname")]
        public string RealName { get; set; }

        [DynamoDBProperty("namevariations")]
        public List<string> NameVariations { get; set; }

        [DynamoDBProperty("aliases")]
        public List<ArtistAlias> Aliases { get; set; }

        [DynamoDBProperty("groups")]
        public List<ArtistGroup> Groups { get; set; }

        [DynamoDBProperty("members")]
        public List<ArtistMember> Members { get; set; }
    }
}
