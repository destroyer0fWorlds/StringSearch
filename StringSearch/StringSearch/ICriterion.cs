using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    public interface ICriterion
    {
        string Name { get; set; }

        LogicOperatorType LogicOperator { get; set; }
    }
}
