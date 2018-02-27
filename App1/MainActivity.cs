using Android.App;
using Android.OS;
using Aircraft.Fragments;
using Android.Support.V7.App;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using App1;
using Android.Views;
using App1.Fragments;
using App1.Data;

namespace Aircraft
{
    [Activity(Label = "Aircraft", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawer;
        NavigationView navigationView;

        public object DateEntryPoint { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            V7Toolbar toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            if (savedInstanceState == null)
            {
                Android.Support.V4.App.Fragment newFragment = new HomeFragment();
                Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                ft.Add(Resource.Id.content_frame, newFragment);
                ft.AddToBackStack(null);
                ft.Commit();
            }
            drawer = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);

            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            //ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
            //        this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
            drawer.SetDrawerListener(toggle);
            toggle.SyncState();

            navigationView = (NavigationView)FindViewById(Resource.Id.nav_view);
            SetupDrawerContent(navigationView); //Calling Function 


            DataEntryPoint.Instance.Init();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }

        private void SetupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                Android.Support.V4.App.Fragment fragment = new HomeFragment();
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.civil:
                        SetTitle(Resource.String.civil);
                        fragment = new CivilFragment();
                        break;
                    case Resource.Id.military:
                        //fragment = new QuizzFragment();
                        break;
                    case Resource.Id.business:
                        //fragment = new ScoresFragment();
                        break;
                    case Resource.Id.heli:
                        //fragment = new AboutFragment();
                        break;
                }

                if (fragment != null)
                {
                    Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.content_frame, fragment);
                    ft.Commit();
                }

                e.MenuItem.SetChecked(true);
                drawer.CloseDrawers();
            };
        }
        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    navigationView.InflateMenu(Resource.Menu.nav_menu); //Navigation Drawer Layout Menu Creation  
        //    return true;
        //}

    }
}

