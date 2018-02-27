using Aircraft.Entities;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using App1.Adapters;
using App1.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using static Android.Support.V7.Widget.RecyclerView;

namespace App1.Fragments
{
    public class CivilFragment : Android.Support.V4.App.Fragment
    {
        RecyclerView _recyclerView;
        LayoutManager _layoutManager;
        private List<Game> _games;
        GameAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var res = inflater.Inflate(Resource.Layout.civil, container, false);

           
            _recyclerView = res.FindViewById<RecyclerView>(Resource.Id.civilRecyclerView);

            _layoutManager = new LinearLayoutManager(_recyclerView.Context);
            _recyclerView.SetLayoutManager(_layoutManager);

            _games = DataEntryPoint.Instance.Games.Get("civil");


            _adapter = new GameAdapter(_games);
            _adapter.ItemClick += OnItemClick;
            _adapter.ItemLongClick += OnItemLongClick;
            _recyclerView.SetAdapter(_adapter);

            return res;
        }

        private void OnItemLongClick(object sender, int e)
        {

        }

        private void OnItemClick(object sender, int e)
        {
            var currentGame = _games[e];
            Intent intent = new Intent(this.Activity, typeof(GameActivity));
            string json = JsonConvert.SerializeObject(currentGame);
            intent.PutExtra("CurrentGame", json);
            StartActivity(intent);
        }
    }
}