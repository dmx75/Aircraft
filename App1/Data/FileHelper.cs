using Android.Content;
using Android.Graphics;
using Java.IO;
using System;
using System.IO;


namespace App1.Data
{
    public class FileHelper
    {
        public string Save(byte[] imageData, string fileName)
        {
            var pictures = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filePath = System.IO.Path.Combine(pictures, fileName);
            try
            {
                System.IO.File.WriteAllBytes(filePath, imageData);
                return filePath;
                //Java.IO.File fl = new Java.IO.File(filePath);
            }
            catch (System.Exception e) { System.Console.WriteLine(e.ToString()); }

            return string.Empty;
        }


    }
}