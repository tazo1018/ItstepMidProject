using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    internal interface IMode
    {
        public int GenerateNumber();
    }
}
