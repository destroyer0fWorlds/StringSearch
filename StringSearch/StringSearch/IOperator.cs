using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    public interface IOperator
    {
        string Token { get; set; }

        OperatorType Type { get; set; }
    }
}
