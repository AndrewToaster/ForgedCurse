using System.Text.Json.Serialization;

namespace ForgedCurse.Json
{
    public class Category
    {
        /// <summary>
        /// The identifier of this category
        /// </summary>
        [JsonPropertyName("categoryId")]
        public int Identifier { get; set; }

        /// <summary>
        /// The name of this category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The URL pointing to a curseforge search with the category
        /// </summary>
        [JsonPropertyName("url")]
        public string CurseForgeUrl { get; set; }

        /// <summary>
        /// The URL pointing to the icon image for this category
        /// </summary>
        [JsonPropertyName("avatarUrl")]
        public string IconUrl { get; set; }

        /// <summary>
        /// The identifier of the addon this category was used for
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// The identifier of the game this category is associated with
        /// </summary>
        [JsonPropertyName("gameId")]
        public int GameIdentifier { get; set; }

        public int RootId { get; set; }
        public int ParentId { get; set; }
    }
}