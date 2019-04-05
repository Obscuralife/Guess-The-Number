using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber.PlayerStuff
{
    class PlayerHistory
    {
        public List<string> History { get; private set; } = new List<string>();
        public void AddAction(string message) => History.Add(DateTime.Now.ToString("hh:mm:ss ") + message);
    }
}
