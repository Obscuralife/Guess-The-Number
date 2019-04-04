using GuessTheNumber.DataBase;

namespace GuessTheNumber.PlayerStuff
{
    static class Authorization
    {
        public static bool SignUp(string name, string password) => JsonDataBase.SaveAccount(new Account(name, password));
        public static bool LogIn(string name, string password) => JsonDataBase.Contains(name, password);
    }
}

