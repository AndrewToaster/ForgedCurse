using ForgedCurse.Enumeration;
using ForgedCurse.Utility;
using ForgedCurse.WrapperTypes;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ForgedCurse
{
    /// <summary>
    /// Quality of life class, making the querying of addons easier
    /// </summary>
    public class AddonSearchIterator : IDefferedArray<Addon>, IDisposable
    {
        private ForgeClient _fmlClient;
        private HttpClient _client;

        /// <summary>
        /// The data used for querying the addons
        /// </summary>
        public AddonSearchData SearchData { get; set; }

        public AddonSearchIterator(ForgeClient client, string addonName = "", string gameVersion = "", int amount = 10, int offset = 0, AddonKind kind = AddonKind.Mod,
            MinecraftCategory category = MinecraftCategory.All, AddonSorting sorting = AddonSorting.Featured)
        {
            _client = new HttpClient() { BaseAddress = new Uri("https://addons-ecs.forgesvc.net/api/v2/") };
            _fmlClient = client;
            SearchData = new AddonSearchData(addonName, gameVersion, amount, offset, kind, category, sorting);
        }

        public AddonSearchIterator(ForgeClient client, AddonSearchData data)
        {
            _client = new HttpClient() { BaseAddress = new Uri("https://addons-ecs.forgesvc.net/api/v2/") };
            _fmlClient = client;
            SearchData = data;
        }

        /// <summary>
        /// Returns the next addon in the query
        /// </summary>
        /// <returns>The queried <see cref="CurseJSON.AddonInfo"/></returns>
        public async Task<Addon> NextAsync()
        {
            SearchData.Amount = 1;

            string url = SearchData.BuildSearchUrl();
            var result = await ForgeClient.RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(url).CheckSuccess());
            var resp = result.Value;

            SearchData.Offset++;

            return await resp.ParseJsonAsync<CurseJSON.AddonInfo[]>().ContinueWith(task => new Addon(task.Result[0], _fmlClient));
        }

        /// <summary>
        /// Returns next range of addons in the query
        /// </summary>
        /// <param name="length">The amount of addons to query</param>
        /// <returns>The queried range of <see cref="CurseJSON.AddonInfo"/> array</returns>
        public async Task<IEnumerable<Addon>> NextRangeAsync(int length)
        {
            SearchData.Amount = length;

            string url = SearchData.BuildSearchUrl();
            var result = await ForgeClient.RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(url).CheckSuccess());
            var resp = result.Value;

            SearchData.Offset += length;

            return await resp.ParseJsonAsync<CurseJSON.AddonInfo[]>().ContinueWith(task => task.Result.Select(x => new Addon(x, _fmlClient)));
        }

        /// <summary>
        /// Returns the next addon in the query
        /// </summary>
        /// <returns>The queried <see cref="CurseJSON.AddonInfo"/></returns>
        public Addon Next()
        {
            return AsyncContext.Run(() => NextAsync());
        }

        /// <summary>
        /// Returns next range of addons in the query
        /// </summary>
        /// <param name="length">The amount of addons to query</param>
        /// <returns>The queried range of <see cref="CurseJSON.AddonInfo"/> array</returns>
        public IEnumerable<Addon> NextRange(int length)
        {
            return AsyncContext.Run(() => NextRangeAsync(length));
        }

        /// <summary>
        /// Resets the offset of <see cref="SearchData"/>
        /// </summary>
        public void ResetPosition()
        {
            SearchData.Offset = 0;
        }

        /// <summary>
        /// Recycles this instance by resetting the <see cref="SearchData"/> without creating a new instance
        /// </summary>
        public void Recycle(string addonName = "", string gameVersion = "", int amount = 10, int offset = 0, AddonKind kind = AddonKind.Mod,
            MinecraftCategory category = MinecraftCategory.All, AddonSorting sorting = AddonSorting.Featured)
        {
            SearchData = new AddonSearchData(addonName, gameVersion, amount, offset, kind, category, sorting);
        }

        /// <summary>
        /// Recycles this instance by resetting the <see cref="SearchData"/> without creating a new instance
        /// </summary>
        public void Recycle(AddonSearchData data)
        {
            SearchData = data;
        }

        /// <summary>
        /// Releases the resources used by the <see cref="_client"/>
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
