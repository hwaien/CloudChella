using System.Collections.Generic;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.IO;
using System.Linq;
using Xunit;

namespace CloudChellaHwaiEn
{
    public class ArtistTests
    {
        [Fact]
        public void object_mapping_works_for_group_artist()
        {
            // Arrange
            var expectedUrls = new[]
            {
                "http://en.wikipedia.org/wiki/Ferry_Aid"
            };
            var expectedMembers = new[]
            {
                new ArtistMember { Id = "5647", Name = "Nik Kershaw", IsActive = true },
                new ArtistMember { Id = "6707", Name = "Mandy Smith", IsActive = true },
                new ArtistMember { Id = "7247", Name = "Samantha Fox", IsActive = true },
                new ArtistMember { Id = "7705", Name = "Juliet Roberts", IsActive = true },
                new ArtistMember { Id = "35301", Name = "Paul McCartney", IsActive = true },
                new ArtistMember { Id = "40576", Name = "Andy Bell", IsActive = true },
                new ArtistMember { Id = "71081", Name = "Kim Wilde", IsActive = true },
                new ArtistMember { Id = "72872", Name = "Rick Astley", IsActive = true }
            };
            var regionEndpoint = RegionEndpoint.GetBySystemName("us-west-1");
            var context = new DynamoDBContext(regionEndpoint);
            var json = File.ReadAllText("FerryAid.json");
            var doc = Document.FromJson(json);

            // Act
            var artist = context.FromDocument<Artist>(doc);

            // Assert
            Assert.Equal("420265", artist.Id);
            Assert.Equal("Ferry Aid", artist.Name);
            Assert.Equal(expectedMembers, artist.Members, new MemberEqualityComparer());
            Assert.Equal(expectedUrls, artist.Urls);
        }

        [Fact]
        public void object_mapping_works_for_individual_artist()
        {
            // Arrange
            var expectedUrls = new[]
            {
                "http://www.rickastley.co.uk/",
                "http://en.wikipedia.org/wiki/Rick_Astley",
                "https://www.facebook.com/RickAstley",
                "https://twitter.com/rickastley",
                "https://myspace.com/rickastley",
                "https://www.instagram.com/officialrickastley/",
                "https://www.youtube.com/channel/UCuAXFkgsw1L7xaCfnd5JJOw",
                "https://www.bookogs.com/credit/559827-rick-astley"
            };
            var expectedNameVariations = new[]
            {
                "Astley R.",
                "R Astley",
                "R. Asley",
                "R. Astley",
                "R.Astley",
                "Richard Astley",
                "Rick Ashley",
                "Rick Asley",
                "Rick Astle",
                "Rick Astly"
            };
            var expectedAliases = new []
            {
                new ArtistAlias { Id = "1141583", Name = "Dick Spatsley" }
            };
            var expectedGroups = new[]
            {
                new ArtistGroup { Id = "146979", Name = "Band Aid II", IsActive = true },
                new ArtistGroup { Id = "420265", Name = "Ferry Aid", IsActive = true }
            };
            var regionEndpoint = RegionEndpoint.GetBySystemName("us-west-1");
            var context = new DynamoDBContext(regionEndpoint);
            var json = File.ReadAllText("RickAstley.json");
            var doc = Document.FromJson(json);

            // Act
            var artist = context.FromDocument<Artist>(doc);

            // Assert
            Assert.Equal("72872", artist.Id);
            Assert.Equal("Rick Astley", artist.Name);
            Assert.Equal("Richard Paul Astley", artist.RealName);
            Assert.Equal(expectedNameVariations, artist.NameVariations);
            Assert.Equal(expectedAliases, artist.Aliases, new AliasEqualityComparer());
            Assert.Equal(expectedGroups, artist.Groups, new GroupEqualityComparer());
            Assert.Equal(expectedUrls, artist.Urls);
        }

        private class AliasEqualityComparer : IEqualityComparer<ArtistAlias>
        {
            public bool Equals(ArtistAlias x, ArtistAlias y)
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

            public int GetHashCode(ArtistAlias obj)
            {
                return AggregateHashCode(obj.Id, obj.Name);
            }
        }

        private class GroupEqualityComparer : IEqualityComparer<ArtistGroup>
        {
            public bool Equals(ArtistGroup x, ArtistGroup y)
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
                    x.IsActive == y.IsActive;
            }

            public int GetHashCode(ArtistGroup obj)
            {
                return AggregateHashCode(obj.Id, obj.Name, obj.IsActive);
            }
        }

        private class MemberEqualityComparer : IEqualityComparer<ArtistMember>
        {
            public bool Equals(ArtistMember x, ArtistMember y)
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
                    x.IsActive == y.IsActive;
            }

            public int GetHashCode(ArtistMember obj)
            {
                return AggregateHashCode(obj.Id, obj.Name, obj.IsActive);
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
