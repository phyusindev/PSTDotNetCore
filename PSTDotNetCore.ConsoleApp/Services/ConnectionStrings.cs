using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTDotNetCore.ConsoleApp.Services
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "PSTDotNetCore",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true

        };
    }
}
