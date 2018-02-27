using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aircraft;
using Aircraft.Entities;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Extensions;
using Newtonsoft.Json;

namespace App1
{
    [Activity(Label = "ResultActivity")]
    public class ResultActivity : Activity
    {
        private int _score;
        private int _total;
        private TextView _txtScore;
        private Button _btnBack;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.result);

            _txtScore = FindViewById<TextView>(Resource.Id.txtScore);
            _btnBack = FindViewById<Button>(Resource.Id.btnBack);
            _btnBack.Click += _btnBack_Click;

            string xml = Intent.GetStringExtra("CurrentGame");
            var currentGame = xml.ToObject<Game>();
            //var currentGame = (Game)JsonConvert.DeserializeObject(xml);

            //var currentGame = (Game)Intent.GetSerializableExtra("CurrentGame");
            _txtScore.Text = string.Format("Your score is {0}/{1}", currentGame.Score, currentGame.Total);

        }

        private void _btnBack_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}