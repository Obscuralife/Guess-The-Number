using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    static class JsonDataBase
    {
        private static string repository;
        private static Account currentAccount;
        private static Comparer comparer = new Comparer();

        static JsonDataBase()
        {
            var applacationPath = Environment.CurrentDirectory;
            repository = Path.Combine(applacationPath, "JsonDataBase");
            Directory.CreateDirectory(repository);
        }

        public static bool SaveAccount(Account account)
        {
            var newAccount = new Account(account.Name, account.Password, GetId());
            var fileName = $"User {newAccount.Id}.json";
            var filePath = Path.Combine(repository, fileName);

            if (Contains(account))
            {
                return false;
            }
            else
            {
                var json = JsonConvert.SerializeObject(newAccount);
                using (var stream = new StreamWriter(filePath))
                {
                    stream.Write(json);
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
                    var json = sr.ReadToEnd();
                    temp = JsonConvert.DeserializeObject<Account>(json);
                }
                if (account.Name == temp.Name)
                {
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
                    var json = sr.ReadToEnd();
                    temp = JsonConvert.DeserializeObject<Account>(json);
                }
                if (name == temp.Name && password == temp.Password)
                {
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

    class Comparer : IEqualityComparer<Account>
    {
        bool IEqualityComparer<Account>.Equals(Account x, Account y)
        {
            if (x.Name == y.Name) { return true; }
            return false;
        }

        int IEqualityComparer<Account>.GetHashCode(Account obj)
        {
            int hashCode = (int)obj.Id ^ obj.Name.Length;
            return hashCode.GetHashCode();
        }
    }
}
