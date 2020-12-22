using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Enumeration;
using ForgedCurse.Utility;
using ForgedCurse.WrapperTypes;

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
        /// Code sugar for reducing the length of checking a async <see cref="HttpResponseMessage"/>'s status code
        /// </summary>
        /// <param name="responseMessage">The message to check</param>
        /// <returns>Same message as <paramref name="responseMessage"/></returns>
        public static async Task<HttpResponseMessage> CheckSuccess(this Task<HttpResponseMessage> responseMessage)
        {
            return await responseMessage.ContinueWith(task => task.Result.EnsureSuccessStatusCode());
        }

        /// <summary>
        /// Converts all entries in source array to their <see cref="object.ToString"/> representation
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="array">The array to convert</param>
        /// <returns><see cref="string"/>[] array</returns>
        public static string[] ArrayToString<T>(this T[] array)
        {
            string[] outArr = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                outArr[i] = array[i].ToString();
            }

            return outArr;
        }

        /// <summary>
        /// Creates a <see cref="IReadOnlyCollection{TOut}"/> from an <typeparamref name="TIn"/>[] array using <paramref name="selector"/> to select each entry
        /// </summary>
        /// <typeparam name="TOut">The output type</typeparam>
        /// <typeparam name="TIn">The input type</typeparam>
        /// <param name="array">The array to select from</param>
        /// <param name="selector">The <see cref="Func{TIn, TOut}"/> to select each entry</param>
        /// <returns><see cref="IReadOnlyCollection{TOut}"/></returns>
        public static IReadOnlyCollection<TOut> SelectReadOnly<TOut, TIn>(this TIn[] array, Func<TIn, TOut> selector)
        {
            TOut[] outArray = new TOut[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                outArray[i] = selector(array[i]);
            }

            return Array.AsReadOnly(outArray);
        }
    }
}
