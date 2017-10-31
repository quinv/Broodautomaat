using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Util;
using Android.Views;
using Android.Graphics;

namespace BroodAutomaat
{
    [Activity(Label = "BroodAutomaat", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Landscape,
        Theme = "@android:style/Theme.DeviceDefault.NoActionBar")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            FontChangeCrawler fontChangeCrawler = new FontChangeCrawler(Typeface.CreateFromAsset(Assets, "Fonts/krungthep.ttf"));
            fontChangeCrawler.ReplaceFonts(FindViewById<ViewGroup>(Resource.Id.root_main));

            Button loginButton = FindViewById<Button>(Resource.Id.button_login);
            loginButton.Click += BakkerLogIn;

            Button helpButton = FindViewById<Button>(Resource.Id.button_help);
            helpButton.Click += Help;
        }

        private void Help(object sender, System.EventArgs e)
        {
            View customView = LayoutInflater.Inflate(Resource.Layout.InformationUser, null);
            customView.Background = Resources.GetDrawable(Resource.Drawable.RoundedButton, null);
            AlertDialog builder = new AlertDialog.Builder(this).Create();
            builder.SetView(customView);
            builder.Show();
            FontChangeCrawler fontChangeCrawler = new FontChangeCrawler(Typeface.CreateFromAsset(Assets, "Fonts/krungthep.ttf"));
            fontChangeCrawler.ReplaceFonts(customView as ViewGroup);

        }

        private void BakkerLogIn(object sender, System.EventArgs e)
        {
            StartActivity(typeof(BakkerActivity));
            Finish();
        }
    }
}

