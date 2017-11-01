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

        private enum BakkerSelection
        {
            automaten,
            aanvullen,
            statistieken,
            route
        }

        private BakkerSelection activeButton = BakkerSelection.automaten;

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

            buttonsBakker[(int)BakkerSelection.automaten] = FindViewById<Button>(Resource.Id.button_automaten);
            buttonsBakker[(int)BakkerSelection.aanvullen] = FindViewById<Button>(Resource.Id.button_aanvullen);
            buttonsBakker[(int)BakkerSelection.statistieken] = FindViewById<Button>(Resource.Id.button_statistieken);
            buttonsBakker[(int)BakkerSelection.route] = FindViewById<Button>(Resource.Id.button_route);

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
            switch (btn.Id)
            {
                case Resource.Id.button_automaten:
                    activeButton = BakkerSelection.automaten;
                    break;
                case Resource.Id.button_aanvullen:
                    activeButton = BakkerSelection.aanvullen;
                    break;
                case Resource.Id.button_statistieken:
                    activeButton = BakkerSelection.statistieken;
                    break;
                case Resource.Id.button_route:
                    activeButton = BakkerSelection.route;
                    break;
                default:
                    activeButton = BakkerSelection.automaten;
                    break;
            }
            LinearLayout.LayoutParams layoutParams = buttonsBakker[(int)activeButton].LayoutParameters as LinearLayout.LayoutParams;
            layoutParams.RightMargin = Resources.GetDimensionPixelSize(Resource.Dimension.bakkerButtonMarginActive);
            buttonsBakker[(int)activeButton].LayoutParameters = layoutParams;
        }

        private void Help(object sender, System.EventArgs e)
        {
            //add info dependant on current selection
            int customTextID;
            switch (activeButton)
            {
                case BakkerSelection.automaten:
                    customTextID = Resource.String.info_bakker_automaten;
                    break;
                case BakkerSelection.aanvullen:
                    customTextID = Resource.String.info_bakker_aanvullen;
                    break;
                case BakkerSelection.statistieken:
                    customTextID = Resource.String.info_bakker_statistieken;
                    break;
                case BakkerSelection.route:
                default:
                    customTextID = Resource.String.info_bakker_route;
                    break;
            }
            OverlayWindow.CreateOverlayFromView(Resource.Layout.InformationUser, customTextID, Assets, this);
        }

        private void BakkerLogOut(object sender, System.EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}

