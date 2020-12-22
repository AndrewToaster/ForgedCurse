using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Wrapper around the <see cref="CurseJSON.AddonInfo"/> class
    /// </summary>
    public class Addon : ForgeWrapper<CurseJSON.AddonInfo>
    {
        /// <summary>
        /// Constructs a new instance of <see cref="Addon"/>
        /// </summary>
        /// <param name="addonInfo">The <see cref="CurseJSON.AddonInfo"/> to wrap around</param>
        /// <param name="client"></param>
        public Addon(CurseJSON.AddonInfo addonInfo, ForgeClient client) : base(addonInfo, client)
        {
            Authors = WrappedType.authors.SelectReadOnly(auth => new AddonAuthor(auth, Client));
            Attachments = WrappedType.attachments.SelectReadOnly(attach => new AddonAttachment(attach, Client));
            LatestFiles = WrappedType.latestFiles.SelectReadOnly(file => new AddonFile(file, Client));
            Files = WrappedType.gameVersionLatestFiles.SelectReadOnly(file => new AddonFileLite(file, Client));
            Statistics = new AddonStatistics(addonInfo);
        }

        /// <summary>
        /// The name of this addon
        /// </summary>
        public string Name { get => WrappedType.name; }

        /// <summary>
        /// Provides a small description for this addon
        /// </summary>
        public string Summary { get => WrappedType.summary; }

        /// <summary>
        /// The unique identifier of this addon
        /// </summary>
        public int Identifier { get => WrappedType.id; }

        /// <summary>
        /// The authors that are associated with this addon
        /// </summary>
        public IReadOnlyCollection<AddonAuthor> Authors { get; }

        /// <summary>
        /// The attachments that come with this addon (e.g. Images)
        /// </summary>
        public IReadOnlyCollection<AddonAttachment> Attachments { get; }

        /// <summary>
        /// Returns the 3 latest addon releases
        /// </summary>
        /// <remarks>
        /// <see cref="LatestFiles"/> has considerably more information about each element than <see cref="Files"/>.
        /// This is the reason for only having 3 entries (due to the size limitation)
        /// </remarks>
        public IReadOnlyCollection<AddonFile> LatestFiles { get; }

        /// <summary>
        /// Returns the all the addon releases
        /// </summary>
        /// <remarks>
        /// <see cref="Files"/> has considerably less information about each element than <see cref="LatestFiles"/>
        /// This is the reason for having all entries (due to the size limitation)
        /// </remarks>
        public IReadOnlyCollection<AddonFileLite> Files { get; }

        /// <summary>
        /// The date this addon was created (first ever release date)
        /// </summary>
        public DateTime CreatedAt { get => WrappedType.dateCreated; }

        /// <summary>
        /// The date this addon was released
        /// </summary>
        /// <remarks>
        /// The release date is not similiar to the date created, rather the date it was last modified
        /// </remarks>
        public DateTime ReleasedAt { get => WrappedType.dateReleased; }

        /// <summary>
        /// The date this addon was last modified
        /// </summary>
        public DateTime ModifiedAt { get => WrappedType.dateModified; }

        /// <summary>
        /// Whether or not the addon is featured
        /// </summary>
        public bool Featured { get => WrappedType.isFeatured; }

        /// <summary>
        /// Whether or not this addon is available
        /// </summary>
        /// <remarks>
        /// Addon becomes unavailable due to multiple reaons (created marked it that way; it was abandoned; ...). 
        /// Either way this means it will not show-up in search can be only accessed using its url
        /// </remarks>
        public bool Available { get => WrappedType.isFeatured; }

        /// <summary>
        /// Whether or not the addon is experimental
        /// </summary>
        public bool Experimental { get => WrappedType.isFeatured; }

        /// <summary>
        /// The CurseForge URL for this addon
        /// </summary>
        public string Website { get => WrappedType.websiteUrl; }

        /// <summary>
        /// The statistics for this addon
        /// </summary>
        public AddonStatistics Statistics { get; }

        /// <summary>
        /// Returns the HTML description of this addon
        /// </summary>
        /// <remarks>
        /// This method accesses the <see cref="ForgeClient.GetAddonDescriptionAsync(int)"/>, rather then a field from <see cref="CurseJSON.AddonInfo"/>
        /// </remarks>
        /// <returns>The HTML text for the description</returns>
        public async Task<string> GetDescriptionAsync()
        {
            return await Client.GetAddonDescriptionAsync(Identifier);
        }

        /// <summary>
        /// Returns the HTML description of this addon
        /// </summary>
        /// <remarks>
        /// This method accesses the <see cref="ForgeClient.GetAddonDescription(int)"/>, rather then a field from <see cref="CurseJSON.AddonInfo"/>
        /// </remarks>
        /// <returns>The HTML text for the description</returns>
        public string GetDescription()
        {
            return Client.GetAddonDescription(Identifier);
        }
    }
}
