using inizio.Models;
namespace inizio.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void DeserializeSearchJSONIntoClass()
        {
            var json = @"{
                ""organic"": [
                    {
                        ""title"": ""Example Title"",
                        ""link"": ""https://www.example.com"",
                        ""snippet"": ""This is an example snippet."",
                        ""position"": 1
                    }
                ]
            }";

            SearchResult? result = System.Text.Json.JsonSerializer.Deserialize<SearchResult>(json);

            Assert.NotNull(result);
            Assert.NotNull(result.Sites);
            Assert.Single(result.Sites);

            GoogleSite site = result.Sites[0];

            Assert.Equal("Example Title", site.Title);
            Assert.Equal("https://www.example.com", site.Link);
            Assert.Equal("This is an example snippet.", site.Snippet);
            Assert.Equal(1, site.Position);
        }
    }
}
