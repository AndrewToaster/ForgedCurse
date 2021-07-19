using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ForgedCurse.Json;
using ForgedCurse.Utility;

namespace ForgedCurse.Sections
{
    public abstract class BaseSection
    {
        private static RetryPolicy _retry { get => ForgeClient.RetryPolicy; }

        /// <summary>
        /// The base <see cref="ForgeClient"/>
        /// </summary>
        public ForgeClient Client { get; }

        internal readonly HttpClient _http;

        internal BaseSection(ForgeClient client)
        {
            Client = client;
            _http = Client.HttpClient;
        }

        protected Task<T> HttpGetJson<T>(string url)
        {
            return HttpJson<T>(url, _http.GetAsync);
        }

        protected Task<T> HttpPostJson<T>(string url, HttpContent content)
        {
            return HttpJson<T>(url, x => _http.PostAsync(x, content));
        }

        protected static Task<string> HttpString(string url, Func<string, Task<HttpResponseMessage>> method)
        {
            return HttpResponse(url, method).ContinueWith(t => t.Result.Content.ReadAsStringAsync().Result);
        }

        protected static async Task<T> HttpJson<T>(string url, Func<string, Task<HttpResponseMessage>> method)
        {
            return await GetResponseJson<T>(await HttpResponse(url, method).ConfigureAwait(false)).ConfigureAwait(false);
        }

        protected static async Task<HttpResponseMessage> HttpResponse(string url, Func<string, Task<HttpResponseMessage>> method)
        {
            var result = await _retry.ExecutePolicyAsync(() => method(url).CheckSuccess()).ConfigureAwait(false);
            if (!result.IsSuccess)
            {
                throw new("Could not execute the function");
            }

            return result.Value;
        }

        protected static async Task<T> GetResponseJson<T>(HttpResponseMessage resp)
        {
            return await JsonSerializer.DeserializeAsync<T>(await resp.Content.ReadAsStreamAsync().ConfigureAwait(false), ForgeClient.SerializerSettings).ConfigureAwait(false);
        }
    }
}
