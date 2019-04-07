using Guess_The_Number.GameEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Guess_The_Number.PlayerStuff
{
    class UsersValidator
    {
        public List<ValidationResult> Results { get; private set; }
        public ValidationContext Context { get; private set; }
        public UserAccount Account { get; private set; }

        public UsersValidator(UserAccount userAccount)
        {
            Results = new List<ValidationResult>();
            Context = new ValidationContext(userAccount);
            Account = userAccount;
        }

        public bool IsValid()
        {
            if (!Validator.TryValidateObject(Account, Context, Results, true))
            {
                foreach (var error in Results)
                {
                    ColorEngine.Red();
                    Console.WriteLine(error.ErrorMessage);
                }
                return false;
            }
            return true;
        }
    }
}
