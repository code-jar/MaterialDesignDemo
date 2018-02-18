using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Content;
using Android.Views.Animations;
using System.Collections.Generic;
using Android.Support.V7.App;
using Com.Bumptech.Glide;

namespace MaterialDesign
{
    [Activity(Label = "MaterialDesign", MainLauncher = true)]
    public class MainActivity : AppCompatActivity,
        NavigationView.IOnNavigationItemSelectedListener,
        View.IOnClickListener,
        ViewPager.IOnPageChangeListener
    {
        private static DrawerLayout drawer;
        private FloatingActionButton fab;
        private TabLayout mTabLayout;
        private ViewPager mViewPager;
        private static RelativeLayout relative_main;
        private ImageView img_page_start;

        private static bool isShowPageStart = true;
        private const int MESSAGE_SHOW_DRAWER_LAYOUT = 0x001;
        private const int MESSAGE_SHOW_START_PAGE = 0x002;
        private static Android.Content.Context currentContext;

        public Handler mHandler = new Handler((msg) =>
        {

            switch (msg.What)
            {
                case MESSAGE_SHOW_DRAWER_LAYOUT:
                    drawer.OpenDrawer(GravityCompat.Start);
                    ISharedPreferences sharedPreferences = currentContext.GetSharedPreferences("app", FileCreationMode.Private);
                    var editor = sharedPreferences.Edit();
                    editor.PutBoolean("isFirst", false);
                    editor.Apply();
                    break;

                case MESSAGE_SHOW_START_PAGE:
                    AlphaAnimation alphaAnimation = new AlphaAnimation(1.0f, 0.0f);
                    alphaAnimation.Duration = 300;
                    alphaAnimation.SetAnimationListener(new AnimationListener());
                    relative_main.StartAnimation(alphaAnimation);
                    break;

                default:
                    break;
            }

        });
        class AnimationListener : Java.Lang.Object, Animation.IAnimationListener
        {
            public void OnAnimationEnd(Animation animation)
            {
                relative_main.Visibility = ViewStates.Gone;
            }

            public void OnAnimationRepeat(Animation animation)
            {
            }

            public void OnAnimationStart(Animation animation)
            {
            }
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            currentContext = this;

            InitView();
            InitViewPager();

            try
            {
                Android.Preferences.PreferenceManager.SetDefaultValues(this, Resource.Xml.preferences_settings, false);
            }
            catch (System.Exception ex)
            { }

            ISharedPreferences sharedPreferences = GetSharedPreferences("app", FileCreationMode.Private);

            if (isShowPageStart)
            {
                relative_main.Visibility = ViewStates.Visible;
                Glide.With(this).Load(Resource.Drawable.ic_launcher_big).Into(img_page_start);
                if (sharedPreferences.GetBoolean("isFirst", true))
                {
                    mHandler.SendEmptyMessageDelayed(MESSAGE_SHOW_START_PAGE, 2000);
                }
                else
                {
                    mHandler.SendEmptyMessageDelayed(MESSAGE_SHOW_START_PAGE, 1000);
                }
                isShowPageStart = false;
            }

            if (sharedPreferences.GetBoolean("isFirst", true))
            {
                mHandler.SendEmptyMessageDelayed(MESSAGE_SHOW_DRAWER_LAYOUT, 2500);
            }

        }

        public void OnClick(View view)
        {
            switch (view.Id)
            {
                case Resource.Id.nav_header:
                    Intent intent = new Intent(this, typeof(Class.Activitys.LoginActivity));
                    StartActivity(intent);
                    DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                    drawer.CloseDrawer(GravityCompat.Start);
                    break;

                case Resource.Id.fab_main:
                    Snackbar.Make(view, GetString(Resource.String.main_snack_bar), Snackbar.LengthLong)
                            .SetAction(GetString(Resource.String.main_snack_bar_action), (v) => { }).Show();
                    break;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_menu_main_1:
                    Intent intent = new Intent(this, typeof(Class.Activitys.AboutActivity));
                    StartActivity(intent);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            Intent intent = new Intent();

            switch (menuItem.ItemId)
            {
                case Resource.Id.nav_recycler_and_swipe_refresh:
                    intent.SetClass(this, typeof(Class.Activitys.RecyclerViewActivity));
                    StartActivity(intent);
                    break;

                case Resource.Id.nav_scrolling:
                    intent.SetClass(this, typeof(Class.Activitys.ScrollingActivity));
                    StartActivity(intent);
                    break;

                case Resource.Id.nav_full_screen:
                    intent.SetClass(this, typeof(Class.Activitys.FullscreenActivity));
                    StartActivity(intent);
                    break;

                case Resource.Id.nav_bottom_navigation:
                    intent.SetClass(this, typeof(Class.Activitys.BottomNavigationActivity));
                    StartActivity(intent);
                    break;

                case Resource.Id.nav_settings:
                    intent.SetClass(this, typeof(Class.Activitys.SettingsActivity));
                    StartActivity(intent);
                    break;

                case Resource.Id.nav_about:
                    intent.SetClass(this, typeof(Class.Activitys.AboutActivity));
                    StartActivity(intent);
                    break;

                case Resource.Id.nav_donate:
                    intent.SetClass(this, typeof(Class.Activitys.DonateActivity));
                    StartActivity(intent);
                    break;

                case Resource.Id.nav_color:
                    if (MaterialDesign.Class.Utils.AppUtils.CkeckAppInstalled(this, MaterialDesign.Class.Constant.MATERIAL_DESIGN_COLOR_PACKAGE))
                    {
                        intent = PackageManager.GetLaunchIntentForPackage(MaterialDesign.Class.Constant.MATERIAL_DESIGN_COLOR_PACKAGE);
                        if (intent != null)
                        {
                            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ResetTaskIfNeeded | ActivityFlags.ClearTop);
                        }
                        StartActivity(intent);
                    }
                    else
                    {
                        intent.SetData(Android.Net.Uri.Parse(MaterialDesign.Class.Constant.MATERIAL_DESIGN_COLOR_URL));
                        intent.SetAction(Intent.ActionView);
                        StartActivity(intent);
                    }
                    break;
            }


            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public override void OnBackPressed()
        {
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }
        protected override void OnDestroy()
        {
            mHandler.RemoveCallbacksAndMessages(null);
            base.OnDestroy();
        }

        private void InitView()
        {
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_main);
            this.SetSupportActionBar(toolbar);

            drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                    this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            navigationView.ItemIconTintList = null;

            View headerView = navigationView.GetHeaderView(0);
            LinearLayout nav_header = headerView.FindViewById<LinearLayout>(Resource.Id.nav_header);
            nav_header.SetOnClickListener(this);

            fab = FindViewById<FloatingActionButton>(Resource.Id.fab_main);
            fab.SetOnClickListener(this);

            relative_main = FindViewById<RelativeLayout>(Resource.Id.relative_main);
            img_page_start = FindViewById<ImageView>(Resource.Id.img_page_start);
        }
        private void InitViewPager()
        {
            mTabLayout = FindViewById<TabLayout>(Resource.Id.tab_layout_main);
            mViewPager = FindViewById<ViewPager>(Resource.Id.view_pager_main);

            List<Java.Lang.String> titles = new List<Java.Lang.String>();

            titles.Add(new Java.Lang.String(GetString(Resource.String.tab_title_main_1)));
            titles.Add(new Java.Lang.String(GetString(Resource.String.tab_title_main_2)));
            titles.Add(new Java.Lang.String(GetString(Resource.String.tab_title_main_3)));
            mTabLayout.AddTab(mTabLayout.NewTab().SetText(titles[0]));
            mTabLayout.AddTab(mTabLayout.NewTab().SetText(titles[1]));
            mTabLayout.AddTab(mTabLayout.NewTab().SetText(titles[2]));

            List<Android.Support.V4.App.Fragment> fragments = new List<Android.Support.V4.App.Fragment>();
            fragments.Add(new Class.fragment.CardsFragment());
            fragments.Add(new Class.fragment.DialogsFragment());
            fragments.Add(new Class.fragment.WidgetsFragment());

            mViewPager.OffscreenPageLimit = 2;

            Class.Adapters.FragmentAdapter mFragmentAdapter = new Class.Adapters.FragmentAdapter(SupportFragmentManager, fragments, titles);
            mViewPager.Adapter = mFragmentAdapter;
            mTabLayout.SetupWithViewPager(mViewPager);
            mTabLayout.SetTabsFromPagerAdapter(mFragmentAdapter);

            mViewPager.AddOnPageChangeListener(this);
        }



        public void OnPageScrollStateChanged(int state)
        {
        }
        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }
        public void OnPageSelected(int position)
        {
            if (position == 2)
            {
                fab.Show();
            }
            else
            {
                fab.Hide();
            }
        }
    }
}

