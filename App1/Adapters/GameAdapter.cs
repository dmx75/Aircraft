using System;
using System.Collections.Generic;
using System.Linq;
using Aircraft.Entities;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace App1.Adapters
{
    public class GameAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public event EventHandler<int> ItemLongClick;
        public List<Game> Games { get; set; }

        public GameAdapter(List<Game> games)
        {
            Games = games;
        }

        public override int ItemCount
        {
            get
            {
                var count = Games.Count();
                return count;
            }
        }

        public void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }

        public void OnLongClick(int position)
        {
            if (ItemLongClick != null)
                ItemLongClick(this, position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            GameAdapterViewHolder vh = holder as GameAdapterViewHolder;

            var item = Games.ElementAt(position);
            vh.NoteProgressBar.Max = item.Total;
            vh.Name.Text = item.Name;
            vh.Result.Text = string.Format("{0}/{1}", item.Score, item.Total);
            vh.NoteProgressBar.Progress = item.Score;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.gameCardLayout, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            GameAdapterViewHolder vh = new GameAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        public class GameAdapterViewHolder : RecyclerView.ViewHolder
        {
            public TextView Name { get; set; }
            public TextView Result { get; set; }
            public ProgressBar NoteProgressBar { get; set; }

            public GameAdapterViewHolder(View itemView, Action<int> listener, Action<int> longClickListener) : base(itemView)
            {
                Name = itemView.FindViewById<TextView>(Resource.Id.name);
                Result = itemView.FindViewById<TextView>(Resource.Id.result);
                NoteProgressBar = ItemView.FindViewById<ProgressBar>(Resource.Id.progressBar);
                //NoteProgressBar.Max = 20;
                itemView.Click += (sender, e) => listener(base.LayoutPosition);
                itemView.LongClick += (sender, e) => longClickListener(base.LayoutPosition);
            }
        }
    }
}