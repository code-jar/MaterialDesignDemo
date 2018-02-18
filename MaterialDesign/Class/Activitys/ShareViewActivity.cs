using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "ShareViewActivity")]
    public class ShareViewActivity : AppCompatActivity,
        View.IOnClickListener,
        View.IOnTouchListener,
        Animation.IAnimationListener
    {
        private TextView tv_share_view_tip;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_share_view);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_share_view);
            toolbar.Subtitle = "Shared Element Transitions";
            toolbar.SetNavigationIcon(Resource.Drawable.ic_close_white_24dp);
            SetSupportActionBar(toolbar);

            toolbar.SetNavigationOnClickListener(this);

            InitView();
        }

        private void InitView()
        {
            CardView card_share_view = FindViewById<CardView>(Resource.Id.card_share_view);
            RelativeLayout rela_round_big = FindViewById<RelativeLayout>(Resource.Id.rela_round_big);
            tv_share_view_tip = FindViewById<TextView>(Resource.Id.tv_share_view_tip);

            if (Intent != null)
            {
                int color = Intent.GetIntExtra("color", 0);
                if (color == 1)
                {
                    rela_round_big.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(this, Resource.Color.google_blue)));
                }
                else if (color == 2)
                {
                    rela_round_big.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(this, Resource.Color.google_green)));
                }
                else if (color == 3)
                {
                    rela_round_big.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(this, Resource.Color.google_yellow)));
                }
                else if (color == 4)
                {
                    rela_round_big.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(this, Resource.Color.google_red)));
                }
                else
                {
                    rela_round_big.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(this, Resource.Color.gray)));
                }

                card_share_view.SetOnTouchListener(this);

                AlphaAnimation alphaAnimation = new AlphaAnimation(1.0f, 0.0f);
                alphaAnimation.Duration = 1500;
                alphaAnimation.StartOffset = 1000;
                alphaAnimation.SetAnimationListener(this);

                tv_share_view_tip.StartAnimation(alphaAnimation);

            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    ObjectAnimator upAnim = ObjectAnimator.OfFloat(v, "translationZ", 0);
                    upAnim.SetDuration(200);
                    upAnim.SetInterpolator(new DecelerateInterpolator());
                    upAnim.Start();
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    ObjectAnimator downAnim = ObjectAnimator.OfFloat(v, "translationZ", 20);
                    downAnim.SetDuration(200);
                    downAnim.SetInterpolator(new AccelerateInterpolator());
                    downAnim.Start();
                    break;
            }
            return false;
        }

        public void OnClick(View v)
        {
            OnBackPressed();
        }

        public void OnAnimationEnd(Animation animation)
        {
            tv_share_view_tip.Visibility = ViewStates.Gone;
        }
        public void OnAnimationRepeat(Animation animation)
        {
        }
        public void OnAnimationStart(Animation animation)
        {
        }
    }
}