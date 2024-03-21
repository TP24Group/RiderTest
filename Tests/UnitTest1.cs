using System.Net;
using System.Net.Http.Json;
using Shouldly;

namespace Tests;

public class UnitTest1
{
    private readonly HttpTestServer server = new();
    
    [Fact]
    public async Task Rejects_bad_content_for_creation()
    {
        var httpClient = await this.server.GetClient();

        var content = "dwqewf{/}";
        var response = await httpClient.PostAsync("/eligibility/rulesets", JsonContent.Create(content));
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}