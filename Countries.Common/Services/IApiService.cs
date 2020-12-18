using Countries.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Common.Services
{
    public interface IApiService
    {
        /// <summary>
        /// Requests a Json file from a given API url and converts it to the given Type.
        /// </summary>
        /// <typeparam name="T">Type that is going to be returned</typeparam>
        /// <param name="urlBase"></param>
        /// <param name="servicePrefix"></param>
        /// <param name="controller"></param>
        /// <returns>Response with the T object or an error message</returns>
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
    }
}
