using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Cedita.One.Api.Transport.Http
{
    internal class HttpClientTransport : IDisposable
    {
        protected HttpClient HttpClient { get; set; }

        public HttpClientTransport()
        {
            HttpClient = new HttpClient();
        }

        public void SetTimeout(TimeSpan timeout)
        {
            HttpClient.Timeout = timeout;
        }

        public void SetServiceName(string serviceName)
        {
            SetUri(GetServiceUri(serviceName));
        }

        public void SetBearerToken(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void SetUri(Uri uri)
        {
            HttpClient.BaseAddress = uri;
        }

        public void SetUri(string uri)
        {
            HttpClient.BaseAddress = new Uri(uri);
        }

        protected Uri GetServiceUri(string serviceName)
        {
            return new Uri(string.Format(Constants.ApiUriFormat, serviceName));
        }

        public async Task<HttpResponseMessage> MakeRequest(HttpClientMethod method, string apiEndpoint, object requestObject = null)
        {
            var jsonString = requestObject == null ? null : Newtonsoft.Json.JsonConvert.SerializeObject(requestObject);
            var jsonContent = new StringContent(jsonString ?? "", System.Text.Encoding.UTF8, "application/json");

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
