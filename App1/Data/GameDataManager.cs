using Aircraft.Entities;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace App1.Data
{
    public class GameDataManager : BaseDataManager<Game>
    {
        public GameDataManager(SQLiteConnection db) : base(db)
        {

        }

        public void Insert(Game game)
        {
            _db.Insert(game);
        }

        public List<Game> Get(string category)
        {
            return _db.Table<Game>().Where(g => g.Category == category).ToList();
        }

        public void Update(Game game)
        {
            _db.Update(game);
        }
    }
}