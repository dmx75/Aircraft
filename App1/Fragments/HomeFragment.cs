using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using App1;
using App1.Fragments;
using App1.Helpers;

namespace Aircraft.Fragments
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {       
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             var res = inflater.Inflate(Resource.Layout.home, container, false);

           
            return res;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}