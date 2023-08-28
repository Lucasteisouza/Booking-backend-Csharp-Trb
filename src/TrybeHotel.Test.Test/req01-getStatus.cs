namespace trybe_hotel.Test.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

public class StatusJson {
    public string? Message { get; set; }
}

public class TestReq01 : IClassFixture<WebApplicationFactory<Program>>
{
    public HttpClient _clientStatusGet;

    public TestReq01(WebApplicationFactory<Program> factory)
    {
        _clientStatusGet = factory.CreateClient();
    }

    [Trait("Category", "1. Desenvolva o endpoint GET")]
    [Theory(DisplayName = "Será validado que a resposta será um status http 200")]
    [InlineData("/")]
    public async Task TestStatusController(string url)
    {
        var response = await _clientStatusGet.GetAsync(url);
        var responseString = await response.Content.ReadAsStringAsync();
        StatusJson jsonResponse = JsonConvert.DeserializeObject<StatusJson>(responseString);
        response.EnsureSuccessStatusCode();
        Assert.Contains("online", jsonResponse.Message);
    }
}