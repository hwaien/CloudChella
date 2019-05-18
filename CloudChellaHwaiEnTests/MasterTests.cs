using System.Collections.Generic;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.IO;
using System.Linq;
using Xunit;

namespace CloudChellaHwaiEn
{
    public class MasterTests
    {
        [Fact]
        public void object_mapping_works()
        {
            // Arrange
            var expectedStyles = new[]
            {
                "Euro-Disco"
            };
            var expectedGenres = new[]
            {
                "Electronic",
                "Pop"
            };
            var expectedArtists = new[]
            {
                new ArtistCompact { Id = "72872", Name = "Rick Astley" }
            };
            var expectedTracks = new[]
            {
                new Track { Position = "A", Duration = "3:32", Title = "Never Gonna Give You Up", Type = "track" },
                new Track { Position = "B", Duration = "3:30", Title = "Never Gonna Give You Up (Instrumental)", Type = "track" }
            };
            var expectedVideos = new[]
            {
                new Video
                {
                    Title = "Rick Astley - Never Gonna Give You Up (Video)",
                    Duration = 213,
                    Description = "Rick Astley - Never Gonna Give You Up (Video)",
                    Uri = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    Embed = true
                },
                new Video
                {
                    Title = "Never Gonna Give You Up (Cake Mix)    Rick Astley 1987",
                    Duration = 346,
                    Description = "Never Gonna Give You Up (Cake Mix)    Rick Astley 1987",
                    Uri = "https://www.youtube.com/watch?v=1lN2bSlL5SE",
                    Embed = true
                },
                new Video
                {
                    Title = "Rick Astley - Never Gonna Give You Up (Stephen Gilham - PHD Extended Mix)",
                    Duration = 436,
                    Description = "Rick Astley - Never Gonna Give You Up (Stephen Gilham - PHD Extended Mix)",
                    Uri = "https://www.youtube.com/watch?v=afKTEsELh_s",
                    Embed = true
                },
                new Video
                {
                    Title = "Never Gonna Give You Up (Escape to NY Mix)",
                    Duration = 422,
                    Description = "Never Gonna Give You Up (Escape to NY Mix)",
                    Uri = "https://www.youtube.com/watch?v=xoPIl7xvK5Q",
                    Embed = true
                },
            };
            var regionEndpoint = RegionEndpoint.GetBySystemName("us-west-1");
            var context = new DynamoDBContext(regionEndpoint);
            var json = File.ReadAllText("NeverGonnaGiveYouUpMaster.json");
            var doc = Document.FromJson(json);

            // Act
            var master = context.FromDocument<Master>(doc);

            // Assert
            Assert.Equal("96559", master.Id);
            Assert.Equal("Never Gonna Give You Up", master.Title);
            Assert.Equal(1987, master.Year);
            Assert.Equal("249504", master.MainReleaseId);
            Assert.Equal("1612378", master.MostRecentReleaseId);
            Assert.Equal(expectedStyles, master.Styles);
            Assert.Equal(expectedGenres, master.Genres);
            Assert.Equal(expectedArtists, master.Artists, new ArtistCompactEqualityComparer());
            Assert.Equal(expectedTracks, master.Tracks, new TrackEqualityComparer());
            Assert.Equal(expectedVideos, master.Videos, new VideoEqualityComparer());
        }

        private class VideoEqualityComparer : IEqualityComparer<Video>
        {
            public bool Equals(Video x, Video y)
            {
                if (x == null)
                {
                    return y == null;
                }

                if (y == null)
                {
                    return false;
                }

                return
                    x.Title.Equals(y.Title) &&
                    x.Description.Equals(y.Description) &&
                    x.Duration.Equals(y.Duration) &&
                    x.Uri.Equals(y.Uri) &&
                    x.Embed.Equals(y.Embed);
            }

            public int GetHashCode(Video obj)
            {
                return AggregateHashCode(obj.Title, obj.Description, obj.Duration, obj.Uri, obj.Embed);
            }
        }

        private class TrackEqualityComparer : IEqualityComparer<Track>
        {
            public bool Equals(Track x, Track y)
            {
                if (x == null)
                {
                    return y == null;
                }

                if (y == null)
                {
                    return false;
                }

                return
                    x.Title.Equals(y.Title) &&
                    x.Position.Equals(y.Position) &&
                    x.Duration.Equals(y.Duration) &&
                    x.Type.Equals(y.Type);
            }

            public int GetHashCode(Track obj)
            {
                return AggregateHashCode(obj.Duration, obj.Position, obj.Type, obj.Title);
            }
        }

        private class ArtistCompactEqualityComparer : IEqualityComparer<ArtistCompact>
        {
            public bool Equals(ArtistCompact x, ArtistCompact y)
            {
                if (x == null)
                {
                    return y == null;
                }

                if (y == null)
                {
                    return false;
                }

                return
                    x.Id.Equals(y.Id) &&
                    x.Name.Equals(y.Name);
            }

            public int GetHashCode(ArtistCompact obj)
            {
                return AggregateHashCode(obj.Id, obj.Name);
            }
        }

        private static int AggregateHashCode(params object[] list)
        {
            return list.Aggregate(19, UpdateHashCode);
        }

        private static int UpdateHashCode(int hashCode, object obj)
        {
            if (obj == null)
            {
                return hashCode;
            }

            unchecked
            {
                return hashCode * 31 + obj.GetHashCode();
            }
        }
    }
}
