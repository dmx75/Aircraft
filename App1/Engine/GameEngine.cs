using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Graphics;
using App1.Data;
using App1.Entities;
using App1.Extensions;
using Dropbox.Api;

namespace App1.Engine
{
    public sealed class GameEngine
    {
        private static volatile GameEngine instance;
        private static object syncRoot = new Object();

        private GameEngine() { }

        public static GameEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GameEngine();
                    }
                }

                return instance;
            }
        }

        public Dictionary<string, Bitmap> Images { get; set; }
        public List<string> Names { get; set; }

        public void InitGame()
        {
            Images = new Dictionary<string, Bitmap>();
            Names = new List<string>();
            //var manager = new DropboxFileManager("C7mpgQEgUaAAAAAAAAAAGoGa6oPB-nPyWM5CBhAN_joOESncwn_TrrGRHsQQgAoo", @"C:\Dev\");
            var task = Task.Run(Download);
            task.Wait();

            //return manager.Bitmaps;
        }

        public async Task Download()
        {
            //var dataEntryPoint = new DataEntryPoint();
            using (var dbx = new DropboxClient("C7mpgQEgUaAAAAAAAAAAGoGa6oPB-nPyWM5CBhAN_joOESncwn_TrrGRHsQQgAoo"))
            {
                var list = await dbx.Files.ListFolderAsync(string.Empty);

                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    using (var response = await dbx.Files.DownloadAsync("/" + item.Name))
                    {
                        //var byteArray = await response.GetContentAsByteArrayAsync();
                        //dataEntryPoint


                        //var stream = await response.GetContentAsStreamAsync();
                        //Bitmap bitmap = BitmapFactory.DecodeStream(stream);
                        //Images.Add(item.Name.RemoveExtension(), bitmap);
                        //Names.Add(item.Name.RemoveExtension());
                    }
                }
            }
        }

        public List<GameItem> GetGameItems()
        {
            var res = new List<GameItem>();
            foreach (var item in Images)
            {
                var gameItem = new GameItem();
                //gameItem.Image = item.Value;
                gameItem.AddAnswer(item.Key, true);

                foreach (var name in GetOtherAnswers(item.Key))
                {
                    gameItem.AddAnswer(name, false);
                }

                gameItem.Shuffle();

                res.Add(gameItem);
            }


            return res;

        }

        public List<string> GetOtherAnswers(string goodAnswer)
        {
            var res = new List<string>();
            foreach (var item in Names.Where(n => n != goodAnswer))
            {
                res.Add(item);
            }

            return res;
        }


        //public List<string>  GetAnswers(int imagePosition)
        //{
        //    var res = new List<string>();
        //    foreach (var item in Images.Where(s=>s.Name != Images[0].Name))
        //    {
        //        res.Add(item.Name);
        //    }

        //    return res;
        //}
    }
}