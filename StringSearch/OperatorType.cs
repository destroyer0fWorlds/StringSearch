﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    /// <summary>
    /// Supported operations
    /// </summary>
    public enum OperatorType
    {
        Equals = 0,
        NotEquals = 1,
        Like = 2,
        NotLike = 3,
        GreaterThan = 4,
        GreaterThanOrEqualTo = 5,
        LessThan = 6,
        LessThanOrEqualTo = 7,
        IsNull = 8,
        IsNotNull = 9,
        StartsWith = 10,
        DoesNotStartWith = 11,
        EndsWith = 12,
        DoesNotEndWith = 13,
        Between = 14,
        NotBetween = 15,
        And = 16,
        Or = 17
    }
}