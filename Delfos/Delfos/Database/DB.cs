using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<T>> FindAllAsync<T>() where T : new()
        {
            List<T> results = await connection.Table<T>().ToListAsync();
            return results;
        }

        public async Task<int> SaveAsync<T>(T model)
        {
            int rowsAdded = await connection.InsertAsync(model);
            return rowsAdded;
        }

        public async Task<int> UpdateAsync<T>(T model)
        {
            int rowsUpdated = await connection.UpdateAsync(model);
            return rowsUpdated;
        }

        public async Task<int> DeleteAsync<T>(T model)
        {
            int rowsDeleted = await connection.DeleteAsync(model);
            return rowsDeleted;
        }

        public async Task<List<T>> QueryAsync<T>(string query) where T : new()
        {
            List<T> results = await connection.QueryAsync<T>(query);
            return results;
        }
    }
}
