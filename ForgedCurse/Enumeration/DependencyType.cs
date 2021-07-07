using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgedCurse.Enumeration
{
    public enum DependencyType
    {
        EmbededLibrary = 0,
        Optional = 1,
        Required = 2,
        Tool = 3,
        Incompatible = 4,
        Include = 5
    }
}
