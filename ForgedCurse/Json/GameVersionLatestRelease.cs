using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgedCurse.Json
{
	public class GameVersionLatestRelease
	{
		public string GameVersion { get; set; }

		[JsonPropertyName("projectFileId")]
		public int FileId { get; set; }

		[JsonPropertyName("projectFileName")]
		public string FileName { get; set; }

		public int FileType { get; set; }
		public int ModLoader { get; set; }
	}
}
