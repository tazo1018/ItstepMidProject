using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator

{
    internal class Number
    {
        public float Value { get; set; }
        public bool Isvalid { get; set; }

        public Number()
        {
            
        }

        public Number(float value, bool isvalid)
        {
            Value = value;
            Isvalid = isvalid;
        }
        public static Number operator -(Number first, Number second)
        {
            Number result = new Number();
            result.Value = first.Value - second.Value;
            return result;
        }

        public static Number operator +(Number first, Number second)
        {
            Number result = new Number();
            result.Value = first.Value + second.Value;
            return result;
        }

        public static Number operator /(Number first, Number second)
        {
            Number result = new Number();
            if (second.Value == 0) {
                second.Isvalid = false;
                throw new DivideByZeroException();
            } else
            {
                result.Value = first.Value / second.Value;
            }
            return result;
        }

        public static Number operator *(Number first, Number second)
        {
            Number result = new Number();
            result.Value = first.Value * second.Value;
            
            return result;
        }

    }
}
