
using System.Collections.Generic;

namespace FSM
{
    public static class CollectionHelper
    {
        public static bool IsInList<T>(List<T> list, int index)
        {
            if (list == null || index < 0 || index > list.Count)
            {
                return false;
            }

            return true;
        }
    }
}