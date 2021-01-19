using Microsoft.AspNetCore.Http;

namespace SimpleTemplate_Shop.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest request) => /*The PathAndQuery () method works with the HttpRequest class to describe HTTP requests.
                                                                          This method generates a URL to which the browser will return after an update.
                                                                          basket of the requested request*/
            request.QueryString.HasValue
            ? $"{request.Path}{request.QueryString}"
            : request.Path.ToString();
    }
}
