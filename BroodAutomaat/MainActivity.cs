﻿using Android.App;
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

        private enum BreadTypes
        {
            grof = 1,
            licht = 2,
            wit = 4,
            alle = grof | licht | wit
        }

        private BreadTypes breadFilter = BreadTypes.alle;

        TextView seekBarText;
        int seekBarValue = 5;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //change fontface
            FontChangeCrawler.ChangeTypeFaceToKrungthep(FindViewById(Resource.Id.root_main), Assets);

            //get views
            Button loginButton = FindViewById<Button>(Resource.Id.button_login);
            Button helpButton = FindViewById<Button>(Resource.Id.button_help);
            RadioGroup breadGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup_broden);
            SeekBar seekBarRange = FindViewById<SeekBar>(Resource.Id.seekBar_range);
            seekBarText = FindViewById<TextView>(Resource.Id.range_value);

            //add events
            loginButton.Click += BakkerLogIn;
            helpButton.Click += Help;
            breadGroup.CheckedChange += BreadGroup_CheckedChange;
            seekBarRange.ProgressChanged += SeekBarRange_ProgressChanged; ;
        }

        private void SeekBarRange_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            SeekBar seekBar = sender as SeekBar;
            seekBarValue = (int)seekBar.Progress;
            seekBarText.Text = seekBarValue.ToString() + "km";
        }

        private void BreadGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            RadioGroup radioGroup = sender as RadioGroup;
            switch (radioGroup.CheckedRadioButtonId)
            {
                case Resource.Id.radioButtonGrof:
                    breadFilter = BreadTypes.grof;
                    break;
                case Resource.Id.radioButtonLicht:
                    breadFilter = BreadTypes.licht;
                    break;
                case Resource.Id.radioButtonWit:
                    breadFilter = BreadTypes.wit;
                    break;
                case Resource.Id.radioButtonAlle:
                default:
                    breadFilter = BreadTypes.alle;
                    break;
            }
        }

        private void Help(object sender, System.EventArgs e)
        {
            //create overlay window with info in main
            OverlayWindow.CreateOverlayFromView(Resource.Layout.InformationUser, Resource.String.info_home, Assets, this);
        }

        private void BakkerLogIn(object sender, System.EventArgs e)
        {
            StartActivity(typeof(BakkerActivity));
            Finish();
        }
    }
}

