using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Coda.Http
{
    public class Transport : ITransport, IDisposable
    {
        internal HttpClientTransport HttpClientTransport { get; set; }

        public Transport()
        {
            HttpClientTransport = new HttpClientTransport();
        }

        public void Configure(ITransportConfiguration configuration)
        {
            HttpClientTransport.SetTimeout(configuration.RequestTimeout);
            HttpClientTransport.SetBearerToken(configuration.AuthenticationKey);
            HttpClientTransport.SetUri(configuration.BaseUrl);
        }

        /// <summary>
        /// Sends a Raw (GET) Request to the API with no request object
        /// </summary>
        /// <typeparam name="TResponse"> Response Type</typeparam>
        /// <param name="methodName">Method Name / API Path</param>
        /// <returns>Wrapped Result from Transport</returns>
        public async Task<CommunicationResult<TResponse>> RawRequestAsync<TResponse>(string methodName)
        {
            return await InternalRequest<TResponse>(HttpClientMethod.Get, methodName, null);
        }

        /// <summary>
        /// Sends a Raw (POST) Request to the API with a request object
        /// </summary>
        /// <typeparam name="TObject">Request Type</typeparam>
        /// <typeparam name="TResponse"> Response Type</typeparam>
        /// <param name="methodName">Method Name / API Path</param>
        /// <param name="request">Request Object</param>
        /// <returns>Wrapped Result from Transport</returns>
        public async Task<CommunicationResult<TResponse>> RawRequestAsync<TObject, TResponse>(string methodName, TObject request)
        {
            return await InternalRequest<TResponse>(HttpClientMethod.Post, methodName, (object)request);
        }

        /// <summary>
        /// Internally make and translate the request to the Transport
        /// </summary>
        /// <typeparam name="TResponse"> Response Type</typeparam>
        /// <param name="method">HTTP Method</param>
        /// <param name="apiUrl"></param>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        internal async Task<CommunicationResult<TResponse>> InternalRequest<TResponse>(HttpClientMethod method, string apiUrl, object requestObj)
        {
            var returnResult = new CommunicationResult<TResponse>
            {
                DateSent = DateTime.Now,
            };

            try
            {
                var response = await HttpClientTransport.MakeRequest(method, apiUrl, requestObj).ConfigureAwait(false);
                string responseContentString = null;

                if (response.Content.Headers.ContentLength > 0)
                {
                    responseContentString = await response.Content.ReadAsStringAsync().ConfigureAwait(false); // Read this regardless of response
                }

                if (response.IsSuccessStatusCode)
                {
                    returnResult.Status = CommunicationStatus.Success;

                    // Don't wrap this, if it throws it will (rightfully) fall to ClientError
                    if (typeof(TResponse) == typeof(string))
                    {
                        returnResult.Data = (TResponse)Convert.ChangeType(responseContentString, typeof(TResponse));
                    }
                    else
                    {
                        var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponse>(responseContentString);
                        returnResult.Data = deserialized;
                    }
                }
                else
                {
                    returnResult.Status = CommunicationStatus.RemoteError;
                    returnResult.Exception = GetRemoteException(response.StatusCode, response.ReasonPhrase, response.Headers, responseContentString);
                }
            }

            // Catch timeouts specifically
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken.IsCancellationRequested)
                {
                    // Cancelled by implementation
                    returnResult.Status = CommunicationStatus.ClientError;
                    returnResult.Exception = ex;
                }
                else
                {
                    // HTTP Transport Timeout
                    returnResult.Status = CommunicationStatus.Timeout;
                }
            }
            catch (Exception ex)
            {
                returnResult.Status = CommunicationStatus.ClientError;
                returnResult.Exception = ex;
            }
            finally
            {
                returnResult.DateReturned = DateTime.Now;
            }

            return returnResult;
        }

        protected Exception GetRemoteException(HttpStatusCode statusCode, string statusReason, HttpResponseHeaders headers, string remoteMessage)
        {
            var remoteEx = new Exception($"Transport Remote Exception ({statusCode} {statusReason}), see Data.");
            remoteEx.Data.Add("Status Code", statusCode);
            remoteEx.Data.Add("Status Reason", statusReason);
            remoteEx.Data.Add("Headers", headers);
            remoteEx.Data.Add("Content", remoteMessage);
            return remoteEx;
        }

        public void Dispose()
        {
            HttpClientTransport?.Dispose();
            HttpClientTransport = null;
        }
    }
}