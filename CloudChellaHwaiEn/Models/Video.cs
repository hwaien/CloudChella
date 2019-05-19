using Amazon.DynamoDBv2.DataModel;

namespace CloudChellaHwaiEn
{
    public class Video
    {
        [DynamoDBProperty("title")]
        public string Title { get; set; }

        [DynamoDBProperty("description")]
        public string Description { get; set; }

        [DynamoDBProperty("duration")]
        public int Duration { get; set; }

        [DynamoDBProperty("uri")]
        public string Uri { get; set; }

        [DynamoDBProperty("embed")]
        public bool Embed { get; set; }
    }
}
