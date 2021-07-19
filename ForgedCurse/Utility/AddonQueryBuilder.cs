using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Enumeration;
using ForgedCurse.Json;

namespace ForgedCurse.Utility
{
    public sealed class AddonQueryBuilder
    {
        private const string API_BASE = "https://addons-ecs.forgesvc.net/api/v2/addon/search?";

        public string Version { get; set; }
        public string Name { get; set; }
        public AddonSorting Sorting { get; set; }
        public int Section { get; set; }
        public int Category { get; set; }
        public int Amount { get; set; }
        public int Offset { get; set; }
        public int Game { get; set; }

        public AddonQueryBuilder WithVersion(string version)
        {
            Version = version;
            return this;
        }

        public AddonQueryBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public AddonQueryBuilder WithAmount(int amount)
        {
            Amount = amount;
            return this;
        }

        public AddonQueryBuilder WithOffset(int offset)
        {
            Offset = offset;
            return this;
        }

        public AddonQueryBuilder WithSorting(AddonSorting sorting)
        {
            Sorting = sorting;
            return this;
        }

        public AddonQueryBuilder WithSection(int sectionId)
        {
            Section = sectionId;
            return this;
        }

        public AddonQueryBuilder WithCategory(int categoryId)
        {
            Category = categoryId;
            return this;
        }

        public AddonQueryBuilder WithGame(int gameId)
        {
            Game = gameId;
            return this;
        }

        public string Build()
        {
            StringBuilder _sb = new();

            _sb.Append(API_BASE)
                .Append("&categoryId=").Append(Category)
                .Append("&gameId=").Append(Game)
                .Append("&sort=").Append((int)Sorting)
                .Append("&index=").Append(Offset)
                .Append("&pageSize=").Append(Amount)
                .Append("&sectionId=").Append(Section);

            if (!string.IsNullOrWhiteSpace(Version))
                _sb.Append("&gameVersion=").Append(Version);

            if (!string.IsNullOrWhiteSpace(Name))
                _sb.Append("&searchFilter=").Append(Name);

            return _sb.ToString();
        }

		public override string ToString()
		{
            return Build();
		}
	}
}
