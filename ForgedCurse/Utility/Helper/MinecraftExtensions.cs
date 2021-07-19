using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Enumeration;

namespace ForgedCurse.Utility.Helper
{
    public static class MinecraftExtensions
    {
        public static AddonQueryBuilder WithSection(this AddonQueryBuilder builder, MinecraftSection section)
        {
            return builder.WithSection((int)section);
        }

        public static AddonQueryBuilder WithCategory(this AddonQueryBuilder builder, MinecraftCategory category)
        {
            return builder.WithCategory((int)category);
        }
    }
}
