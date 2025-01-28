using Newtonsoft.Json;
using System.Text;

namespace PQAMCAPI.Services
{
    public interface IHttpClientHelper
    {
        Uri CreateRequestUri(string relativePath, string queryString = "");
        HttpContent CreateHttpContent<T>(T content);
        Task<T> ParseResponseAsync<T>(HttpResponseMessage response);
    }

    public class HttpClientHelper : IHttpClientHelper
    {
        
        public Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var uriBuilder = new UriBuilder(relativePath);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        public HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string route, string credentials, string? content = null) 
        {
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, route);
            request.Headers.Add("credentials", credentials);

            if (content != null)
            {
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            return request;
        }

        public HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, IsoDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<T> ParseResponseAsync<T>(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(data);
        }

        private JsonSerializerSettings IsoDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                };
            }
        }

    }
}
