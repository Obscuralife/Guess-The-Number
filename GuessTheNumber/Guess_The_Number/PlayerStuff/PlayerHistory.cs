using System;
using System.Collections.Generic;

namespace Guess_The_Number.PlayerStuff
{
    class PlayerHistory
    {
        public List<string> Actions { get; private set; } = new List<string>();

        public void AddAction(string message) => Actions.Add(DateTime.Now.ToString("hh:mm:ss ") + message);
    }
}
