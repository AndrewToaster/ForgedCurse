using ForgedCurse.Enumeration;

namespace ForgedCurse
{
    /// <summary>
    /// Structure holding data for searching addons
    /// </summary>
    public class AddonSearchData
    {
        /// <summary>
        /// Name filter of the query data
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Game version filter of the query data
        /// </summary>
        public string GameVersion { get; set; }

        /// <summary>
        /// Amount of addons to query
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Amount of addons to skip before querying
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// The kind of the addons being queried
        /// </summary>
        public AddonKind Kind { get; set; }

        /// <summary>
        /// The category of the addons to query
        /// </summary>
        public AddonCategory Category { get; set; }

        /// <summary>
        /// The sorting method of the addons
        /// </summary>
        public AddonSorting Sorting { get; set; }

        public AddonSearchData(string addonName = "", string gameVersion = "", int amount = 10, int offset = 0, AddonKind kind = AddonKind.Mod,
            AddonCategory category = AddonCategory.All, AddonSorting sorting = AddonSorting.Featured)
        {
            Name = addonName;
            GameVersion = gameVersion;
            Amount = amount;
            Offset = offset;
            Kind = kind;
            Category = category;
            Sorting = sorting;
        }

        /// <summary>
        /// Constructs a URL to request the addons
        /// </summary>
        /// <returns>Constructed URL</returns>
        public string BuildSearchUrl()
        {
            return BuildSearchUrl(Name, GameVersion, Amount, Offset, Category, Sorting, Kind);
        }

        /// <summary>
        /// Returns the URL to request the addons
        /// </summary>
        /// <remarks>
        /// Internally calls <see cref="BuildSearchUrl"/>
        /// </remarks>
        /// <returns>String representation of this instance</returns>
        public override string ToString()
        {
            return BuildSearchUrl();
        }

        /// <summary>
        /// Returns a string representation for not filtering using versions (query all versions)
        /// </summary>
        public static readonly string AllGameVersions = string.Empty;

        /// <summary>
        /// Returns a string representation for not filtering using name (query addons with different names)
        /// </summary>
        public static readonly string AllNames = string.Empty;

        /// <summary>
        /// Construct a URL to request addons
        /// </summary>
        /// <remarks>
        /// Look at the documentation of each query argument
        /// </remarks>
        /// <param name="addonName">The name filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="gameVersion">The game version filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="amount">The amount of mods to retrieve</param>
        /// <param name="offset">The amount of mods to skip (e.g: You retrieve 10, but skip 3. You skip the first 3 mods in the list, then retrieve 10</param>
        /// <param name="kind">The kind of addon you are querying (Mod, World, ...)</param>
        /// <param name="category">The category filter of this query (Addons, Server Utility, ...)</param>
        /// <param name="sorting">The method of sorting the addons from which to query</param>
        /// <returns>Constructed URL</returns>
        public static string BuildSearchUrl(string version = null, string name = null, int amount = 10, int offset = 0, AddonCategory category = AddonCategory.All,
            AddonSorting sort = AddonSorting.Featured, AddonKind kind = AddonKind.Mod)
        {
            string url = $"https://addons-ecs.forgesvc.net/api/v2/addon/search?categoryId={(int)category}&gameId=432&sort={(int)sort}&index={offset}&pageSize={amount}&sectionId={(int)kind}";

            if (!string.IsNullOrWhiteSpace(version))
                url += string.Format("&gameVersion={0}", version);

            if (!string.IsNullOrWhiteSpace(name))
                url += string.Format("&searchFilter={0}", name);

            return url;
        }
    }
}
