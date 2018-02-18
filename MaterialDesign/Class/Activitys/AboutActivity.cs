using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "AboutActivity")]
    public class AboutActivity : AppCompatActivity, View.IOnClickListener
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_about);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_about);
            SetSupportActionBar(toolbar);
            if (SupportActionBar != null)
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                toolbar.NavigationClick += (sender, args) => { OnBackPressed(); };
            }

            Window.SetNavigationBarColor(new Android.Graphics.Color(Utils.AppUtils.GetColor(this, Resource.Color.colorPrimary)));

            InitView();
        }
        private void InitView()
        {
            Animation animation = AnimationUtils.LoadAnimation(this, Resource.Animation.anim_about_card_show);
            ScrollView scroll_about = FindViewById<ScrollView>(Resource.Id.scroll_about);
            scroll_about.StartAnimation(animation);

            LinearLayout ll_card_about_2_shop = FindViewById<LinearLayout>(Resource.Id.ll_card_about_2_shop);
            LinearLayout ll_card_about_2_email = FindViewById<LinearLayout>(Resource.Id.ll_card_about_2_email);
            LinearLayout ll_card_about_2_git_hub = FindViewById<LinearLayout>(Resource.Id.ll_card_about_2_git_hub);
            LinearLayout ll_card_about_source_licenses = FindViewById<LinearLayout>(Resource.Id.ll_card_about_source_licenses);
            ll_card_about_2_shop.SetOnClickListener(this);
            ll_card_about_2_email.SetOnClickListener(this);
            ll_card_about_2_git_hub.SetOnClickListener(this);
            ll_card_about_source_licenses.SetOnClickListener(this);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab_about_share);
            fab.SetOnClickListener(this);

            AlphaAnimation alphaAnimation = new AlphaAnimation(0.0f, 1.0f);
            alphaAnimation.Duration = 300;
            alphaAnimation.StartOffset = 600;

            TextView tv_about_version = FindViewById<TextView>(Resource.Id.tv_about_version);
            tv_about_version.SetText(Utils.AppUtils.GetVersionName(this), TextView.BufferType.Normal);
            tv_about_version.StartAnimation(alphaAnimation);
        }
        public void OnClick(View view)
        {
            Intent intent = new Intent();

            switch (view.Id)
            {
                case Resource.Id.ll_card_about_2_shop:
                    intent.SetData(Android.Net.Uri.Parse(Constant.APP_URL));
                    intent.SetAction(Intent.ActionView);
                    StartActivity(intent);
                    break;
                case Resource.Id.ll_card_about_2_email:
                    intent.SetAction(Intent.ActionSendto);
                    intent.SetData(Android.Net.Uri.Parse(Constant.EMAIL));
                    intent.PutExtra(Intent.ExtraSubject, GetString(Resource.String.about_email_intent));
                    //intent.putExtra(Intent.EXTRA_TEXT, "Hi,");
                    try
                    {
                        StartActivity(intent);
                    }
                    catch (Exception e)
                    {
                        Toast.MakeText(this, GetString(Resource.String.about_not_found_email), ToastLength.Short).Show();
                    }
                    break;
                case Resource.Id.ll_card_about_source_licenses:
                    Dialog dialog = new Dialog(this, Resource.Style.DialogFullscreenWithTitle);
                    dialog.SetTitle(GetString(Resource.String.about_source_licenses));
                    dialog.SetContentView(Resource.Layout.dialog_source_licenses);

                    Android.Webkit.WebView webView = dialog.FindViewById<Android.Webkit.WebView>(Resource.Id.web_source_licenses);
                    webView.LoadUrl("file:///android_asset/source_licenses.html");

                    Button btn_source_licenses_close = dialog.FindViewById<Button>(Resource.Id.btn_source_licenses_close);
                    btn_source_licenses_close.SetOnClickListener(new BtnClickListener(dialog));
                    dialog.Show();

                    break;

                case Resource.Id.ll_card_about_2_git_hub:
                    intent.SetData(Android.Net.Uri.Parse(Constant.GIT_HUB));
                    intent.SetAction(Intent.ActionView);
                    StartActivity(intent);
                    break;

                case Resource.Id.fab_about_share:
                    intent.SetAction(Intent.ActionSend);
                    intent.PutExtra(Intent.ExtraText, Constant.SHARE_CONTENT);
                    intent.SetType("text/plain");
                    StartActivity(Intent.CreateChooser(intent, GetString(Resource.String.share_with)));
                    break;
            }


        }

        class BtnClickListener : Java.Lang.Object, View.IOnClickListener
        {
            private Dialog dialog;
            public BtnClickListener(Dialog dialog)
            {
                this.dialog = dialog;
            }
            public void OnClick(View v)
            {
                dialog.Dismiss();
            }
        }
    }
}