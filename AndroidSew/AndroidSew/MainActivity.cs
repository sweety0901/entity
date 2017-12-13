using Android.App;
using Android.Widget;
using Android.OS;

namespace AndroidSew
{
    [Activity(Label = "Утор", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            button.Click += Button_order;

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += Button_Catalog;

            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += Button_About;
        }

        private void Button_Catalog(object sender, System.EventArgs e)
        {
            StartActivity(typeof(ActivityCatalog));
        }

        private void Button_About(object sender, System.EventArgs e)
        {
            StartActivity(typeof(ActivityAbout));
        }

        private void Button_order(object sender, System.EventArgs e)
        {
            StartActivity(typeof(ActivityOrder));
        }
    }
}

