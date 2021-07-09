using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgedCurse.Json
{
    public class Attachment
    {
        /// <summary>
        /// The identifier of the attachment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The identifier of the addon this attachment is associated with
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// The name of this attachment (NOT file name)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of this attachment
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The URL of the actual attachment
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The URL of the attachments thumbnail (small preview)
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Whether or not this is the default attachment (i.e. The icon of the addon)
        /// </summary>
        [JsonPropertyName("isDefault")]
        public bool Default { get; set; }

        /// <summary>
        /// The number identifier of the status of this mod (1 by default)
        /// </summary>
        public int Status { get; set; }
    }
}
