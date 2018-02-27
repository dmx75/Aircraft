using SQLite;

namespace App1.Entities
{
    public class BaseEntity : SerializableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public BaseEntity()
        {

        }
    }
}