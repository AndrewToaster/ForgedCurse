using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ForgedCurse.Utility
{
    /// <summary>
    /// Provides a way to convert objects into json HttpContent
    /// </summary>
    public sealed class JsonContent : StringContent
    {
		/// <summary>
		/// Constructs a new instance of <see cref="JsonContent"/> using the specified JSON data
		/// </summary>
		/// <remarks>
		/// Internally calls <see cref="StringContent(string, Encoding, string)"/> with args: <paramref name="json"/>, <see cref="Encoding.UTF8"/>, "application/json"
		/// </remarks>
		/// <param name="json">JSON data to encode</param>
		public JsonContent(string json) : base(json, Encoding.UTF8, "application/json")
        {
        }

		/// <summary>
		/// Constructs a new instance of <see cref="JsonContent"/> from a object
		/// </summary>
		/// <remarks>
		/// Internally calls <see cref="JsonContent(string)"/> where the <see cref="string"/> value is set to <see cref="JsonSerializer.Serialize{T}(T, JsonSerializerOptions?)"/> from <paramref name="obj"/>
		/// </remarks>
		/// <param name="obj">The object to serialize</param>
		/// <returns>Instance of the constructed <see cref="JsonContent"/></returns>
		public static JsonContent FromObject<T>(T obj)
        {
            return new JsonContent(JsonSerializer.Serialize(obj));
        }
    }
}
