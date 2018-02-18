using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;

namespace MaterialDesign.Class.Views
{
    public class BottomNavigationPageTransformer : Java.Lang.Object, ViewPager.IPageTransformer
    {
        public void TransformPage(View page, float position)
        {
            if (position < 0)
            {
                page.Alpha = position + 1;
            }
            else
            {
                page.TranslationX = page.MeasuredWidth * -position;
            }
        }
    }
}