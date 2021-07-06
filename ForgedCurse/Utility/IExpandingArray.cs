using System;
using System.Collections.Generic;
using System.Text;

namespace ForgedCurse.Utility
{
    public interface IDefferedArray<T>
    {
        T Next();
        IEnumerable<T> NextRange(int length);
    }
}
