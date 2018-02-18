using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;

namespace MaterialDesign.Class.fragment
{
    public class WidgetsFragment : Android.Support.V4.App.Fragment
    {
        private EditText et_main_3;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            NestedScrollView nestedScrollView = (NestedScrollView)inflater.Inflate(Resource.Layout.fragment_widgets, container, false);
            et_main_3 = nestedScrollView.FindViewById<EditText>(Resource.Id.et_main_3);

            return nestedScrollView;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            et_main_3.RequestFocus();
        }
    }
}