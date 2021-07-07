using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ForgedCurse.Enumeration;

namespace ForgedCurse.Json
{
    /// <summary>
    /// Json-Parsed class containing info about an addon
    /// </summary>
    public class Addon//ForgeWrapper<CurseJSON.AddonInfo>
    {
        /// <summary>
        /// The name of this addon
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Provides a small description for this addon
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// The unique identifier of this addon
        /// </summary>
        [JsonPropertyName("id")]
        public int Identifier { get; set; }

        /// <summary>
        /// The authors that are associated with this addon
        /// </summary>
        public AddonAuthor[] Authors { get; set; }

        /// <summary>
        /// The attachments that come with this addon (e.g. Images)
        /// </summary>
        public AddonAttachment[] Attachments { get; set; }

        /// <summary>
        /// Returns the 3 latest addon releases
        /// </summary>
        /// <remarks>
        /// <see cref="LatestFiles"/> has considerably more information about each element than <see cref="Files"/>.
        /// This is the reason for only having 3 entries (due to the size limitation)
        /// </remarks>
        public LatestRelease[] LatestFiles { get; }

        /// <summary>
        /// Returns the all the addon releases
        /// </summary>
        /// <remarks>
        /// <see cref="Files"/> has considerably less information about each element than <see cref="LatestFiles"/>
        /// This is the reason for having all entries (due to the size limitation)
        /// </remarks>
        [JsonPropertyName("gameVersionLatestFiles")]
        public GameVersionLatestFile[] Files { get; set; }

        /// <summary>
        /// The date this addon was created (first ever release date)
        /// </summary>
        [JsonPropertyName("dateCreated")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date this addon was released
        /// </summary>
        /// <remarks>
        /// The release date is not similiar to the date created, rather the date it was last modified
        /// </remarks>
        [JsonPropertyName("dateReleased")]
        public DateTime ReleasedAt { get; set; }

        /// <summary>
        /// The date this addon was last modified
        /// </summary>
        [JsonPropertyName("dateModified")]
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        /// Whether or not the addon is featured
        /// </summary>
        [JsonPropertyName("isFeatured")]
        public bool Featured { get; set; }

        /// <summary>
        /// Whether or not this addon is available
        /// </summary>
        /// <remarks>
        /// Addon becomes unavailable due to multiple reaons (created marked it that way; it was abandoned; ...).
        /// Either way this means it will not show-up in search can be only accessed using its url
        /// </remarks>
        [JsonPropertyName("isAvailable")]
        public bool Available { get; set; }

        /// <summary>
        /// Whether or not the addon is experimental
        /// </summary>
        [JsonPropertyName("isExperiemental")]
        public bool Experimental { get; set; }

        /// <summary>
        /// The CurseForge URL for this addon
        /// </summary>
        [JsonPropertyName("websiteUrl")]
        public string Website { get; set; }

        /// <summary>
        /// The amount of downloads this addon has
        /// </summary>
        /// <remarks>
        /// For some reason CurseForge has its download count as a floating point number
        /// </remarks>
        [JsonPropertyName("downloadCount")]
        public float Downloads { get; set; }

        /// <summary>
        /// The popularity score of this addon
        /// </summary>
        [JsonPropertyName("popularityScore")]
        public float Popularity { get; set; }

        /// <summary>
        /// The popularity rank of this addon
        /// </summary>
        [JsonPropertyName("gamePopularityRank")]
        public int Rank { get; set; }

        /// <summary>
        /// The primary language token (e.g. enUS)
        /// </summary>
        public string PrimaryLanguage { get; set; }

        /// <summary>
        /// The string list of supported mod-loaders
        /// </summary>
        public string[] ModLoaders { get; set; }

        /// <summary>
        /// The default file (release) identifier
        /// </summary>
        public int DefaultFileId { get; set; }

        /// <summary>
        /// What the status of this addon is
        /// </summary>
        public ItemStatus Status { get; set; }

        /// <summary>
        /// The addon section this mod is in (e.g. 'mods' or 'resource-packs')
        /// </summary>
        [JsonPropertyName("categorySection")]
        public AddonCategorySection AddonType { get; set; }

        /// <summary>
        /// The categories in which this mod belongs in
        /// </summary>
        public Category[] Categories { get; set; }

        /// <summary>
        /// The name of the hosting website (portal)
        /// </summary>
        [JsonPropertyName("portalName")]
        public string Portal { get; set; }

        /// <summary>
        /// The name of the game this addon is for
        /// </summary>
        [JsonPropertyName("gameName")]
        public string Game { get; set; }

        /// <summary>
        /// The URL resource identifier for this addon
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Clean_URL#Slug
        /// </remarks>
        public string Slug { get; set; }

        /// <summary>
        /// The URL resource identifier for the game this addon is for
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Clean_URL#Slug
        /// </remarks>
        public string GameSlug { get; set; }
    }
}
