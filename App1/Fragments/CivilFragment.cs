using Aircraft.Entities;
using Aircraft.Mock;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using App1.Adapters;
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

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var res = inflater.Inflate(Resource.Layout.civil, container, false);

            _recyclerView = res.FindViewById<RecyclerView>(Resource.Id.civilRecyclerView);

            // Plug in the linear layout manager:
            _layoutManager = new LinearLayoutManager(_recyclerView.Context);
            _recyclerView.SetLayoutManager(_layoutManager);

            _games = GameMock.Get();

            //// Plug in my adapter:
            _adapter = new GameAdapter(_games);
            _adapter.ItemClick += OnItemClick;
            _adapter.ItemLongClick += OnItemLongClick;
            _recyclerView.SetAdapter(_adapter);

            return res;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void OnItemLongClick(object sender, int e)
        {
            
        }

        private void OnItemClick(object sender, int e)
        {
            Intent intent = new Intent(this.Activity, typeof(GameActivity));
            StartActivity(intent);
        }
    }
}