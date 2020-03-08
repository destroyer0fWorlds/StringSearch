
namespace StringSearch
{
    /// <summary>
    /// Operator type
    /// </summary>
    public enum ConditionOperatorType
    {
        /// <summary>
        /// Is equal to
        /// </summary>
        Equals = 0,
        /// <summary>
        /// Is not equal to
        /// </summary>
        NotEquals = 1,
        /// <summary>
        /// Is like
        /// </summary>
        Like = 2,
        /// <summary>
        /// Is not like
        /// </summary>
        NotLike = 3,
        /// <summary>
        /// Is greater than
        /// </summary>
        GreaterThan = 4,
        /// <summary>
        /// Is greater than or equal to
        /// </summary>
        GreaterThanOrEqualTo = 5,
        /// <summary>
        /// Is less than
        /// </summary>
        LessThan = 6,
        /// <summary>
        /// Is less than or equal to
        /// </summary>
        LessThanOrEqualTo = 7,
        /// <summary>
        /// Is null
        /// </summary>
        IsNull = 8,
        /// <summary>
        /// Is not null
        /// </summary>
        IsNotNull = 9,
        /// <summary>
        /// Starts with
        /// </summary>
        StartsWith = 10,
        /// <summary>
        /// Does not start with
        /// </summary>
        DoesNotStartWith = 11,
        /// <summary>
        /// Ends with
        /// </summary>
        EndsWith = 12,
        /// <summary>
        /// Does not end with
        /// </summary>
        DoesNotEndWith = 13,
        /// <summary>
        /// Is between
        /// </summary>
        Between = 14,
        /// <summary>
        /// Is not between
        /// </summary>
        NotBetween = 15
    }
}
