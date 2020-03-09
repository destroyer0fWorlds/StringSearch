using System;
using System.Collections.Generic;
using System.Linq;
using StringSearch.Tokens;

namespace StringSearch.Groups
{
    /// <summary>
    /// Parses a string into logical groups
    /// </summary>
    class GroupParser
    {
        private readonly HashSet<IOperator> _operators;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupParser"/> class
        /// </summary>
        /// <param name="operators"></param>
        public GroupParser(HashSet<IOperator> operators)
        {
            _operators = operators;
        }

        /// <summary>
        /// Parses the supplied string into a hierarchy of conditions and operators
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEnumerable<IGroup> Parse(string value)
        {
            return this.RecursiveParse(value);
        }

        /// <summary>
        /// Recursively parse a string value into groups
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private IEnumerable<IGroup> RecursiveParse(string value)
        {
            value = value ?? string.Empty;
            var groups = this.IdentifyGroups(value);

            var iGroups = new List<IGroup>();
            foreach (var group in groups)
            {
                IGroup iGroup = null;
                if (group.StartsWith("(") && group.EndsWith(")"))
                {
                    iGroup = new CriterionGroup() { Index = value.IndexOf(group), Value = group };
                    if (group.StartsWith("(("))
                    {
                        // Nested group
                        var subGroup = group.Substring(1);
                        subGroup = subGroup.Substring(0, subGroup.Length - 1);
                        iGroup.NestedGroups = new List<IGroup>(this.RecursiveParse(subGroup));
                    }
                    else
                    {
                        // Single group
                        iGroup.Tokens = new List<IToken>(this.ParseTokens(iGroup));
                    }
                }
                else if (group.StartsWith("[") && group.EndsWith("]"))
                {
                    // Logic group
                    iGroup = new LogicGroup() { Index = value.IndexOf(group), Value = group };
                    iGroup.Tokens = new List<IToken>(this.ParseTokens(iGroup));
                }
                else
                {
                    var unknownGroupMessage = $"Invalid format '{group}'. Conditions must be surrounded by parentheses and operators must be surrounded by square braces i.e. (condition[operator]value)";
                    throw new FormatException(unknownGroupMessage);
                }

                if (iGroup != null)
                {
                    iGroups.Add(iGroup);
                }
            }

            return iGroups;
        }

        /// <summary>
        /// Process a string, attempting to break it into more recognizable components
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private IEnumerable<string> IdentifyGroups(string value)
        {
            var andOp = _operators.Single(i => i.Type == OperatorType.And);
            var andValue = $"[{andOp.Value}]";
            var ands = this.Split(new string[] { value }, andValue);

            var orOp = _operators.Single(i => i.Type == OperatorType.Or);
            var orValue = $"[{orOp.Value}]";
            var andsAndOrs = this.Split(ands, orValue);

            // Look for criteria groups
            var groups = new List<string>();
            for (int i = 0; i < andsAndOrs.Length; i++)
            {
                var component = andsAndOrs[i];
                if (component.StartsWith("(("))
                {
                    // Determine how many parentheses start this component
                    int startingParensCount = 0;
                    foreach (var c in component)
                    {
                        if (c != '(') { break; }
                        startingParensCount++;
                    }

                    // Move to the next component
                    i++;

                    // Find the end component - the one that ends with at least as many parentheses as this one starts with.
                    for (; i < andsAndOrs.Length; i++)
                    {
                        var endComponent = andsAndOrs[i];
                        component += endComponent;
                        if (endComponent.EndsWith("))"))
                        {
                            // Determine how many parentheses end this component
                            int endingParensCount = 0;
                            var endCharacters = endComponent.ToCharArray().Reverse();
                            foreach (var c in endCharacters)
                            {
                                if (c != ')') { break; }
                                endingParensCount++;
                            }

                            if (endingParensCount >= startingParensCount) { break; }
                        }
                    }
                }

                groups.Add(component);
            }

            return groups;
        }

        /// <summary>
        /// Further slit a collection of strings into multiple sub strings using the supplied value
        /// </summary>
        /// <param name="values"></param>
        /// <param name="splitValue"></param>
        /// <returns></returns>
        private string[] Split(string[] values, string splitValue)
        {
            var split = new List<string>();
            foreach (var val in values)
            {
                var items = val.Split(new string[] { splitValue }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < items.Length; i++)
                {
                    split.Add(items[i]);
                    if (i < items.Length - 1)
                    {
                        split.Add(splitValue);
                    }
                }
            }
            return split.ToArray();
        }

        /// <summary>
        /// Converts a group's value into recognizable tokens
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private IEnumerable<IToken> ParseTokens(IGroup group)
        {
            ITokenParser tokenParser = null;
            if (group is CriterionGroup)
            {
                var groupIdentifier = new GroupIdentifier(group, _operators);
                if (groupIdentifier.IsRangeCondition())
                {
                    tokenParser = new RangeTokenParser(_operators);
                }
                else if (groupIdentifier.IsSingleCondition())
                {
                    tokenParser = new SingleTokenParser(_operators);
                }
            }
            else
            {
                tokenParser = new LogicTokenParser(_operators);
            }

            if (tokenParser != null) { return tokenParser.Parse(group.Value); }
            return null;
        }
    }
}
