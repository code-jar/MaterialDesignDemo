using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MaterialDesign.Class.Views;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "BottomNavigationActivity")]
    public class BottomNavigationActivity : AppCompatActivity
    {
        private static ViewPager viewPager;
        private static BottomNavigationView navigation;
        private static List<View> viewList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_bottom_navigation);

            //window.getDecorView().setSystemUiVisibility(View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN);
            Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
            //window.addFlags(WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            //window.setStatusBarColor(Color.argb(33, 0, 0, 0));
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(33, 0, 0, 0));

            InitView();
        }

        private void InitView()
        {
            View view1 = LayoutInflater.Inflate(Resource.Layout.item_view_pager_1, null);
            View view2 = LayoutInflater.Inflate(Resource.Layout.item_view_pager_2, null);
            View view3 = LayoutInflater.Inflate(Resource.Layout.item_view_pager_3, null);
            View view4 = LayoutInflater.Inflate(Resource.Layout.item_view_pager_4, null);

            viewList = new List<View>();
            viewList.Add(view1);
            viewList.Add(view2);
            viewList.Add(view3);
            viewList.Add(view4);

            viewPager = FindViewById<ViewPager>(Resource.Id.view_pager_bottom_navigation);
            viewPager.Adapter = new PageAdapter();
            viewPager.AddOnPageChangeListener(new OnPageChange());
            viewPager.SetPageTransformer(true, new BottomNavigationPageTransformer());

            navigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            navigation.SetOnNavigationItemSelectedListener(new OnNavigationItemSelected());

            BottomNavigationViewHelper.disableShiftMode(navigation);
        }
        class OnPageChange : Java.Lang.Object, ViewPager.IOnPageChangeListener
        {
            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {
            }

            public void OnPageScrollStateChanged(int state)
            {
            }

            public void OnPageSelected(int position)
            {
                switch (position)
                {
                    case 0:
                        navigation.SelectedItemId = Resource.Id.bottom_navigation_blue;
                        break;
                    case 1:
                        navigation.SelectedItemId = Resource.Id.bottom_navigation_green;
                        break;
                    case 2:
                        navigation.SelectedItemId = Resource.Id.bottom_navigation_yellow;
                        break;
                    case 3:
                        navigation.SelectedItemId = Resource.Id.bottom_navigation_red;
                        break;
                }
            }
        }
        class OnNavigationItemSelected : Java.Lang.Object, BottomNavigationView.IOnNavigationItemSelectedListener
        {
            bool BottomNavigationView.IOnNavigationItemSelectedListener.OnNavigationItemSelected(IMenuItem item)
            {
                switch (item.ItemId)
                {
                    case Resource.Id.bottom_navigation_blue:
                        viewPager.SetCurrentItem(0, true);
                        return true;
                    case Resource.Id.bottom_navigation_green:
                        viewPager.SetCurrentItem(1, true);
                        return true;
                    case Resource.Id.bottom_navigation_yellow:
                        viewPager.SetCurrentItem(2, true);
                        return true;
                    case Resource.Id.bottom_navigation_red:
                        viewPager.SetCurrentItem(3, true);
                        return true;
                }
                return false;
            }
        }
        class PageAdapter : PagerAdapter
        {
            public override int Count => viewList.Count;

            public override bool IsViewFromObject(View view, Java.Lang.Object @object)
            {
                return view == @object;
            }
            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
            {
                var view = @object as View;
                container.RemoveView(view);
            }
            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                container.AddView(viewList[position]);
                return viewList[position];
            }
        }
    }
}