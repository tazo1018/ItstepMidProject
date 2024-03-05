using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    internal class MediumMode : IMode
    {
        public readonly int point = 3;
        public int GenerateNumber()
        {
            Random rand = new Random();
            return rand.Next(1, 26);
        }
    }
}
