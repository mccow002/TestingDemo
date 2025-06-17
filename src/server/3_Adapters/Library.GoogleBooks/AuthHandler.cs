using System.Net;
using System.Web;

namespace Library.GoogleBooks;

public class AuthHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var uri = request.RequestUri;

        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["key"] = "AIzaSyCC478lljstyd4uqJQo-Kudqeddx5Osx2o";
        uriBuilder.Query = query.ToString();

        request.RequestUri = uriBuilder.Uri;
        
        return base.SendAsync(request, cancellationToken);
    }
}