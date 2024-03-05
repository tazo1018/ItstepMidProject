using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    internal class HardMode : IMode
    {
        public readonly int point = 5;
        public int GenerateNumber()
        {
            Random rand = new Random();
            return rand.Next(1, 51);
        }
    }
}
