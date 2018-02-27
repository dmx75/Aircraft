using Aircraft.Entities;
using App1.Extensions;
using Dropbox.Api;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App1.Data
{
    public class DataEntryPoint
    {
        private List<Game> _games = new List<Game>();
        private Dictionary<string, int> _dico = new Dictionary<string, int>();
        SQLiteConnection _db;

        public ImageDataManager Images { get; set; }
        public GameDataManager Games { get; set; }

        private static volatile DataEntryPoint instance;
        private static object syncRoot = new Object();

        private DataEntryPoint()
        {
            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
            _db = new SQLiteConnection(dbPath);

            Images = new ImageDataManager(_db);
            Games = new GameDataManager(_db);
        }

        public static DataEntryPoint Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataEntryPoint();
                    }
                }

                return instance;
            }
        }
        public void Init()
        {
            if (Images.Count == 0)
            {
                var task = Task.Run(Download);
                task.Wait();
            }
        }

        public async Task Download()
        {
            using (var dbx = new DropboxClient("C7mpgQEgUaAAAAAAAAAAHmzOsxCfjuEU8CRZfABoEKIrPVCIA5u5tugVHPUi-awt"))
            {
                var list = await dbx.Files.ListFolderAsync(string.Empty, true);

                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    var folders = item.PathDisplay.GetFolders();

                    var category = folders[1];
                    var gameName = folders[2];

                    IncrementDico(category, gameName);
                    using (var response = await dbx.Files.DownloadAsync(item.PathDisplay))
                    {
                        var byteArray = await response.GetContentAsByteArrayAsync();
                        Images.Insert(byteArray, item.Name.RemoveExtension(), category, gameName);

                        CreateGame(category, gameName);

                    }
                }
            }

            InsertGameDb();
        }

        private void InsertGameDb()
        {
            foreach (var game in _games)
            {
                var key = game.Category + game.Name;
                var total = _dico[key];
                game.Total = total;

                Games.Insert(game);
            }
        }

        private void IncrementDico(string category, string gameName)
        {
            var key = category + gameName;
            if (_dico.Any(kvp => kvp.Key == key))
            {
                var value = _dico[key];
                value++;

                _dico[key] = value;
            }
            else
            {
                _dico.Add(key, 1);
            }
        }

        private void CreateGame(string category, string name)
        {
            var game = new Game();
            game.Category = category;
            game.Name = name;
            if (!_games.Any(g => g.Equals(game)))
            {
                _games.Add(game);
            }
        }
    }
}