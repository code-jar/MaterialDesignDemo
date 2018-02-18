using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MaterialDesign.Class.Adapters;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "RecyclerViewActivity")]
    public class RecyclerViewActivity : AppCompatActivity, View.IOnClickListener, SwipeRefreshLayout.IOnRefreshListener
    {
        private SwipeRefreshLayout swipeRefreshLayout;
        private RecyclerView mRecyclerView;
        private FloatingActionButton fab;
        private RecyclerViewAdapter adapter;
        private int color = 0;
        private List<string> data;
        private string insertData;
        private bool loading;
        private int loadTimes;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_recycler_view);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_recycler_view);
            SetSupportActionBar(toolbar);
            if (SupportActionBar != null)
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                toolbar.NavigationClick += (sender, args) => { OnBackPressed(); };
            }

            InitData();
            InitView();
        }

        private void InitView()
        {
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab_recycler_view);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view_recycler_view);

            if (GetScreenWidthDp() >= 1200)
            {
                GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 3);
                mRecyclerView.SetLayoutManager(gridLayoutManager);
            }
            else if (GetScreenWidthDp() >= 800)
            {
                GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 2);
                mRecyclerView.SetLayoutManager(gridLayoutManager);
            }
            else
            {
                LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
                mRecyclerView.SetLayoutManager(linearLayoutManager);
            }

            adapter = new RecyclerViewAdapter(this);
            mRecyclerView.SetAdapter(adapter);
            adapter.SetItems(data);
            adapter.AddFooter();

            fab.SetOnClickListener(this);
            ItemTouchHelper.Callback callback = new Views.ItemTouchHelperCallback(adapter);
            ItemTouchHelper mItemTouchHelper = new ItemTouchHelper(callback);
            mItemTouchHelper.AttachToRecyclerView(mRecyclerView);

            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_refresh_layout_recycler_view);
            swipeRefreshLayout.SetColorSchemeResources(Resource.Color.google_blue, Resource.Color.google_green, Resource.Color.google_red, Resource.Color.google_yellow);
            swipeRefreshLayout.SetOnRefreshListener(this);

            mRecyclerView.AddOnScrollListener(new OnScrollListener(this));
        }

        private void InitData()
        {
            data = new List<string>();
            for (int i = 1; i <= 20; i++)
            {
                data.Add(i + "");
            }

            insertData = "0";
            loadTimes = 0;
        }
        private int GetScreenWidthDp()
        {
            DisplayMetrics displayMetrics = Resources.DisplayMetrics;
            return (int)(displayMetrics.WidthPixels / displayMetrics.Density);
        }

        public void OnClick(View v)
        {
            LinearLayoutManager linearLayoutManager = (LinearLayoutManager)mRecyclerView.GetLayoutManager();
            adapter.AddItem(linearLayoutManager.FindFirstVisibleItemPosition() + 1, insertData);
        }

        public void OnRefresh()
        {
            new Handler().PostDelayed(() =>
            {

                if (color > 4)
                {
                    color = 0;
                }
                adapter.SetColor(++color);
                swipeRefreshLayout.Refreshing = false;

            }, 2000);
        }


        class OnScrollListener : RecyclerView.OnScrollListener
        {
            private RecyclerViewActivity currentActivity;
            public OnScrollListener(RecyclerViewActivity activity)
            {
                currentActivity = activity;
            }
            public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
            {
                base.OnScrolled(recyclerView, dx, dy);

                LinearLayoutManager linearLayoutManager = (LinearLayoutManager)recyclerView.GetLayoutManager();

                if (!currentActivity.loading && linearLayoutManager.ItemCount == (linearLayoutManager.FindLastVisibleItemPosition() + 1))
                {

                    new Handler().PostDelayed(() =>
                    {

                        if (currentActivity.loadTimes <= 5)
                        {
                            currentActivity.adapter.RemoveFooter();
                            currentActivity.loading = false;
                            currentActivity.adapter.AddItems(currentActivity.data);
                            currentActivity.adapter.AddFooter();
                            currentActivity.loadTimes++;
                        }
                        else
                        {
                            currentActivity.adapter.RemoveFooter();
                            var snackbar = Snackbar.Make(currentActivity.mRecyclerView,
                                 currentActivity.GetString(Resource.String.no_more_data),
                                 Snackbar.LengthShort);
                            snackbar.AddCallback(new MyCallback(currentActivity));
                        }

                    }, 1500);

                    currentActivity.loading = true;
                }
            }

            class MyCallback : BaseTransientBottomBar.BaseCallback
            {
                private RecyclerViewActivity currentActivity;
                public MyCallback(RecyclerViewActivity activity)
                {
                    currentActivity = activity;
                }
                public override void OnDismissed(Java.Lang.Object transientBottomBar, int @event)
                {
                    base.OnDismissed(transientBottomBar, @event);
                    currentActivity.loading = false;
                    currentActivity.adapter.AddFooter();
                }
            }

        }

    }
}