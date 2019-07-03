﻿using System;
using System.Collections.Generic;
using System.Linq;
using StringSearch.Tokens;

namespace StringSearch.Groups
{
    /// <summary>
    /// Parses a string into logical groups
    /// </summary>
    class Grouper
    {
        private readonly HashSet<IOperator> _operators;

        /// <summary>
        /// Initializes a new instance of the <see cref="Grouper"/> class
        /// </summary>
        /// <param name="operators"></param>
        public Grouper(HashSet<IOperator> operators)
        {
            _operators = operators;
        }

        /// <summary>
        /// Parses the supplied string into a hierarchy of conditions and operators
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEnumerable<IGroup> Group(string value)
        {
            var characterSets = new List<CharacterSet>()
            {
                new CharacterSet('(', ')'),
                new CharacterSet('[', ']')
            };

            return this.IdentifyGroups(value, characterSets);
        }

        /// <summary>
        /// Recursively identify groups
        /// </summary>
        /// <param name="value"></param>
        /// <param name="characterSets"></param>
        /// <returns></returns>
        private IEnumerable<IGroup> IdentifyGroups(string value, IEnumerable<CharacterSet> characterSets)
        {
            value = value ?? string.Empty;
            this.ValidateInput(value);

            var characterGroups = new List<IGroup>();
            var stack = new Stack<KeyValuePair<int, char>>();
            var characters = value.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                var character = characters[i];

                if (characterSets.Any(x => x.StartCharacter == character))
                {
                    stack.Push(new KeyValuePair<int, char>(i, character));
                }
                else if (characterSets.Any(x => x.EndCharacter == character))
                {
                    var characterSet = characterSets.First(x => x.EndCharacter == character);

                    if (stack.Count == 0)
                    {
                        // Error - No beginning characters
                        throw new FormatException($"Invalid filter string. Found '{characterSet.EndCharacter}' without a matching opening character.");
                    }

                    var keyValuePair = stack.Pop();
                    if (characterSet.StartCharacter != keyValuePair.Value)
                    {
                        // Error - End without a matching beginning
                        throw new FormatException($"Invalid filter string. Found '{characterSet.EndCharacter}' without a matching opening character.");
                    }

                    if (stack.Count == 0)
                    {
                        int startIndex = keyValuePair.Key;
                        int length = i - startIndex + 1;

                        IGroup group = null;
                        if (character == ')')
                        {
                            group = new CriterionGroup() { Index = i, Value = value.Substring(startIndex, length) };
                        }
                        else if (character == ']')
                        {
                            group = new LogicGroup() { Index = i, Value = value.Substring(startIndex, length) };
                        }

                        if (group != null)
                        {
                            if (!this.IsNestedGroup(group, characterSets))
                            {
                                group.Tokens = new List<IToken>(this.IdentifyTokens(group));
                            }
                            characterGroups.Add(group);
                        }
                    }
                }
            }

            if (stack.Count > 0)
            {
                // Error - Beginning without an end
                var quotedCharacters = stack.Select(i => $"'{i.Value}'");
                var beginningCharacterList = string.Join(", ", quotedCharacters);
                throw new FormatException($"Invalid filter string. Found the following without matching closing characters: {beginningCharacterList}");
            }

            // Process nested groups
            foreach (var group in characterGroups)
            {
                if (this.IsNestedGroup(group, characterSets))
                {
                    var nestedValue = group.Value;
                    nestedValue = nestedValue.Substring(1, nestedValue.Length - 2);
                    group.NestedGroups = this.IdentifyGroups(nestedValue, characterSets).ToList();
                }
            }

            return characterGroups;
        }

        /// <summary>
        /// Validate the string input is at least vaguely formatted correctly
        /// </summary>
        /// <param name="value"></param>
        private void ValidateInput(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var hasOpenParentheses = value.Contains("(");
                var hasClosedParentheses = value.Contains(")");
                if (!hasOpenParentheses && !hasClosedParentheses)
                {
                    throw new FormatException("Invalid filter string. No tokens found. Filter conditions must be enclosed in parentheses.");
                }
                else if (!hasOpenParentheses)
                {
                    throw new FormatException("Invalid filter string. Filter condition is missing one or more opening parenthesis.");
                }
                else if (!hasClosedParentheses)
                {
                    throw new FormatException("Invalid filter string. Filter condition is missing one or more closing parenthesis.");
                }
            }
        }

        /// <summary>
        /// Determines whether the group is nested
        /// </summary>
        /// <param name="group"></param>
        /// <param name="characterSets"></param>
        /// <returns></returns>
        private bool IsNestedGroup(IGroup group, IEnumerable<CharacterSet> characterSets)
        {
            var isNestedGroup = false;
            foreach (var characterSet in characterSets)
            {
                if (group.Value.Contains($"{characterSet.StartCharacter}{characterSet.StartCharacter}"))
                {
                    isNestedGroup = true;
                    break;
                }
            }
            return isNestedGroup;
        }

        /// <summary>
        /// Converts a group's value into recognizable tokens
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private IEnumerable<IToken> IdentifyTokens(IGroup group)
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

            // Return
            if (tokenParser != null)
            {
                return tokenParser.Parse(group.Value);
            }
            return null;
        }
    }
}