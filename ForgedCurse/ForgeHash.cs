using ForgedCurse.Utility;
using System.IO;

namespace ForgedCurse
{
    /// <summary>
    /// Class for hashing addons / files
    /// </summary>
    public static class ForgeHash
    {
        /// <summary>
        /// Computes a hash of a file
        /// </summary>
        /// <param name="file">The path pointing towards the file for hashing</param>
        /// <returns>Computed hash</returns>
        public static uint ComputeHash(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException(file);

            return MurmurHash2.HashNormal(File.ReadAllBytes(file));
        }

        /// <summary>
        /// Computes a hash from a byte array
        /// </summary>
        /// <param name="fileData">The byte data of the file</param>
        /// <returns>Computed fingerprint</returns>
        public static uint ComputeHash(byte[] fileData)
        {
            return MurmurHash2.HashNormal(fileData);
        }
    }
}
