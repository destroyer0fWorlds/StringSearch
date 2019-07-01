using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    public class Criterion : ICriterion
    {
        public string Name { get; set; }

        public ConditionOperatorType Operator { get; set; }

        public object Value { get; set; }

        public LogicOperatorType LogicOperator { get; set; }

        public Criterion()
        {

        }

        public Criterion(string name)
        {
            this.Name = name;
        }

        public Criterion(string name, ConditionOperatorType @operator, object value)
        {
            this.Name = name;
            this.Operator = @operator;
            this.Value = value;
        }
    }
}
