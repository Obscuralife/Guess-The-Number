using Guess_The_Number.PlayerStuff;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Guess_The_Number.DataBase
{
    class JsonDataBase
    {
        public string Repository { get; private set; }
        public string CurrentAccountPath { get; private set; }
        public UserAccount CurrentAccount { get; private set; }

        private static JsonDataBase _this;

        private JsonDataBase()
        {
            var applacationPath = Environment.CurrentDirectory;
            Repository = Path.Combine(applacationPath, "JsonDataBase");
            Directory.CreateDirectory(Repository);
        }

        public static JsonDataBase GetDataBase()
        {
            if (_this is null)
            {
                _this = new JsonDataBase();
            }
            return _this;
        }

        public bool CreateNewUserAccount(UserAccount account)
        {
            var newAccount = new UserAccount(account.Name, account.Password, GetId());
            var fileName = $"UserAccount {newAccount.Id}.json";
            CurrentAccountPath = Path.Combine(Repository, fileName);

            if (IsDbContains(account))
            {
                return false;
            }
            else
            {
                var json = JsonConvert.SerializeObject(newAccount);
                using (var stream = new StreamWriter(CurrentAccountPath))
                {
                    stream.WriteLine(json);
                }
                CurrentAccount = newAccount;
                return true;
            }
        }

        public void AddHistoryToCurrentAccount(PlayerHistory history)
        {
            var jsonHistory = JsonConvert.SerializeObject(history, Formatting.Indented);
            using (var stream = new StreamWriter(CurrentAccountPath, true))
            {
                stream.WriteLine(jsonHistory);
            }
        }

        public bool IsDbContains(UserAccount account)
        {
            foreach (var item in Directory.GetFiles(Repository))
            {
                UserAccount temp = null;
                using (var sr = new StreamReader(Path.Combine(Repository, item)))
                {
                    var json = sr.ReadLine();
                    temp = JsonConvert.DeserializeObject<UserAccount>(json);
                }
                if (account.Name == temp.Name && account.Password == temp.Password)
                {
                    CurrentAccountPath = Path.Combine(Repository, item);
                    CurrentAccount = account;
                    return true;
                }
            }
            return false;
        }

        private ulong GetId()
        {
            var files = (Directory.GetFiles(Repository)).Where(p => p.Contains("UserAccount"));
            return (ulong)files.LongCount() + 1;
        }
    }
}
