using ForgedCurse.Utility;
using ForgedCurse.WrapperTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ForgedCurse
{
    /// <summary>
    /// Class for fingerprinting addons / files
    /// </summary>
    public static class Fingerprinting
    {
        /// <summary>
        /// Computes a fingerprint of a JAR file
        /// </summary>
        /// <param name="jarFile">The path pointing towards a JAR file</param>
        /// <returns>Computated fingerprint</returns>
        public static long ComputeFingerprint(string jarFile)
        {
            if (!File.Exists(jarFile) || Path.GetExtension(jarFile) != ".jar")
                throw new ArgumentException("The specified path either doesn't exist or isn't pointing to a JAR file", nameof(jarFile));

            return MurmurHash2.HashNormal(File.ReadAllBytes(jarFile));
        }

        /// <summary>
        /// Computes a fingerprint of a JAR file
        /// </summary>
        /// <param name="jarStream">The contents of the JAR file</param>
        /// <returns>Computated fingerprint</returns>
        public static long ComputeFingerprint(Span<byte> jarStream)
        {
            return MurmurHash2.HashNormal(jarStream.ToArray());
        }
    }
}
