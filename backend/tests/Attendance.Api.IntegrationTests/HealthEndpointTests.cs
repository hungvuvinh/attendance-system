using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Attendance.Api.IntegrationTests;

public class HealthEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public HealthEndpointTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_health_returns_healthy_status()
    {
        using var response = await client.GetAsync("/api/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("Healthy", document.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task Swagger_ui_is_available_in_development()
    {
        using var response = await client.GetAsync("/swagger/index.html");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Cors_allows_only_the_frontend_origin()
    {
        using var allowedRequest = new HttpRequestMessage(HttpMethod.Get, "/api/health");
        allowedRequest.Headers.Add("Origin", "http://localhost:3000");
        using var allowedResponse = await client.SendAsync(allowedRequest);

        Assert.Equal("http://localhost:3000", allowedResponse.Headers.GetValues("Access-Control-Allow-Origin").Single());

        using var rejectedRequest = new HttpRequestMessage(HttpMethod.Get, "/api/health");
        rejectedRequest.Headers.Add("Origin", "http://localhost:9999");
        using var rejectedResponse = await client.SendAsync(rejectedRequest);

        Assert.False(rejectedResponse.Headers.Contains("Access-Control-Allow-Origin"));
    }
}
