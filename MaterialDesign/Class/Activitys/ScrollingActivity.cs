using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using Com.Bumptech.Glide.Load.Resource.Drawable;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "ScrollingActivity")]
    public class ScrollingActivity : AppCompatActivity, View.IOnClickListener
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_scrolling);

            Window.AddFlags(WindowManagerFlags.TranslucentNavigation);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            if (SupportActionBar != null)
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                toolbar.NavigationClick += (sender, args) => { OnBackPressed(); };
            }

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab_scrolling);
            fab.SetOnClickListener(this);

            ImageView image_scrolling_top = FindViewById<ImageView>(Resource.Id.image_scrolling_top);
            Glide.With(this).Load(Resource.Drawable.material_design_3).Apply(RequestOptions.FitCenterTransform()).Into(image_scrolling_top);

        }

        protected override void OnResume()
        {
            base.OnResume();

            Configuration configuration = Resources.Configuration;
            if (configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentNavigation);

                CollapsingToolbarLayout collapsing_toolbar_layout = FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar_layout);
                collapsing_toolbar_layout.SetExpandedTitleTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Transparent));
            }
            else
            {
                Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
            }
        }

        public void OnClick(View v)
        {
            Intent intent = new Intent();
            intent.SetAction(Intent.ActionSend);
            intent.PutExtra(Intent.ExtraText, Constant.SHARE_CONTENT);
            intent.SetType("text/plain");
            StartActivity(Intent.CreateChooser(intent, GetString(Resource.String.share_with)));
        }
    }
}