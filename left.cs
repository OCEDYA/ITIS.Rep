using System;
using System.Collections.Generic;

namespace Autocomplete;

public class LeftBorderTask
{
    public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        if (right - left <= 1)
            return left;
        
        int mid = left + (right - left) / 2;
        
        if (string.Compare(phrases[mid], prefix, StringComparison.InvariantCultureIgnoreCase) < 0 
            && !phrases[mid].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return GetLeftBorderIndex(phrases, prefix, mid, right);
        else
            return GetLeftBorderIndex(phrases, prefix, left, mid);
    }
}
