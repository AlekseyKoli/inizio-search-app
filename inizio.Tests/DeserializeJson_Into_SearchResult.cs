using inizio.Models;
namespace inizio.Tests
{
    public class DeserializeJson_Into_SearchResult
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
        [Fact]
        public void DeserializeJson_WithMissingOrganicProperty_ShouldHaveEmptySitesList()
        {
            var json = @"{
                ""someOtherProperty"": []
            }";
            SearchResult? result = System.Text.Json.JsonSerializer.Deserialize<SearchResult>(json);
            Assert.NotNull(result);
            Assert.NotNull(result.Sites);
            Assert.Empty(result.Sites);
        }
        [Fact]
        public void DeserializeJson_WithEmptyOrganicArray_ShouldHaveEmptySitesList()
        {
            var json = @"{
                ""organic"": []
            }";
            SearchResult? result = System.Text.Json.JsonSerializer.Deserialize<SearchResult>(json);
            Assert.NotNull(result);
            Assert.NotNull(result.Sites);
            Assert.Empty(result.Sites);
        }
        [Fact]
        public void DeserializeJson_WithMultipleSites_ShouldPopulateSitesList()
        {
            var json = @"{
                ""organic"": [
                    {
                        ""title"": ""First Title"",
                        ""link"": ""https://www.first.com"",
                        ""snippet"": ""First snippet."",
                        ""position"": 1
                    },
                    {
                        ""title"": ""Second Title"",
                        ""link"": ""https://www.second.com"",
                        ""snippet"": ""Second snippet."",
                        ""position"": 2
                    }
                ]
            }";
            SearchResult? result = System.Text.Json.JsonSerializer.Deserialize<SearchResult>(json);
            Assert.NotNull(result);
            Assert.NotNull(result.Sites);
            Assert.Equal(2, result.Sites.Count);
            GoogleSite firstSite = result.Sites[0];
            Assert.Equal("First Title", firstSite.Title);
            Assert.Equal("https://www.first.com", firstSite.Link);
            Assert.Equal("First snippet.", firstSite.Snippet);
            Assert.Equal(1, firstSite.Position);
            GoogleSite secondSite = result.Sites[1];
            Assert.Equal("Second Title", secondSite.Title);
            Assert.Equal("https://www.second.com", secondSite.Link);
            Assert.Equal("Second snippet.", secondSite.Snippet);
            Assert.Equal(2, secondSite.Position);
        }
    }
}
