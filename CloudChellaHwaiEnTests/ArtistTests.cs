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
                new Member { Id = "5647", Name = "Nik Kershaw", IsActive = true },
                new Member { Id = "6707", Name = "Mandy Smith", IsActive = true },
                new Member { Id = "7247", Name = "Samantha Fox", IsActive = true },
                new Member { Id = "7705", Name = "Juliet Roberts", IsActive = true },
                new Member { Id = "35301", Name = "Paul McCartney", IsActive = true },
                new Member { Id = "40576", Name = "Andy Bell", IsActive = true },
                new Member { Id = "71081", Name = "Kim Wilde", IsActive = true },
                new Member { Id = "72872", Name = "Rick Astley", IsActive = true }
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
                new Alias { Id = "1141583", Name = "Dick Spatsley" }
            };
            var expectedGroups = new[]
            {
                new Group { Id = "146979", Name = "Band Aid II", IsActive = true },
                new Group { Id = "420265", Name = "Ferry Aid", IsActive = true }
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

        private class AliasEqualityComparer : IEqualityComparer<Alias>
        {
            public bool Equals(Alias x, Alias y)
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

            public int GetHashCode(Alias obj)
            {
                return AggregateHashCode(obj.Id, obj.Name);
            }
        }

        private class GroupEqualityComparer : IEqualityComparer<Group>
        {
            public bool Equals(Group x, Group y)
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
                    x.Name.Equals(y.Name) &&
                    x.IsActive.Equals(y.IsActive);
            }

            public int GetHashCode(Group obj)
            {
                return AggregateHashCode(obj.Id, obj.Name, obj.IsActive);
            }
        }

        private class MemberEqualityComparer : IEqualityComparer<Member>
        {
            public bool Equals(Member x, Member y)
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
                    x.Name.Equals(y.Name) &&
                    x.IsActive.Equals(y.IsActive);
            }

            public int GetHashCode(Member obj)
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
