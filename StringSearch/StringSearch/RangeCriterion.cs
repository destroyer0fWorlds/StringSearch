using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    public class RangeCriterion : ICriterion
    {
        public string Name { get; set; }

        public ConditionOperatorType Operator { get; set; }

        public object StartValue { get; set; }

        public object EndValue { get; set; }

        public LogicOperatorType LogicOperator { get; set; }

        public RangeCriterion()
        {

        }

        public RangeCriterion(string name)
        {
            this.Name = name;
        }

        public RangeCriterion(string name, ConditionOperatorType @operator, object startValue, object endValue)
        {
            this.Name = name;
            this.Operator = @operator;
            this.StartValue = startValue;
            this.EndValue = endValue;
        }
    }
}
