using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Enumeration;
using ForgedCurse.Utility;

namespace ForgedCurse
{
    /// <summary>
    /// Class containing methods that don't fit in a seperate class or is an extension method
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Reads and deserializes the <see cref="HttpResponseMessage.Content"/> into a generic type <typeparamref name="T"/> asynchronously
        /// </summary>
        /// <typeparam name="T">The type which to deserialize</typeparam>
        /// <param name="response">The respose from which to read the content</param>
        /// <returns>Constructed type</returns>
        public static async Task<T> ParseJsonAsync<T>(this HttpResponseMessage response)
        {
            return CurseJSON.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Reads and deserializes the <see cref="HttpResponseMessage.Content"/> into a generic type <typeparamref name="T"/> while also checking
        /// that the message has succeeded (<see cref="HttpResponseMessage.EnsureSuccessStatusCode"/>) asynchronously
        /// </summary>
        /// <typeparam name="T">The type which to deserialize</typeparam>
        /// <param name="response">The respose from which to read the content</param>
        /// <returns>Constructed type</returns>
        public static async Task<T> ParseJsonSafeAsync<T>(this HttpResponseMessage response)
        {
            return CurseJSON.Deserialize<T>(await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Computes a normalized array, removing all whitespace characters
        /// </summary>
        /// <param name="bytes">The byte array to normalize</param>
        /// <returns>Computated normalized array</returns>
        public static Span<byte> GetNormalizedArray(Span<byte> bytes)
        {
            bool IsWhitespace(byte b)
            {
                return b == 9 || b == 10 || b == 13 || b == 32;
            }

            List<byte> normalized = new List<byte>();

            foreach (byte b in bytes)
            {
                if (!IsWhitespace(b))
                {
                    normalized.Add(b);
                }
            }

            return normalized.ToArray();
        }

        /// <summary>
        /// Computes a fingerprint of a JAR file
        /// </summary>
        /// <param name="jarFile">The path pointing towards a JAR file</param>
        /// <returns>Computated fingerprint</returns>
        public static uint ComputeFingerprint(string jarFile)
        {
            if (!File.Exists(jarFile) || Path.GetExtension(jarFile) != ".jar")
                throw new ArgumentException("The specified path either doesn't exist or isn't pointing to a JAR file", nameof(jarFile));

            return MurmurHash2.HashNormalize(File.ReadAllBytes(jarFile));
        }

        /// <summary>
        /// Computes a fingerprint of a JAR file
        /// </summary>
        /// <param name="jarStream">The contents of the JAR file</param>
        /// <returns>Computated fingerprint</returns>
        public static uint ComputeFingerprint(Span<byte> jarStream)
        {
            return MurmurHash2.HashNormalize(jarStream.ToArray());
        }
    }
}
