using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgedCurse.Json
{
    public class HashSearchResult
    {
        /// <summary>
        /// Unknown meaning
        /// </summary>
        public bool IsCacheBuilt { get; set; }

        /// <summary>
        /// The list of all hashes included in this <see cref="HashSearchResult"/>
        /// </summary>
        [JsonPropertyName("installedFingerprints")]
        public uint[] Hashes { get; set; }

        /// <summary>
        /// The list of all hashes that were not found/matched
        /// </summary>
        [JsonPropertyName("unmatchedFingerprints")]
        public uint[] NotFound { get; set; }

        /// <summary>
        /// The list of all hashes that were not found/matched
        /// </summary>
        /// <remarks>
        /// Indexes of <see cref="Found"/> hashes correspond to indexes in <see cref="Hits"/>
        /// </remarks>
        [JsonPropertyName("exactFingerprints")]
        public uint[] Found { get; set; }

        /// <summary>
        /// The list of all <see cref="HashHit"/>s in this <see cref="HashSearchResult"/>
        /// </summary>
        /// <remarks>
        /// Indexes of <see cref="Found"/> hashes correspond to indexes in <see cref="Hits"/>
        /// </remarks>
        [JsonPropertyName("exactMatches")]
        public HashHit[] Hits { get; set; }

        /// <summary>
        /// Retrieves the instance of <see cref="HashHit"/> with the hash value of <paramref name="hash"/>
        /// </summary>
        /// <param name="hash">The hash to search with</param>
        /// <returns><see cref="HashHit"/> if found; otherwise, <see langword="null"/></returns>
        public HashHit GetHit(uint hash)
        {
            for (int i = 0; i < Found.Length; i++)
            {
                uint fHash = Found[i];
                if (fHash == hash)
                {
                    return Hits[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieves the a hash value using an instance of <see cref="HashHit"/>
        /// </summary>
        /// <param name="hit">The <see cref="HashHit"/> instance to search with</param>
        /// <returns><see cref="uint"/> if found; otherwise, 0</returns>
        public uint GetHash(HashHit hit)
        {
            for (int i = 0; i < Hits.Length; i++)
            {
                HashHit fHit = Hits[i];
                if (fHit == hit)
                {
                    return Found[i];
                }
            }

            return 0;
        }

        /// <summary>
        /// Class containing information about a result in <see cref="HashSearchResult"/>
        /// </summary>
        public class HashHit : IEquatable<HashHit>
        {
            /// <summary>
            /// The identifier of the addon associated with the hash
            /// </summary>
            [JsonPropertyName("id")]
            public int AddonId { get; set; }

            /// <summary>
            /// The release found
            /// </summary>
            public HashHitRelease File { get; set; }

            public bool Equals(HashHit other)
            {
                return AddonId != 0 && AddonId == other?.AddonId;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as HashHit);
            }

            public override int GetHashCode()
            {
                return AddonId;
            }
        }
    }
}
