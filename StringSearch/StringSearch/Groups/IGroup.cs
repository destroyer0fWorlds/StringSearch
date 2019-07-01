using System;
using System.Collections.Generic;
using System.Text;
using StringSearch.Tokens;

namespace StringSearch.Groups
{
    interface IGroup
    {
        int Index { get; set; }

        string Value { get; set; }

        List<IGroup> NestedGroups { get; set; }

        List<IToken> Tokens { get; set; }

        bool HasNestedGroups { get; }
    }
}
