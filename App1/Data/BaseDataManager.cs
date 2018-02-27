using System;
using SQLite;

namespace App1.Data
{
    public class BaseDataManager<T> where T : new()
    {
        protected SQLiteConnection _db;

        public int Count
        {
            get
            {
                if (_db == null)
                {
                    return 0;
                }
                return _db.Table<T>().Count();
            }
        }

        public BaseDataManager(SQLiteConnection db)
        {
            _db = db ?? throw new ArgumentNullException("db");
            _db.CreateTable<T>();
        }
    }
}