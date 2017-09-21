using System;
using System.Threading.Tasks;

namespace Coda.Http
{
    public interface ITransport : IDisposable
    {
        /// <summary>
        /// Configure the Transport
        /// </summary>
        /// <param name="configuration">Transport Configuration</param>
        void Configure(ITransportConfiguration configuration);

        /// <summary>
        /// Perform a CRUD Request on the Transport's Endpoint
        /// </summary>
        /// <typeparam name="TObject">CRUD Object Type</typeparam>
        /// <typeparam name="TResponse">Response Type</typeparam>
        /// <param name="method">CRUD Method</param>
        /// <param name="objectName">Object Name on Remote</param>
        /// <param name="request">Request Object</param>
        /// <returns></returns>
        Task<CommunicationResult<TResponse>> CrudRequestAsync<TObject, TResponse>(TransportCrudMethod method, string objectName, TObject request);

        /// <summary>
        /// Perform a Raw Request on the Transport's Endpoint
        /// </summary>
        /// <typeparam name="TResponse">Response Type</typeparam>
        /// <param name="methodName">Method Name</param>
        /// <returns></returns>
        Task<CommunicationResult<TResponse>> RawRequestAsync<TResponse>(string methodName);
        /// <summary>
        /// Perform a Raw Request on the Transport's Endpoint with a Request Object
        /// </summary>
        /// <typeparam name="TObject">Request Type</typeparam>
        /// <typeparam name="TResponse">Response Type</typeparam>
        /// <param name="methodName">Method Name</param>
        /// <param name="request">Request Object</param>
        /// <returns></returns>
        Task<CommunicationResult<TResponse>> RawRequestAsync<TObject, TResponse>(string methodName, TObject request);
    }
}