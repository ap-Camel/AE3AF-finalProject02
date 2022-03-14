using MovieApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services
{
    public class LocalDataBase
    {

        private readonly SQLiteAsyncConnection dbConnection;

        public LocalDataBase(string dbPath)
        {
            dbConnection = new SQLiteAsyncConnection(dbPath);

            Task.Run(() => CreateTables()).Wait();
            Task.Run(() => InitializeData()).Wait();
        }

        private async Task CreateTables()
        {
            await dbConnection.CreateTableAsync<FavouriteMovie>();
        }

        private async Task InitializeData()
        {

        }

        public async Task<List<T>> GetItems<T>() where T : new()
        {
            return await dbConnection.Table<T>().ToListAsync();
        }

        public async Task<bool> RowExists(int id, string table)
        {


            bool rows = await dbConnection.ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT 1 FROM " + table + " WHERE char_id=?)", id);

            return rows;


        }

        public async Task<bool> AddItem<T>(T item) where T : new()
        {
            int rows = await dbConnection.InsertAsync(item);
            return (rows == 1);
        }

        public async Task<bool> UpdateItem<T>(T item) where T : new()
        {
            int rows = await dbConnection.UpdateAsync(item);
            return (rows == 1);
        }

        public async Task<bool> DeleteItem<T>(T item) where T : new()
        {
            int rows = await dbConnection.DeleteAsync(item);
            return (rows == 1);
        }


    }
}
