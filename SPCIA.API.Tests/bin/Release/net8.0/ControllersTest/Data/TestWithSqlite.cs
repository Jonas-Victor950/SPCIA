using BRCSystem.ClassLibrary.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SPCIA.API.Tests.Data
{
    public class TestWithSqlite : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly DataContext DbContext;

        protected TestWithSqlite()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<DataContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new DataContext(options);
            DbContext.Database.EnsureCreated();


        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}