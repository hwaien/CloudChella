using Amazon;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.IO;
using Xunit;

namespace CloudChellaHwaiEn
{
    public class LabelTests
    {
        [Fact]
        public void object_mapping_works()
        {
            // Arrange
            var expectedUrls = new[]
            {
                "http://www.rcarecords.com/",
                "http://historysdumpster.blogspot.co.uk/2012/08/rca-colour-record-labels-of-70s.html",
                "https://www.bookogs.com/credit/535946-rca"
            };
            var regionEndpoint = RegionEndpoint.GetBySystemName("us-west-1");
            var context = new DynamoDBContext(regionEndpoint);
            var json = File.ReadAllText("RCA.json");
            var doc = Document.FromJson(json);

            // Act
            var label = context.FromDocument<Label>(doc);

            // Assert
            Assert.Equal("895", label.Id);
            Assert.Equal("RCA", label.Name);
            Assert.Equal(expectedUrls, label.Urls);
        }
    }
}
