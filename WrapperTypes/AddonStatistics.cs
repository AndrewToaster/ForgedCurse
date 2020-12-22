using System;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Provides a structure for addon statistics
    /// </summary>
    public struct AddonStatistics
    {
        private readonly CurseJSON.AddonInfo _addon;

        /// <summary>
        /// The amount of downloads this addon has
        /// </summary>
        public int Downloads { get => (int)_addon.downloadCount; }

        /// <summary>
        /// The popularity score of this addon
        /// </summary>
        public float PopularityScore { get => _addon.popularityScore; }

        /// <summary>
        /// The popularity rank of this addon
        /// </summary>
        public int Rank { get => _addon.gamePopularityRank; }

        public AddonStatistics(CurseJSON.AddonInfo addonInfo)
        {
            _addon = addonInfo;
        }
    }
}
