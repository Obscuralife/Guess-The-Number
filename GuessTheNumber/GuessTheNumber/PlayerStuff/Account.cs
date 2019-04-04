using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber.PlayerStuff
{
    class Account
    {
        public ulong Id { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string CreatedTime { get; private set; }
        

        public Account(string name, string password, ulong id = 0)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            CreatedTime = DateTime.Now.ToString("G");
        }
    }
}
