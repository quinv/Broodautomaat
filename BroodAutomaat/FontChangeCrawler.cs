using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Content.Res;

namespace BroodAutomaat
{
    public class FontChangeCrawler
    {
        private Typeface typeface;

        public FontChangeCrawler(Typeface typeface)
        {
            this.typeface = typeface;
        }

        public FontChangeCrawler(AssetManager assets, String assetsFontFileName)
        {
            typeface = Typeface.CreateFromAsset(assets, assetsFontFileName);
        }

        public void ReplaceFonts(ViewGroup viewTree)
        {
            View child;
            for (int i = 0; i < viewTree.ChildCount; ++i)
            {
                child = viewTree.GetChildAt(i);
                if (child is ViewGroup)
            {
                // recursive call
                ReplaceFonts((ViewGroup)child);
            }
            else if (child is TextView)
            {
                // base case
                ((TextView)child).SetTypeface(typeface, ((TextView)child).Typeface.Style);
            }
        }
    }
}
}