using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Views;
using Android.Graphics;

namespace BroodAutomaat
{
    [Activity(Label = "BroodAutomaat", Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Landscape,
        Theme = "@android:style/Theme.DeviceDefault.NoActionBar")]
    public class BakkerActivity : Activity
    {
        Button[] buttonsBakker = new Button[4];

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Bakker);
            
            FontChangeCrawler fontChangeCrawler = new FontChangeCrawler(Typeface.CreateFromAsset(Assets, "Fonts/krungthep.ttf"));
            fontChangeCrawler.ReplaceFonts(FindViewById<ViewGroup>(Resource.Id.root_bakker));

            Button logoutButton = FindViewById<Button>(Resource.Id.button_logout);
            logoutButton.Click += BakkerLogOut;

            Button helpButton = FindViewById<Button>(Resource.Id.button_help);
            helpButton.Click += Help;

            buttonsBakker[0] = FindViewById<Button>(Resource.Id.button_automaten);
            buttonsBakker[1] = FindViewById<Button>(Resource.Id.button_aanvullen);
            buttonsBakker[2] = FindViewById<Button>(Resource.Id.button_statistieken);
            buttonsBakker[3] = FindViewById<Button>(Resource.Id.button_route);

            for (int i = 0; i < buttonsBakker.Length; i++)
            {
                buttonsBakker[i].Click += ChangeBakkerSelection;
            }
        }

        private void ChangeBakkerSelection(object sender, System.EventArgs e)
        {
            for (int i = 0; i < buttonsBakker.Length; i++)
            {
                LinearLayout.LayoutParams layoutParameters = buttonsBakker[i].LayoutParameters as LinearLayout.LayoutParams;
                layoutParameters.RightMargin = Resources.GetDimensionPixelSize(Resource.Dimension.bakkerButtonMargin);
                buttonsBakker[i].LayoutParameters = layoutParameters;
            }

            Button btn = sender as Button;
            int activebutton;
            switch (btn.Id)
            {
                case Resource.Id.button_automaten:
                    activebutton = 0;
                    break;
                case Resource.Id.button_aanvullen:
                    activebutton = 1;
                    break;
                case Resource.Id.button_statistieken:
                    activebutton = 2;
                    break;
                case Resource.Id.button_route:
                    activebutton = 3;
                    break;
                default:
                    activebutton = 0;
                    break;
            }
            LinearLayout.LayoutParams layoutParams = buttonsBakker[activebutton].LayoutParameters as LinearLayout.LayoutParams;
            layoutParams.RightMargin = Resources.GetDimensionPixelSize(Resource.Dimension.bakkerButtonMarginActive);
            buttonsBakker[activebutton].LayoutParameters = layoutParams;
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

        private void BakkerLogOut(object sender, System.EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}

