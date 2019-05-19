using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace CloudChellaHwaiEn
{
    [DynamoDBTable("release")]
    public class Release
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("title")]
        public string Title { get; set; }

        [DynamoDBProperty("master_id")]
        public string Master { get; set; }

        [DynamoDBProperty("year")]
        public int Year { get; set; }

        [DynamoDBProperty("country")]
        public string Country { get; set; }

        [DynamoDBProperty("notes")]
        public string Notes { get; set; }

        [DynamoDBProperty("styles")]
        public List<string> Styles { get; set; }

        [DynamoDBProperty("genres")]
        public List<string> Genres { get; set; }

        [DynamoDBProperty("videos")]
        public List<Video> Videos { get; set; }

        [DynamoDBProperty("labels")]
        public List<ReleaseLabel> Labels { get; set; }

        [DynamoDBProperty("artists")]
        public List<ReleaseArtist> Artists { get; set; }

        [DynamoDBProperty("tracklist")]
        public List<Track> Tracks { get; set; }

        [DynamoDBProperty("formats")]
        public List<ReleaseFormat> Formats { get; set; }
    }
}
