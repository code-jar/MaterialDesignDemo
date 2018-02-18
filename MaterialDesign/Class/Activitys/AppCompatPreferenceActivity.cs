using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "AppCompatPreferenceActivity")]
    public class AppCompatPreferenceActivity : PreferenceActivity
    {
        private AppCompatDelegate mDelegate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            GetDelegate().InstallViewFactory();
            GetDelegate().OnCreate(savedInstanceState);
            base.OnCreate(savedInstanceState);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            GetDelegate().OnPostCreate(savedInstanceState);
        }
        public Android.Support.V7.App.ActionBar GetSupportActionBar()
        {
            return GetDelegate().SupportActionBar;
        }
        public void SetSupportActionBar(Android.Support.V7.Widget.Toolbar toolbar)
        {
            GetDelegate().SetSupportActionBar(toolbar);
        }
        public override MenuInflater MenuInflater => GetDelegate().MenuInflater;
        public override void SetContentView(int layoutResID)
        {
            GetDelegate().SetContentView(layoutResID);
        }
        public override void SetContentView(View view)
        {
            GetDelegate().SetContentView(view);
        }
        public override void SetContentView(View view, ViewGroup.LayoutParams @params)
        {
            GetDelegate().SetContentView(view, @params);
        }
        public override void AddContentView(View view, ViewGroup.LayoutParams @params)
        {
            GetDelegate().AddContentView(view, @params);
        }
        protected override void OnPostResume()
        {
            base.OnPostResume();
            GetDelegate().OnPostResume();
        }
        protected override void OnTitleChanged(ICharSequence title, Color color)
        {
            base.OnTitleChanged(title, color);
            GetDelegate().SetTitle(title);
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            GetDelegate().OnConfigurationChanged(newConfig);
        }
        protected override void OnStop()
        {
            base.OnStop();
            GetDelegate().OnStop();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            GetDelegate().OnDestroy();
        }
        public override void InvalidateOptionsMenu()
        {
            base.InvalidateOptionsMenu();
            GetDelegate().InvalidateOptionsMenu();
        }
        private AppCompatDelegate GetDelegate()
        {
            if (mDelegate == null)
            {
                mDelegate = AppCompatDelegate.Create(this, null);
            }
            return mDelegate;
        }
    }
}