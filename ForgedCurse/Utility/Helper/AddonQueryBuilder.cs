using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Enumeration;

namespace ForgedCurse.Utility.Helper
{
	public static class AddonQueryBuilderHelper
	{
		public static AddonQueryBuilder WithGame(this AddonQueryBuilder builder, ForgeGames game)
		{
			return builder.WithGame((int)game);
		}
	}
}
