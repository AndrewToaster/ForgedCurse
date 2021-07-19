using System.Text.Json.Serialization;
using ForgedCurse.Enumeration;

namespace ForgedCurse.Json
{
    public class CategorySection
    {
        [JsonPropertyName("id")]
        public int Identifier { get; set; }

        /// <summary>
        /// The identifier of the game this section is associated with
        /// </summary>
        [JsonPropertyName("gameId")]
        public int GameIdentifier { get; set; }

        public int PackageType { get; set; }

        public string Path { get; set; }

        public string InitialInclusionPattern { get; set; }

        public string ExtraIncludePattern { get; set; }

        /// <summary>
        /// The kind of category the addon belongs in
        /// </summary>
        [JsonPropertyName("gameCategoryId")]
        public int CategoryId { get; set; }
    }
}