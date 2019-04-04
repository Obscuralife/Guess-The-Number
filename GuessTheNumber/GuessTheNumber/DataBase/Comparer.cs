using GuessTheNumber.PlayerStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber.DataBase
{
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
