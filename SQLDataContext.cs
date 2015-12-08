using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Linq.Expressions;

namespace RevStack.SQL
{
    public class SQLDataContext
    {
        private readonly SQLServerDataProvider _database;

        public string ConnectionString { get; set; }

        public SQLDataContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _database = new SQLServerDataProvider(connectionString);
            ConnectionString = connectionString;
        }
        
        public SQLDataContext(string connectionString)
        {
            _database = new SQLServerDataProvider(connectionString);
            ConnectionString = connectionString;
        }

        internal SQLServerDataProvider DataProvider
        {
            get
            {
                return _database;
            }
        }
    }
}
