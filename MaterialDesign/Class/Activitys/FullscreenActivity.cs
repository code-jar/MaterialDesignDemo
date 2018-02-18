using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "FullscreenActivity")]
    public class FullscreenActivity : AppCompatActivity
    {
        private VideoView video_fullscreen;
        private RelativeLayout relative_fullscreen;
        private ProgressBar progress_fullscreen;
        private bool isShowBar;

        private const int MESSAGE_HIDE_BARS = 0x001;
        private const int MESSAGE_VIDEO_PREPARED = 0x002;
        public Handler mHandler;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_fullscreen);
            mHandler = new Handler((message) =>
              {

                  switch (message.What)
                  {
                      case MESSAGE_HIDE_BARS:
                          new FullscreenActivity().HideBars();
                          break;
                      case MESSAGE_VIDEO_PREPARED:
                          Animation animation = new AlphaAnimation(1.0f, 0.0f);
                          animation.Duration = 500;
                          relative_fullscreen.StartAnimation(animation);
                          relative_fullscreen.Visibility = ViewStates.Gone;
                          break;
                  }

              });

            var actionBar = SupportActionBar;
            if (actionBar != null)
            {
                actionBar.SetDisplayHomeAsUpEnabled(true);
                actionBar.Hide();
            }

            progress_fullscreen = FindViewById<ProgressBar>(Resource.Id.progress_fullscreen);
            relative_fullscreen = FindViewById<RelativeLayout>(Resource.Id.relative_fullscreen);
            video_fullscreen = FindViewById<VideoView>(Resource.Id.video_fullscreen);
        }
        protected override void OnStop()
        {
            base.OnStop();
            relative_fullscreen.Visibility = ViewStates.Visible;
        }
        protected override void OnDestroy()
        {
            mHandler.RemoveCallbacksAndMessages(null);
            base.OnDestroy();
        }
        protected override void OnResume()
        {
            base.OnResume();
            HideBars();
            PlayVideo();
        }

        private void ShowBars()
        {
            isShowBar = true;
            video_fullscreen.SystemUiVisibility = StatusBarVisibility.Visible;

            Window.ClearFlags(WindowManagerFlags.TranslucentStatus | WindowManagerFlags.TranslucentNavigation);

            Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetStatusBarColor(Android.Graphics.Color.Gray);
            Window.SetNavigationBarColor(Android.Graphics.Color.Gray);

            mHandler.RemoveMessages(MESSAGE_HIDE_BARS);
            mHandler.SendEmptyMessageDelayed(MESSAGE_HIDE_BARS, 2000);
        }
        private void HideBars()
        {
            isShowBar = false;
            video_fullscreen.SystemUiVisibility = StatusBarVisibility.Visible;
        }
        private void PlayVideo()
        {
            var uri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + Resource.Raw.full_screen_google);
            video_fullscreen.SetVideoURI(uri);

            video_fullscreen.SetOnPreparedListener(new OnPreparedListener(this));
            video_fullscreen.SetOnCompletionListener(new OnCompletionListener(this));
            video_fullscreen.SetOnErrorListener(new OnErrorListener(this));
            video_fullscreen.SetOnTouchListener(new OnTouchListener(this));

            video_fullscreen.Start();
        }
        class OnPreparedListener : Java.Lang.Object, Android.Media.MediaPlayer.IOnPreparedListener
        {
            private FullscreenActivity currentAcitvity;
            public OnPreparedListener(FullscreenActivity activity)
            {
                this.currentAcitvity = activity;
            }
            public void OnPrepared(MediaPlayer mp)
            {
                currentAcitvity.mHandler.SendEmptyMessageDelayed(MESSAGE_VIDEO_PREPARED, 500);
            }
        }
        class OnCompletionListener : Java.Lang.Object, Android.Media.MediaPlayer.IOnCompletionListener
        {
            private FullscreenActivity currentAcitvity;
            public OnCompletionListener(FullscreenActivity activity)
            {
                this.currentAcitvity = activity;
            }
            public void OnCompletion(MediaPlayer mp)
            {
                currentAcitvity.video_fullscreen.Start();
            }
        }
        class OnErrorListener : Java.Lang.Object, Android.Media.MediaPlayer.IOnErrorListener
        {
            private FullscreenActivity currentAcitvity;
            public OnErrorListener(FullscreenActivity activity)
            {
                this.currentAcitvity = activity;
            }
            public bool OnError(MediaPlayer mp, [GeneratedEnum] MediaError what, int extra)
            {
                currentAcitvity.progress_fullscreen.Visibility = ViewStates.Visible;
                return true;
            }
        }
        class OnTouchListener : Java.Lang.Object, View.IOnTouchListener
        {
            private FullscreenActivity currentActivity;
            public OnTouchListener(FullscreenActivity activity)
            {
                this.currentActivity = activity;
            }
            public bool OnTouch(View v, MotionEvent e)
            {
                if (e.Action == MotionEventActions.Down)
                    if (!currentActivity.isShowBar)
                    {
                        currentActivity.ShowBars();
                    }
                    else
                    {
                        currentActivity.HideBars();
                    }
                return true;
            }
        }
    }
}