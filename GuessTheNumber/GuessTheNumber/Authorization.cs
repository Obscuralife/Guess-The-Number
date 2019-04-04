using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    static class Authorization
    {
        public static bool SignUp(string name, string password)
        {            
            return JsonDataBase.SaveAccount(new Account(name, password));
        }

        public static bool LogIn(string name, string password)
        {
            return JsonDataBase.Contains(name, password);
        }
    }
}
