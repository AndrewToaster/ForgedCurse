using System;
using System.Collections.Generic;
using System.Text;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// A wrapper around the <see cref="CurseJSON.AddonAuthor"/> class
    /// </summary>
    public class AddonAuthor : ForgeWrapper<CurseJSON.AddonAuthor>
    {
        public AddonAuthor(CurseJSON.AddonAuthor author, ForgeClient client) : base(author, client)
        {
        }

        /// <summary>
        /// The name of this author
        /// </summary>
        public string Name { get => WrappedType.name; }

        /// <summary>
        /// CurseForge URL for this author
        /// </summary>
        public string ProfileUrl { get => WrappedType.url; }

        /// <summary>
        /// The user identifier for this author
        /// </summary>
        /// <remarks>
        /// This is a part of the <see cref="ProfileUrl"/>. https://www.curseforge.com/members/{UserIdentifier}-{Name}
        /// </remarks>
        public long UserIdentifier { get => WrappedType.userId; }

        /// <summary>
        /// The twitch identifier for this author
        /// </summary>
        /// <remarks>
        /// This a remnant from the time where CurseForge was on Twitch, rather than on Overwolf
        /// </remarks>
        public long TwitchIdentifer { get => WrappedType.twitchId; }
    }
}
