using Aircraft.Entities;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using App1.Data;
using App1.Entities;
using App1.Extensions;
using App1.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App1
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        private CountDown _timer;
        private Game _currentGame;
        List<Image> _images;
        GameItem _currentItem;
        int _cpt = 0;

        ImageView _image;
        List<Button> _buttons;
        TextView _txtCountdown;
        TextView _txtNumber;

        private int _correctAnswersCount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);

            string xml = Intent.GetStringExtra("CurrentGame");
            _currentGame = xml.ToObject<Game>();

            _image = FindViewById<ImageView>(Resource.Id.image);
            _txtCountdown = FindViewById<TextView>(Resource.Id.txtCountdown);
            _txtNumber = FindViewById<TextView>(Resource.Id.txtNumber);

            _buttons = new List<Button>();

            _buttons.Add(FindViewById<Button>(Resource.Id.btn1));
            _buttons.Add(FindViewById<Button>(Resource.Id.btn2));
            _buttons.Add(FindViewById<Button>(Resource.Id.btn3));
            _buttons.Add(FindViewById<Button>(Resource.Id.btn4));

            foreach (var button in _buttons)
            {
                button.Click += btn_Click;
            }
           
            _images = DataEntryPoint.Instance.Images.Get(_currentGame.Category, _currentGame.Name);

            SetGameItem();
        }

        private void Timer_Tick(long millisUntilFinished)
        {
            _txtCountdown.Text = string.Format("00:{0:D2}", millisUntilFinished / 1000);
        }

        private void HandleTimer()
        {
            ResetButtons();

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

        private void ResetButtons()
        {
            var backgroundNormal = GetDrawable(Resource.Drawable.button_normal);
            foreach (var button in _buttons)
            {
                button.Background = backgroundNormal;
            }
        }

        public async Task Execute(Action action, int timeoutInMilliseconds)
        {
            await Task.Delay(timeoutInMilliseconds);
            action();
        }


        private async Task ManageButtons(object sender)
        {
            _timer.Cancel();
            var backgroundCorrect = GetDrawable(Resource.Drawable.button_correct);
            var backgroundError = GetDrawable(Resource.Drawable.button_error);
            var button = sender as Button;
            if (button != null)
            {
                if (button.Text == _currentItem.GetCorrectAnswer())
                {
                    _correctAnswersCount++;
                    button.Background = backgroundCorrect;
                }
                else
                {
                    button.Background = backgroundError;
                    var correctButton = GetCorrectButton();
                    correctButton.Background = backgroundCorrect;
                }
            }

            await Execute(HandleTimer, 2000);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ManageButtons(sender);
        }

        private void SetGameItem()
        {
            _txtNumber.Text = string.Format("{0}/{1}", _cpt, _images.Count);
            _timer = new CountDown(11000, 1000);
            _timer.Tick += Timer_Tick;
            _timer.Finish += _timer_Finish;
            _timer.Start();
            var names = DataEntryPoint.Instance.Images.GetOtherNames(_images[_cpt]);

            _image.SetImageURI(Android.Net.Uri.FromFile(new Java.IO.File(_images[_cpt].Path)));

            _currentItem = new GameItem();
            _currentItem.AddAnswer(_images[_cpt].Name, true);
            foreach (var item in names)
            {
                _currentItem.AddAnswer(item, false);
            }

            _currentItem.Shuffle();

            for (int i = 0; i < _currentItem.Answers.Count; i++)
            {
                _buttons[i].Text = _currentItem.Answers[i].Value;
            }
        }

        private void _timer_Finish()
        {
            _txtCountdown.Text = "00:00";
            HandleTimer();
        }

        public Button GetCorrectButton()
        {
            foreach (var button in _buttons)
            {
                if (button.Text == _currentItem.GetCorrectAnswer())
                {
                    return button;
                }
            }

            return null;          
        }
    }
}