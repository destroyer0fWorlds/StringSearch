using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    public class NestedCriterion : ICriterion
    {
        public string Name { get; set; }

        public IEnumerable<ICriterion> Criteria { get; set; }

        public LogicOperatorType LogicOperator { get; set; }

        public NestedCriterion()
        {
            this.Name = "Nested";
        }
    }
}
