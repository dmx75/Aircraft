using App1.Entities;
using App1.Extensions;
using SQLite;
using System.Collections.Generic;

namespace App1.Data
{
    public class ImageDataManager : BaseDataManager<Image>
    {
        //SQLiteConnection _db;

        public ImageDataManager(SQLiteConnection db) : base(db)
        {

        }

        public List<Image> Get(string category, string gameName)
        {
            var res = new List<Image>();
            var images = _db.Table<Image>().Where(i => i.Category == category && i.GameName == gameName);
            foreach (var image in images)
            {
                res.Add(image);
            }

            return res;
        }

        public List<string> GetOtherNames(Image image)
        {
            var res = new List<string>();
            foreach (var item in _db.Table<Image>().Where(i => i.Category == image.Category && i.GameName == image.GameName))
            {
                if (item.Id != image.Id)
                {
                    res.Add(item.Name.RemoveExtension());
                }
            }

            return res;
        }

        public void Insert(byte[] array, string fileName, string category, string gameName)
        {
            var fh = new FileHelper();
            var path = fh.Save(array, fileName);

            var image = new Image
            {
                Name = fileName,
                Path = path,
                Category = category,
                GameName = gameName
            };

            _db.Insert(image);
        }
    }
}