using V4App = Android.Support.V4.App;

namespace App1.Helpers
{
    public static class FragmentHelper
    {
        public static void NavigateTo(int containerViewId, V4App.Fragment fragment, V4App.FragmentManager fragmentManager)
        {
            if (fragment != null)
            {
                V4App.FragmentTransaction ft = fragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.content_frame, fragment);
                ft.Commit();
            }
        }

    }
}