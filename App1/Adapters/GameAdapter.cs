using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aircraft.Entities;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App1.Helpers;

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
            vh.NoteProgressBar.Progress = item.Score;
            vh.NoteProgressBar.Max = item.Total;
            vh.Name.Text = item.Name;
            vh.Result.Text = string.Format("{0}/{1}", item.Score, item.Total);

            //float percentage = ((float)item.Score / (float)item.Total) * 100;
            //var currentColor = ColorHelper.GetBlendedColor(percentage);         
            //Drawable progressDrawable = vh.NoteProgressBar.ProgressDrawable.Mutate();
            //progressDrawable.SetColorFilter(currentColor, PorterDuff.Mode.SrcIn);
           
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

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
                itemView.LongClick += (sender, e) => longClickListener(base.LayoutPosition);
            }
        }
    }
}