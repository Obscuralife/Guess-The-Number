using System;
using System.ComponentModel.DataAnnotations;

namespace Guess_The_Number.PlayerStuff
{
    class UserAccount
    {
        public ulong Id { get; private set; }

        [Required(ErrorMessage = "Name can not be empty")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name length should be more than 3 and less than 20")]
        public string Name { get; private set; }
        [Required(ErrorMessage = "Password can not be empty")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password length should be more than 5 and less than 20")]
        public string Password { get; private set; }
        public string CreatedTime { get; private set; }

        public UserAccount(string name, string password, ulong id = 0)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.CreatedTime = DateTime.Now.ToString("G");
        }
    }
}
