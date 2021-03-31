using Invoice.Domain.Models;
using LinqToDB;
using LinqToDB.Data;
using System;

namespace Invoice.Domain
{
    public class Database : DataConnection
    {
        public Database()
        {

        }
        public Database(string connectionStringName = "local") : base(connectionStringName) { }

        public ITable<Model> Model => GetTable<Model>();
        public static Lazy<Database> NewDatabaseInstance => new();
    }
}
