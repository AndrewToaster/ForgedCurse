using System;
using System.Collections.Generic;
using System.Text;

namespace ForgedCurse.Utility
{
    public interface IExpandingArray<T>
    {
        T Next();
        T[] NextRange(int length);
    }
}
