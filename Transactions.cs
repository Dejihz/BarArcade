using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    internal class Transactions
    {

        public bool Error { get; set; }
        public int Amount { get; set; }

        public Jokes[] Jokes { get; set; }
    }

    public class Jokes
    {
        public string Joke { get; set; }

    }
}


