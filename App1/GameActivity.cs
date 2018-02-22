
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using App1.Engine;
using App1.Entities;
using System;
using System.Collections.Generic;

namespace App1
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        List<GameItem> _gameItems;
        GameItem _currentItem;
        int _cpt = 0;

        ImageView _image;
        Button _btn1;
        Button _btn2;
        Button _btn3;
        Button _btn4;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);

            _image = FindViewById<ImageView>(Resource.Id.image);

            _btn1 = FindViewById<Button>(Resource.Id.btn1);
            _btn2 = FindViewById<Button>(Resource.Id.btn2);
            _btn3 = FindViewById<Button>(Resource.Id.btn3);
            _btn4 = FindViewById<Button>(Resource.Id.btn4);

            _btn1.Click += btn_Click;
            _btn2.Click += btn_Click;
            _btn3.Click += btn_Click;
            _btn4.Click += btn_Click;

            GameEngine.Instance.InitGame();

            _gameItems = GameEngine.Instance.GetGameItems();

            SetGameItem();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (button.Text == _currentItem.GetCorrectAnswer())
                {
                    Toast.MakeText(this,"Correct",ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Wrong", ToastLength.Short).Show();
                }
            }

            _cpt++;
            if (_cpt < _gameItems.Count)
            {
                SetGameItem();
            }
            else
            {
                //TODO : results
            }
        }

        private void SetGameItem()
        {
            _currentItem = _gameItems[_cpt];

            _image.SetImageBitmap(_currentItem.Image);
            _btn1.Text = _currentItem.Answers[0].Value;
            _btn2.Text = _currentItem.Answers[1].Value;
            _btn3.Text = _currentItem.Answers[2].Value;
            _btn4.Text = _currentItem.Answers[3].Value;
        }
    }
}