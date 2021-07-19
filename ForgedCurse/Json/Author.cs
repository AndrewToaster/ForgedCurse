using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgedCurse.Json
{
    public class Author
    {

		/// <summary>
		/// The name of the author
		/// </summary>
		public string Name { get; set; }

        /// <summary>
        /// The link of the member page of Curseforge for this author
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The addon id this instance of <see cref="Author"/> is associated with
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// For the actual id of the author see <see cref="UserId"/>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The URL-Slug of the author for the member page of Curseforge
        /// </summary>
        public int UserId { get; set; }

		/// <summary>
		/// The id of the author on Twitch
		/// </summary>
		/// <remarks>
		/// This is most likely a legacy id from before the migration from Twitch to Curseforge
		/// </remarks>
		public int? TwitchId { get; set; }

		/// <summary>
		/// The role of the author for the associated addon (see <see cref="ProjectId"/>)
		/// </summary>
		[JsonPropertyName("projectTitleId")]
        public int? RoleId { get; set; }

        /// <summary>
        /// The role name of the author for the associated addon (see <see cref="ProjectId"/>)
        /// </summary>
        [JsonPropertyName("projectTitleTitle")]
        public string RoleName { get; set; }
    }
}
