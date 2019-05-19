using System.Collections.Generic;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.IO;
using System.Linq;
using Xunit;

namespace CloudChellaHwaiEn
{
    public class ReleaseTests
    {
        [Fact]
        public void object_mapping_works()
        {
            // Arrange
            var expectedNotes = "UK Release has a black label with the text \"Manufactured In England\" printed on it.\n\nSleeve:\n\u2117 1987 \u2022 BMG Records (UK) Ltd. \u00a9 1987 \u2022 BMG Records (UK) Ltd.\nDistributed in the UK by BMG Records \u2022  Distribu\u00e9 en Europe par BMG/Ariola \u2022 Vertrieb en Europa d\u00fcrch BMG/Ariola.\n\nCenter labels:\n\u2117 1987 Pete Waterman Ltd.\nOriginal Sound Recording made by PWL.\nBMG Records (UK) Ltd. are the exclusive licensees for the world.\n\nDurations do not appear on the release.";
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
                new ReleaseArtist { Id = "72872", Name = "Rick Astley" }
            };
            var expectedFormats = new[]
            {
                new ReleaseFormat { Name = "Vinyl", Descriptions = new List<string> { "7\"", "45 RPM", "Single" } }
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
            var expectedLabels = new[] { new ReleaseLabel{ Id = "895", Name = "RCA", EntityType = "Label" } };
            var expectedTracks = new[]
            {
                new Track { Position = "A", Duration = "3:32", Title = "Never Gonna Give You Up", Type = "track" },
                new Track { Position = "B", Duration = "3:30", Title = "Never Gonna Give You Up (Instrumental)", Type = "track" }
            };
            var regionEndpoint = RegionEndpoint.GetBySystemName("us-west-1");
            var context = new DynamoDBContext(regionEndpoint);
            var json = File.ReadAllText("NeverGonnaGiveYouUpRelease.json");
            var doc = Document.FromJson(json);

            // Act
            var release = context.FromDocument<Release>(doc);

            // Assert
            Assert.Equal("249504", release.Id);
            Assert.Equal("Never Gonna Give You Up", release.Title);
            Assert.Equal(1987, release.Year);
            Assert.Equal("96559", release.Master);
            Assert.Equal("UK", release.Country);
            Assert.Equal(expectedNotes, release.Notes);
            Assert.Equal(expectedStyles, release.Styles);
            Assert.Equal(expectedGenres, release.Genres);
            Assert.Equal(expectedArtists, release.Artists, new ArtistCompactEqualityComparer());
            Assert.Equal(expectedTracks, release.Tracks, new TrackEqualityComparer());
            Assert.Equal(expectedVideos, release.Videos, new VideoEqualityComparer());
            Assert.Equal(expectedLabels, release.Labels, new LabelEqualityComparer());
            Assert.Equal(expectedFormats, release.Formats, new FormatEqualityComparer());
        }

        private class FormatEqualityComparer : IEqualityComparer<ReleaseFormat>
        {
            public bool Equals(ReleaseFormat x, ReleaseFormat y)
            {
                if (x == null)
                {
                    return y == null;
                }

                if (y == null)
                {
                    return false;
                }

                if (x.Name != y.Name)
                {
                    return false;
                }

                if (x.Descriptions == null)
                {
                    return y.Descriptions == null;
                }

                if (y.Descriptions == null)
                {
                    return false;
                }

                if (x.Descriptions.Count != y.Descriptions.Count)
                {
                    return false;
                }

                for (var i = 0; i < x.Descriptions.Count; i++)
                {
                    if (x.Descriptions[i] != y.Descriptions[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            public int GetHashCode(ReleaseFormat obj)
            {
                var list = new List<object> {obj.Name};

                if (obj.Descriptions != null)
                {
                    list.AddRange(obj.Descriptions);

                }

                var array = list.ToArray();

                return AggregateHashCode(array);
            }
        }

        private class LabelEqualityComparer : IEqualityComparer<ReleaseLabel>
        {
            public bool Equals(ReleaseLabel x, ReleaseLabel y)
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
                    x.Id == y.Id &&
                    x.Name == y.Name &&
                    x.EntityType == y.EntityType;
            }

            public int GetHashCode(ReleaseLabel obj)
            {
                return AggregateHashCode(obj.Id, obj.Name, obj.EntityType);
            }
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
                    x.Title == y.Title &&
                    x.Description == y.Description &&
                    x.Duration == y.Duration &&
                    x.Uri == y.Uri &&
                    x.Embed == y.Embed;
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
                    x.Title == y.Title &&
                    x.Position == y.Position &&
                    x.Duration == y.Duration &&
                    x.Type == y.Type;
            }

            public int GetHashCode(Track obj)
            {
                return AggregateHashCode(obj.Duration, obj.Position, obj.Type, obj.Title);
            }
        }

        private class ArtistCompactEqualityComparer : IEqualityComparer<ReleaseArtist>
        {
            public bool Equals(ReleaseArtist x, ReleaseArtist y)
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
                    x.Id == y.Id &&
                    x.Name == y.Name;
            }

            public int GetHashCode(ReleaseArtist obj)
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
