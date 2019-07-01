using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch
{
    public class Operator : IOperator
    {
        public string Token { get; set; }

        public OperatorType Type { get; set; }

        public Operator()
        {
            
        }

        public Operator(string token, OperatorType operation)
        {
            Token = token;
            Type = operation;
        }

        public override string ToString()
        {
            return this.Token;
        }

        public override bool Equals(object obj)
        {
            var otherOp = (Operator)obj;
            return this.Type == otherOp.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 38826312;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Token);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }
}
