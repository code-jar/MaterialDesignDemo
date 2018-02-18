using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MaterialDesign.Class.Billings;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "DonateActivity")]
    public class DonateActivity : AppCompatActivity, IabBroadcastReceiver.IabBroadcastListener
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_donate);

            InitView();
        }
        private void InitView()
        {
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_donate);
            SetSupportActionBar(toolbar);
            if (SupportActionBar != null)
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                toolbar.NavigationClick += (sender, args) => { OnBackPressed(); };
            }

        }
        public void receivedBroadcast()
        {
        }
    }
}