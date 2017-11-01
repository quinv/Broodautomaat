using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Views;
using Android.Graphics;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace BroodAutomaat
{

    [Activity(Label = "BroodAutomaat", Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Landscape,
        Theme = "@android:style/Theme.DeviceDefault.NoActionBar")]
    public class BakkerActivity : Activity, IOnMapReadyCallback
    {
        Button[] buttonsBakker = new Button[4];
        Button addAutomaat;
        View statistics;

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

            FontChangeCrawler.ChangeTypeFaceToKrungthep(FindViewById(Resource.Id.root_bakker), Assets);

            //get views
            Button logoutButton = FindViewById<Button>(Resource.Id.button_logout);
            Button helpButton = FindViewById<Button>(Resource.Id.button_help);
            addAutomaat = FindViewById<Button>(Resource.Id.button_add);
            statistics = FindViewById(Resource.Id.statistics);

            buttonsBakker[(int)BakkerSelection.automaten] = FindViewById<Button>(Resource.Id.button_automaten);
            buttonsBakker[(int)BakkerSelection.aanvullen] = FindViewById<Button>(Resource.Id.button_aanvullen);
            buttonsBakker[(int)BakkerSelection.statistieken] = FindViewById<Button>(Resource.Id.button_statistieken);
            buttonsBakker[(int)BakkerSelection.route] = FindViewById<Button>(Resource.Id.button_route);

            //add events
            logoutButton.Click += BakkerLogOut;
            helpButton.Click += Help;
            addAutomaat.Click += AddNewAutomaat;
            
            for (int i = 0; i < buttonsBakker.Length; i++)
            {
                buttonsBakker[i].Click += ChangeBakkerSelection;
            }

            //add map
            MapFragment mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map);
            if (mapFragment != null)
            {
                mapFragment.GetMapAsync(this);
            }
        }

        private void AddNewAutomaat(object sender, System.EventArgs e)
        {
            //add new bread machine
        }

        private void ChangeBakkerSelection(object sender, System.EventArgs e)
        {
            for (int i = 0; i < buttonsBakker.Length; i++)
            {
                LinearLayout.LayoutParams layoutParameters = buttonsBakker[i].LayoutParameters as LinearLayout.LayoutParams;
                layoutParameters.RightMargin = Resources.GetDimensionPixelSize(Resource.Dimension.bakkerButtonMargin);
                buttonsBakker[i].LayoutParameters = layoutParameters;
            }
            addAutomaat.Visibility = ViewStates.Gone;
            statistics.Visibility = ViewStates.Gone;

            Button btn = sender as Button;
            switch (btn.Id)
            {
                case Resource.Id.button_automaten:
                    activeButton = BakkerSelection.automaten;
                    addAutomaat.Visibility = ViewStates.Visible;
                    break;
                case Resource.Id.button_aanvullen:
                    activeButton = BakkerSelection.aanvullen;
                    break;
                case Resource.Id.button_statistieken:
                    activeButton = BakkerSelection.statistieken;
                    statistics.Visibility = ViewStates.Visible;
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
            //switch activity
            StartActivity(typeof(MainActivity));
            Finish();
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            //adds marker at kavka and zooms in on it
            MarkerOptions markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(51.2156454, 4.4031166)); //kavka coordinates
            markerOptions.SetTitle("my position");
            //markerOptions.SetIcon(<Icon>); //use to set icon
            googleMap.AddMarker(markerOptions);
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(markerOptions.Position, 17));

            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = true;
            googleMap.UiSettings.ZoomControlsEnabled = true;
        }
    }
}

