using Newtonsoft.Json.Linq;
using Refit;

namespace Library.IntegrationTests.Core;

public interface IMockServerClient
{
    [Put("/mockserver/verify")]
    Task<HttpResponseMessage> Verify([Body] VerifyRequest request);

    [Put("/mockserver/retrieve")]
    Task<List<GetRecordedRequestsResponse>> GetRecordedRequests([Body] GetRecordedRequests request);

    [Put("/mockserver/retrieve")]
    Task<List<GetRecordedRequestsResponses>> GetAllRecordedRequests();
}

public record GetRecordedRequests(string Path, string Method);

public class GetRecordedRequestsResponse
{
    public JObject Body { get; set; }
}

public class GetRecordedRequestsResponses
{
    public JArray Body { get; set; }
}

public class VerifyRequest(string path, int atLeast = 1)
{
    public HttpRequest HttpRequest { get; set; } = new(path);

    public Times Times { get; set; } = new(atLeast);
}

public record HttpRequest(string Path);

public record Times(int AtLeast);