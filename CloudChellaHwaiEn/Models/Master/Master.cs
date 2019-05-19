using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace CloudChellaHwaiEn
{
    [DynamoDBTable("master")]
    public class Master
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("title")]
        public string Title { get; set; }

        [DynamoDBProperty("year")]
        public int Year { get; set; }

        [DynamoDBProperty("main_release")]
        public string MainReleaseId { get; set; }

        [DynamoDBProperty("most_recent_release")]
        public string MostRecentReleaseId { get; set; }

        [DynamoDBProperty("styles")]
        public List<string> Styles { get; set; }

        [DynamoDBProperty("genres")]
        public List<string> Genres { get; set; }

        [DynamoDBProperty("artists")]
        public List<MasterArtist> Artists { get; set; }

        [DynamoDBProperty("tracklist")]
        public List<Track> Tracks { get; set; }

        [DynamoDBProperty("videos")]
        public List<Video> Videos { get; set; }
    }
}
