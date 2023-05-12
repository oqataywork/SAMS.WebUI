using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SAMS.WebUI.Services.Partials
{
    public interface IService
    {
        string HostUrl { get; }

        Task<T> HttpGet<T>(string apiUrl) where T : class;
        Task<U> HttpPost<T, U>(string apiUrl, T request)
            where T : class
            where U : class;
    }

    public class BaseService : IService
    {
        private static readonly Lazy<HttpClientHandler> _lazy = new Lazy<HttpClientHandler>(() =>
        {
            // Enable cookies.
            HttpClientHandler ret = new HttpClientHandler();
            ret.CookieContainer = new System.Net.CookieContainer();
            ret.ClientCertificateOptions = ClientCertificateOption.Automatic;
            return ret;
        });
        protected static HttpClientHandler _httpClientHandler { get { return _lazy.Value; } }
        static BaseService()
        {
            // For HTTPS Certications.
            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
        }
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient(_httpClientHandler);
            client.BaseAddress = new Uri(this.HostUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private IService _service = null;
        private string _hostUrl = null;
        public string HostUrl
        {
            get
            {
                if (_service != null)
                {
                    return _service.HostUrl;
                }
                return _hostUrl;
            }

            private set
            {
                _hostUrl = value;
            }
        }

        // For decorate pattern.
        public BaseService(IService service)
        {
            _service = service;
        }

        public BaseService(string hostUrl)
        {
            _service = null;
            this.HostUrl = hostUrl;
        }

        public virtual async Task<T> HttpGet<T>(string apiUrl) where T : class
        {
            if (_service != null)
            {
                T respObj = await _service.HttpGet<T>(apiUrl);
                return respObj;
            }

            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    T respObj = await response.Content.ReadAsAsync<T>();
                    return respObj;
                }
                return default(T);
            }
        }

        public virtual async Task<U> HttpPost<T, U>(string apiUrl, T request) where T : class where U : class
        {
            if (_service != null)
            {
                U respObj = await _service.HttpPost<T, U>(apiUrl, request);
                return respObj;
            }

            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, request);
                if (response.IsSuccessStatusCode)
                {
                    U respObj = await response.Content.ReadAsAsync<U>();
                    return respObj;
                }
                return default(U);
            }
        }
    }
}