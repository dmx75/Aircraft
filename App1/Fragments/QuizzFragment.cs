using Android.OS;
using Android.Views;
using App1;

namespace Aircraft.Fragments
{
    public class QuizzFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             return inflater.Inflate(Resource.Layout.quizz, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}