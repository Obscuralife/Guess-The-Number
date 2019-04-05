using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber.PlayerStuff
{
    class PlayerHistory
    {
        public Dictionary<string, string> History { get; private set; }
        public PlayerHistory()
        {
            History = new Dictionary<string, string>();
        }
        public void AddAction(string message) => History.Add(DateTime.Now.ToString("hh:mm:ss:ms"), message);

    }
}
