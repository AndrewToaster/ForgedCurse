using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Wrapper around the <see cref="CurseJSON.PackageFingerprint"/> class
    /// </summary>
    public class PackageFingerprint : ForgeWrapper<CurseJSON.PackageFingerprint>
    {
        public PackageFingerprint(CurseJSON.PackageFingerprint fingerprint, ForgeClient client) : base(fingerprint, client)
        {
            Matches = WrappedType.exactMatches.SelectReadOnly(x => new FingerprintMatch(x, client));
        }

        /// <summary>
        /// All the fingerprints that were used in this <see cref="PackageFingerprint"/>
        /// </summary>
        public IReadOnlyCollection<long> Fingerprints { get => WrappedType.installedFingerprints; }

        /// <summary>
        /// All the fingerprints that didn't match
        /// </summary>
        public IReadOnlyCollection<long> UnmatchedFingerprints { get => WrappedType.unmatchedFingerprints; }

        /// <summary>
        /// All the fingerprints that were matched
        /// </summary>
        public IReadOnlyCollection<long> MatchedFingerprints { get => WrappedType.exactFingerprints; }

        /// <summary>
        /// All the <see cref="FingerprintMatch"/>es that were found
        /// </summary>
        public IReadOnlyCollection<FingerprintMatch> Matches { get; }
    }

    /// <summary>
    /// Wrapper around the <see cref="CurseJSON.ExactMatch"/> class
    /// </summary>
    public class FingerprintMatch : ForgeWrapper<CurseJSON.ExactMatch>
    {
        public FingerprintMatch(CurseJSON.ExactMatch match, ForgeClient client) : base(match, client)
        {
            File = new AddonFile(match.file, client);
        }

        /// <summary>
        /// The identifier of the project that this fingerprint is associated with
        /// </summary>
        public int ProjectIdentifier { get => WrappedType.id; }

        /// <summary>
        /// The identifier of the file that this fingerprint is associated with
        /// </summary>
        public int FileIdentifier { get => WrappedType.file.id; }

        /// <summary>
        /// The value of this fingerprint
        /// </summary>
        public long Fingerprint { get => WrappedType.file.packageFingerprint; }

        /// <summary>
        /// The <see cref="AddonFile"/> that this fingerprint is associated with
        /// </summary>
        public AddonFile File { get; }

        /// <summary>
        /// The download URL of the <see cref="File"/>
        /// </summary>
        public string DownloadUrl { get => File.DownloadUrl; }
    }
}
