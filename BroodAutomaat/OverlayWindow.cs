using Android.App;
using Android.Views;
using Android.Content.Res;
using Android.Widget;

namespace BroodAutomaat
{
    class OverlayWindow
    {

        public static View CreateOverlayFromView(int customViewID, AssetManager assets, Activity activity)
        {
            View customView = activity.LayoutInflater.Inflate(customViewID, null);
            customView.Background = activity.Resources.GetDrawable(Resource.Drawable.RoundedButton, null);
            AlertDialog builder = new AlertDialog.Builder(activity).Create();
            builder.SetView(customView);
            builder.Show();
            FontChangeCrawler.ChangeTypeFaceToKrungthep(customView, assets);
            return customView;
        }

        public static View CreateOverlayFromView(int customViewID, int customText, AssetManager assets, Activity activity)
        {
            View customView = CreateOverlayFromView(customViewID, assets, activity);

            TextView info = customView.FindViewById<TextView>(Resource.Id.info_block);
            info.Text = activity.Resources.GetString(customText);

            return customView;
        }

    }
}