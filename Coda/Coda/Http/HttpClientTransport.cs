using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Coda.Http
{
    internal class HttpClientTransport : IDisposable
    {
        public HttpClientTransport() => HttpClient = new HttpClient();

        protected HttpClient HttpClient { get; set; }

        public void SetTimeout(TimeSpan timeout)
        {
            HttpClient.Timeout = timeout;
        }

        public void SetBearerToken(string token = null)
        {
            if (token != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public void SetUri(Uri uri)
        {
            HttpClient.BaseAddress = uri;
        }

        public void SetUri(string uri)
        {
            HttpClient.BaseAddress = new Uri(uri);
        }

        public async Task<HttpResponseMessage> MakeRequest(HttpClientMethod method, string apiEndpoint, object requestObject = null)
        {
            var jsonString = requestObject == null ? null : Newtonsoft.Json.JsonConvert.SerializeObject(requestObject);
            var jsonContent = new StringContent(jsonString ?? string.Empty, System.Text.Encoding.UTF8, "application/json");

            Func<HttpClient, Task<HttpResponseMessage>> requestTask = null;

            switch(method)
            {
                case HttpClientMethod.Get:
                    requestTask = client => client.GetAsync(apiEndpoint);
                    break;
                case HttpClientMethod.Post:
                    requestTask = client => client.PostAsync(apiEndpoint, jsonContent);
                    break;
                case HttpClientMethod.Put:
                    requestTask = client => client.PutAsync(apiEndpoint, jsonContent);
                    break;
                case HttpClientMethod.Delete:
                    requestTask = client => client.DeleteAsync(apiEndpoint);
                    break;
            }

            return await requestTask(HttpClient).ConfigureAwait(false);
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
            HttpClient = null;
        }
    }
}