using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    internal static class Utilities
    {
        /// <summary>
        /// Code sugar for reducing the length of checking a async <see cref="HttpResponseMessage"/>'s status code
        /// </summary>
        /// <param name="responseMessage">The message to check</param>
        /// <returns>Same message as <paramref name="responseMessage"/></returns>
        internal static Task<HttpResponseMessage> CheckSuccess(this Task<HttpResponseMessage> responseMessage)
        {
            return responseMessage.ContinueWith(task => task.Result.EnsureSuccessStatusCode());
        }
    }
}
