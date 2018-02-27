using SQLite;

namespace App1.Entities
{
    [Table("Image")]
    public class Image : BaseEntity
    {   
        public string Path { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string GameName { get; set; }

        public Image()
        {

        }
    }
}