using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Utility
{
    public class ConnectionString
    {
        private static string cName = "Data Source=DESKTOP-4RB7SO6\\TEW_SQLEXPRESS;Database=OneCarDb;Integrated Security=SSPI";
            //"Data Source=.; Initial Catalog=OneCarDb;User ID=sa;Password=123";
        //"Data Source=localhost\\SQLEXPRESS;Database=databasename;Integrated Security=SSPI";
        public static string CName
        {
            get => cName;
        }
    }
}
