using Amazon.DynamoDBv2.DataModel;

namespace CloudChellaHwaiEn
{
    public class Track
    {
        [DynamoDBProperty("type_")]
        public string Type { get; set; }

        [DynamoDBProperty("title")]
        public string Title { get; set; }

        [DynamoDBProperty("position")]
        public string Position { get; set; }

        [DynamoDBProperty("duration")]
        public string Duration { get; set; }
    }
}
