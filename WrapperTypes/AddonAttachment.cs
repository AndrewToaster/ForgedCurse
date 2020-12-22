using System;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Wrapper type around the <see cref="CurseJSON.AddonAttachment"/>
    /// </summary>
    public class AddonAttachment : ForgeWrapper<CurseJSON.AddonAttachment>
    {
        public AddonAttachment(CurseJSON.AddonAttachment attachment, ForgeClient client) : base(attachment, client)
        {
        }

        /// <summary>
        /// The identifier of this addons attachment
        /// </summary>
        public int Identifier { get => WrappedType.id; }

        /// <summary>
        /// The identifier to the addon that owns this <see cref="AddonAttachment"/>
        /// </summary>
        public int ProjectIdentifier { get => WrappedType.projectId; }

        /// <summary>
        /// The description of this attachment
        /// </summary>
        /// <remarks>
        /// From my testing, this is probably just equal to <see cref="string.Empty"/>
        /// </remarks>
        public string Description { get => WrappedType.description; }

        /// <summary>
        /// Whether or not this attachment is default
        /// </summary>
        /// <remarks>
        /// When an attachment is default, it means that it is the thumbnail displayed on CurseForge
        /// </remarks>
        public bool Default { get => WrappedType.isDefault; }

        /// <summary>
        /// The URL for the full quality image
        /// </summary>
        public string Url { get => WrappedType.url; }

        /// <summary>
        /// The URL for the small low quality version of the attachemt (<see cref="Url"/>)
        /// </summary>
        public string ThumbnailUrl { get => WrappedType.thumbnailUrl; }
    }
}