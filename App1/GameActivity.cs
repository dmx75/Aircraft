using Aircraft.Entities;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using App1.Data;
using App1.Entities;
using App1.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace App1
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        private Game _currentGame;
        List<Image> _images;      
        GameItem _currentItem;
        int _cpt = 0;

        ImageView _image;
        Button _btn1;
        Button _btn2;
        Button _btn3;
        Button _btn4;

        private int _correctAnswersCount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);

            string xml = Intent.GetStringExtra("CurrentGame");
            _currentGame = xml.ToObject<Game>();

            _image = FindViewById<ImageView>(Resource.Id.image);

            _btn1 = FindViewById<Button>(Resource.Id.btn1);
            _btn2 = FindViewById<Button>(Resource.Id.btn2);
            _btn3 = FindViewById<Button>(Resource.Id.btn3);
            _btn4 = FindViewById<Button>(Resource.Id.btn4);

            _btn1.Click += btn_Click;
            _btn2.Click += btn_Click;
            _btn3.Click += btn_Click;
            _btn4.Click += btn_Click;

            _images = DataEntryPoint.Instance.Images.Get(_currentGame.Category, _currentGame.Name);

            SetGameItem();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (button.Text == _currentItem.GetCorrectAnswer())
                {
                    _correctAnswersCount++;
                    Toast.MakeText(this, "Correct", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Wrong", ToastLength.Short).Show();
                }
            }

            _cpt++;
            if (_cpt < _images.Count)
            {
                SetGameItem();
            }
            else
            {

                var intent = new Intent(this, typeof(ResultActivity));

                if (_correctAnswersCount > _currentGame.Score)
                {
                    _currentGame.Score = _correctAnswersCount;
                    DataEntryPoint.Instance.Games.Update(_currentGame);
                    
                }

                intent.PutExtra("CurrentGame", JsonConvert.SerializeObject(_currentGame));
                StartActivity(intent);               
            }
        }

        private void SetGameItem()
        {
            var names = DataEntryPoint.Instance.Images.GetOtherNames(_images[_cpt]);

            _image.SetImageURI(Android.Net.Uri.FromFile(new Java.IO.File(_images[_cpt].Path)));

            _currentItem = new GameItem();
            _currentItem.AddAnswer(_images[_cpt].Name, true);
            foreach (var item in names)
            {
                _currentItem.AddAnswer(item, false);
            }
           
            _currentItem.Shuffle();

            _btn1.Text = _currentItem.Answers[0].Value;
            _btn2.Text = _currentItem.Answers[1].Value;
            _btn3.Text = _currentItem.Answers[2].Value;
            _btn4.Text = _currentItem.Answers[3].Value;
        }
    }
}