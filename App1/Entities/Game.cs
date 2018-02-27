using App1.Entities;
using Java.IO;
using SQLite;
using System;

namespace Aircraft.Entities
{
    [Table("Game")]
    public class Game : BaseEntity, IEquatable<Game>
    {       

        public string Name { get; set; }

        public int Score { get; set; }

        public int Total { get; set; }

        public string Category { get; set; }

        public bool IsPlayed { get; set; }

        public Game()
        {

        }

        public bool Equals(Game other)
        {
            if (other != null)
            {
                return Category == other.Category && Name == other.Name;
            }

            return false;
        }
    }
}