using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Infrastructure;

namespace NadinSoft.Test.Mocks
{
    public static class DatabaseFactory
    {
        public static NadinDbContext CreateNadinDb()
        {
            return new TestDbContext().CreateNadinDb();
        }
    }

    public class TestDbContext
    {
        public SqliteConnection Connection = new("Filename=:memory:");

        public NadinDbContext CreateNadinDb()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            var db = new NadinDbContext(new DbContextOptionsBuilder()
                .ConfigureWarnings(x => x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.AmbientTransactionWarning))
                .UseSqlite(Connection).Options, () => Connection.Dispose());

            db.Database.EnsureCreated();

            return db;
        }
    }
}