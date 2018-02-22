using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Graphics;
using Dropbox.Api;

namespace App1.Engine
{
    public class DropboxFileManager
    {
        private string _key;

        public Dictionary<string,Bitmap> Bitmaps { get; set; }

        public DropboxFileManager(string key, string path)
        {
            _key = key;
            _path = path;

            Bitmaps = new Dictionary<string, Bitmap>();
        }

        public async Task Download()
        {            
            using (var dbx = new DropboxClient(_key))
            {
                var list = await dbx.Files.ListFolderAsync(string.Empty);

                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    using (var response = await dbx.Files.DownloadAsync("/" + item.Name))
                    {
                        var stream = await response.GetContentAsStreamAsync();

                        Bitmap bitmap = BitmapFactory.DecodeStream(stream);
                        Bitmaps.Add(item.Name, bitmap);
                       
                    }
                }
            }            
        }

        //private void SetImage(Stream stream)
        //{
        //    byte[] pictByteArray;
        //    //Use bitarray to use less memory                    
        //    byte[] buffer = new byte[16 * 1024];
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        int read;
        //        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }
        //        pictByteArray = ms.ToArray();
        //    }

        //    stream.Close();

        //    //Get file information
        //    BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
        //    Bitmap bitmap = BitmapFactory.DecodeByteArray(pictByteArray, 0, pictByteArray.Length, options);

        //    Bitmaps.Add(bitmap);
        //    //_image.SetImageBitmap(bitmap);
        //}

        //private void CopyStream(Stream stream, string destPath)
        //{
        //    if (File.Exists(destPath))
        //    {
        //        File.Delete(destPath);
        //    }

        //    using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
        //    {
        //        stream.CopyTo(fileStream);
        //    }
        //}
    }
}