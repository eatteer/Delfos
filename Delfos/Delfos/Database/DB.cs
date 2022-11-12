using System;
using System.Collections.Generic;
using System.Text;
using Delfos.Models;
using SQLite;

namespace Delfos.Database
{
    public class DB
    {
        private SQLiteAsyncConnection connection;

        public DB(string databasePath)
        {
            connection = new SQLiteAsyncConnection(databasePath);
            connection.CreateTableAsync<User>().Wait();
            connection.CreateTableAsync<Note>().Wait();
        }

        public SQLiteAsyncConnection getConnection()
        {
            return connection;
        }
    }
}
