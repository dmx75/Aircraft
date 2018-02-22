using System;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;

namespace App1.Entities
{
    public class GameItem
    {
        public Bitmap Image { get; set; }

        public List<Answer> Answers { get; set; }

        public GameItem()
        {
            Answers = new List<Answer>();
        }

        public void AddAnswer(string answer, bool isCorrect)
        {
            Answers.Add(new Answer { Value = answer, IsCorrect = isCorrect });
        }

        public void Shuffle()
        {
            var rnd = new Random();
            Answers = Answers.OrderBy(item => rnd.Next()).ToList();
        }

        public string GetCorrectAnswer()
        {
            return Answers.FirstOrDefault(a => a.IsCorrect).Value;
        }
    }
}