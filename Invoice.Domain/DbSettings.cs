using Invoice.Domain.Models;
using LinqToDB.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain
{
    public class DbSettings : ILinqToDBSettings
    {
        private ConnectionStringSettings _connectionStringSettings;
        private string _connectionString;
        public DbSettings(ConnectionStringSettings connectionStringSettings)
        {
            _connectionStringSettings = connectionStringSettings;
        }
        public DbSettings(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<IDataProviderSettings> DataProviders
        {
            get { yield break; }
        }

        public string? DefaultConfiguration => "SqlServer";
        public string? DefaultDataProvider => "SqlServer";


        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return new ConnectionStringSettings()
                {
                    Name = _connectionStringSettings?.Name ?? "local",
                    ProviderName = _connectionStringSettings?.ProviderName ?? "SqlServer",
                    ConnectionString = _connectionStringSettings?.ConnectionString ?? _connectionString
                };
            }
        }
    }
}
