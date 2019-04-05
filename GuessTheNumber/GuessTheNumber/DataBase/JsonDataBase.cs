using GuessTheNumber.PlayerStuff;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GuessTheNumber.DataBase
{
    static class JsonDataBase
    {
        private static string repository;
        private static Account currentAccount;
        private static string currentAccountPath;
        private static Comparer comparer = new Comparer();

        static JsonDataBase()
        {
            var applacationPath = Environment.CurrentDirectory;
            repository = Path.Combine(applacationPath, "JsonDataBase");
            Directory.CreateDirectory(repository);
        }

        public static void AddHistoryToCurrentAccount(PlayerHistory history)
        {
            var jsonHistory = JsonConvert.SerializeObject(history, Formatting.Indented);
            using (var stream = new StreamWriter(currentAccountPath,true))
            {
                stream.WriteLine(jsonHistory);
            }
        }

        public static bool SaveAccount(Account account)
        {
            var newAccount = new Account(account.Name, account.Password, GetId());
            var fileName = $"User {newAccount.Id}.json";
            currentAccountPath = Path.Combine(repository, fileName);

            if (Contains(account))
            {
                return false;
            }
            else
            {
                var json = JsonConvert.SerializeObject(newAccount);
                using (var stream = new StreamWriter(currentAccountPath))
                {
                    stream.WriteLine(json);
                }
                currentAccount = newAccount;
                return true;
            }
        }

        public static bool Contains(Account account)
        {
            foreach (var item in Directory.GetFiles(repository))
            {
                Account temp = null;
                using (var sr = new StreamReader(Path.Combine(repository, item)))
                {
                    var json = sr.ReadLine();
                    temp = JsonConvert.DeserializeObject<Account>(json);
                }
                if (account.Name == temp.Name)
                {
                    currentAccountPath = Path.Combine(repository, item);
                    currentAccount = account;
                    return true;
                }

            }
            return false;
        }

        public static bool Contains(string name, string password)
        {
            foreach (var item in Directory.GetFiles(repository))
            {
                Account temp = null;
                using (var sr = new StreamReader(Path.Combine(repository, item)))
                {
                    var json = sr.ReadLine();
                    temp = JsonConvert.DeserializeObject<Account>(json);
                }
                if (name == temp.Name && password == temp.Password)
                {
                    currentAccountPath = Path.Combine(repository, item);
                    currentAccount = temp;
                    return true;
                }
            }
            return false;
        }

        private static ulong GetId()
        {
            var files = Directory.GetFiles(repository);
            return (ulong)files.LongLength + 1;            
        }
    }    
}
