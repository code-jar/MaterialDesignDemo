using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;

namespace MaterialDesign.Class.fragment
{
    public class CardsFragment : Fragment, View.IOnClickListener, View.IOnTouchListener
    {
        private ImageView img_main_card2_share, img_main_card2_bookmark, img_main_card2_favorite;
        private bool isBookmarkClicked, isFavoriteClicked, isBookmark41Clicked, isBookmark42Clicked, isFavorite41Clicked, isFavorite42Clicked;
        private LinearLayout ll_card_main3_rate;
        private Button btn_card_main1_action1, btn_card_main1_action2;
        private ImageView img_main_card_1, img_main_card_2, img_card_main_3, img_main_card_41, img_main_card_42,
                img_main_card41_favorite, img_main_card42_favorite, img_main_card41_bookmark, img_main_card42_bookmark,
                img_main_card41_share, img_main_card42_share;
        private CardView card_main_1_1, card_main_1_2, card_main_1_3, card_main_1_4_1, card_main_1_4_2;
        private AlphaAnimation alphaAnimation, alphaAnimationShowIcon;
        //private AdView ad_view;
        private CardView card_ad;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            NestedScrollView nestedScrollView = (NestedScrollView)inflater.Inflate(Resource.Layout.fragment_cards, container, false);

            btn_card_main1_action1 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_card_main1_action1);
            btn_card_main1_action2 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_card_main1_action2);
            img_main_card2_share = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card2_share);
            img_main_card2_bookmark = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card2_bookmark);
            img_main_card2_favorite = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card2_favorite);
            ll_card_main3_rate = nestedScrollView.FindViewById<LinearLayout>(Resource.Id.ll_card_main3_rate);

            img_main_card_1 = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card_1);
            img_main_card_2 = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card_2);
            img_card_main_3 = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_card_main_3);
            img_main_card_41 = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card_41);
            img_main_card_42 = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card_42);

            img_main_card41_favorite = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card41_favorite);
            img_main_card42_favorite = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card42_favorite);
            img_main_card41_bookmark = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card41_bookmark);
            img_main_card42_bookmark = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card42_bookmark);
            img_main_card41_share = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card41_share);
            img_main_card42_share = nestedScrollView.FindViewById<ImageView>(Resource.Id.img_main_card42_share);

            card_main_1_1 = nestedScrollView.FindViewById<CardView>(Resource.Id.card_main_1_1);
            card_main_1_2 = nestedScrollView.FindViewById<CardView>(Resource.Id.card_main_1_2);
            card_main_1_3 = nestedScrollView.FindViewById<CardView>(Resource.Id.card_main_1_3);
            card_main_1_4_1 = nestedScrollView.FindViewById<CardView>(Resource.Id.card_main_1_4_1);
            card_main_1_4_2 = nestedScrollView.FindViewById<CardView>(Resource.Id.card_main_1_4_2);

            Glide.With(this).Load(Resource.Drawable.material_design_2).Apply(RequestOptions.FitCenterTransform()).Into(img_main_card_1);
            Glide.With(this).Load(Resource.Drawable.material_design_4).Apply(RequestOptions.FitCenterTransform()).Into(img_main_card_2);
            Glide.With(this).Load(Resource.Drawable.material_design_11).Apply(RequestOptions.FitCenterTransform()).Into(img_card_main_3);
            Glide.With(this).Load(Resource.Drawable.material_design_1).Apply(RequestOptions.FitCenterTransform()).Into(img_main_card_41);
            Glide.With(this).Load(Resource.Drawable.material_design_1).Apply(RequestOptions.FitCenterTransform()).Into(img_main_card_42);

            //ad_view = nestedScrollView.FindViewById(Resource.Id.ad_view);
            card_ad = nestedScrollView.FindViewById<CardView>(Resource.Id.card_ad);

            return nestedScrollView;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            btn_card_main1_action1.SetOnClickListener(this);
            btn_card_main1_action2.SetOnClickListener(this);
            img_main_card2_bookmark.SetOnClickListener(this);
            img_main_card2_favorite.SetOnClickListener(this);
            img_main_card2_share.SetOnClickListener(this);
            ll_card_main3_rate.SetOnClickListener(this);

            img_main_card41_favorite.SetOnClickListener(this);
            img_main_card42_favorite.SetOnClickListener(this);
            img_main_card41_bookmark.SetOnClickListener(this);
            img_main_card42_bookmark.SetOnClickListener(this);
            img_main_card41_share.SetOnClickListener(this);
            img_main_card42_share.SetOnClickListener(this);

            card_main_1_1.SetOnClickListener(this);
            card_main_1_2.SetOnClickListener(this);
            card_main_1_3.SetOnClickListener(this);
            card_main_1_4_1.SetOnClickListener(this);
            card_main_1_4_2.SetOnClickListener(this);

            card_main_1_1.SetOnTouchListener(this);
            card_main_1_2.SetOnTouchListener(this);
            card_main_1_3.SetOnTouchListener(this);
            card_main_1_4_1.SetOnTouchListener(this);
            card_main_1_4_2.SetOnTouchListener(this);

            alphaAnimation = new AlphaAnimation(0.0f, 1.0f);
            alphaAnimation.Duration = 700;
            img_main_card_1.StartAnimation(alphaAnimation);
            img_main_card_2.StartAnimation(alphaAnimation);

            alphaAnimationShowIcon = new AlphaAnimation(0.2f, 1.0f);
            alphaAnimationShowIcon.Duration = 500;

            //showAd();
        }
        public void OnClick(View view)
        {
            switch (view.Id)
            {
                case Resource.Id.btn_card_main1_action1:
                    Snackbar.Make(view, view.Context.GetString(Resource.String.main_flat_button_1_clicked), Snackbar.LengthShort).Show();
                    break;

                case Resource.Id.btn_card_main1_action2:
                    Snackbar.Make(view, view.Context.GetString(Resource.String.main_flat_button_2_clicked), Snackbar.LengthShort).Show();
                    break;

                case Resource.Id.img_main_card2_bookmark:
                    if (!isBookmarkClicked)
                    {
                        img_main_card2_bookmark.SetImageResource(Resource.Drawable.ic_bookmark_black_24dp);
                        img_main_card2_bookmark.StartAnimation(alphaAnimationShowIcon);
                        isBookmarkClicked = true;
                    }
                    else
                    {
                        img_main_card2_bookmark.SetImageResource(Resource.Drawable.ic_bookmark_border_black_24dp);
                        img_main_card2_bookmark.StartAnimation(alphaAnimationShowIcon);
                        isBookmarkClicked = false;
                    }
                    break;

                case Resource.Id.img_main_card2_favorite:
                    if (!isFavoriteClicked)
                    {
                        img_main_card2_favorite.SetImageResource(Resource.Drawable.ic_favorite_black_24dp);
                        img_main_card2_favorite.StartAnimation(alphaAnimationShowIcon);
                        img_main_card2_favorite.StartAnimation(alphaAnimationShowIcon);
                        isFavoriteClicked = true;
                    }
                    else
                    {
                        img_main_card2_favorite.SetImageResource(Resource.Drawable.ic_favorite_border_black_24dp);
                        img_main_card2_favorite.StartAnimation(alphaAnimationShowIcon);
                        img_main_card2_favorite.StartAnimation(alphaAnimationShowIcon);
                        isFavoriteClicked = false;
                    }
                    break;

                case Resource.Id.img_main_card41_favorite:
                    if (!isFavorite41Clicked)
                    {
                        img_main_card41_favorite.SetImageResource(Resource.Drawable.ic_favorite_black_24dp);
                        img_main_card41_favorite.StartAnimation(alphaAnimationShowIcon);
                        isFavorite41Clicked = true;
                    }
                    else
                    {
                        img_main_card41_favorite.SetImageResource(Resource.Drawable.ic_favorite_border_black_24dp);
                        img_main_card41_favorite.StartAnimation(alphaAnimationShowIcon);
                        isFavorite41Clicked = false;
                    }
                    break;

                case Resource.Id.img_main_card42_favorite:
                    if (!isFavorite42Clicked)
                    {
                        img_main_card42_favorite.SetImageResource(Resource.Drawable.ic_favorite_black_24dp);
                        img_main_card42_favorite.StartAnimation(alphaAnimationShowIcon);
                        isFavorite42Clicked = true;
                    }
                    else
                    {
                        img_main_card42_favorite.SetImageResource(Resource.Drawable.ic_favorite_border_black_24dp);
                        img_main_card42_favorite.StartAnimation(alphaAnimationShowIcon);
                        isFavorite42Clicked = false;
                    }
                    break;

                case Resource.Id.img_main_card41_bookmark:
                    if (!isBookmark41Clicked)
                    {
                        img_main_card41_bookmark.SetImageResource(Resource.Drawable.ic_bookmark_black_24dp);
                        img_main_card41_bookmark.StartAnimation(alphaAnimationShowIcon);
                        isBookmark41Clicked = true;
                    }
                    else
                    {
                        img_main_card41_bookmark.SetImageResource(Resource.Drawable.ic_bookmark_border_black_24dp);
                        img_main_card41_bookmark.StartAnimation(alphaAnimationShowIcon);
                        isBookmark41Clicked = false;
                    }
                    break;

                case Resource.Id.img_main_card42_bookmark:
                    if (!isBookmark42Clicked)
                    {
                        img_main_card42_bookmark.SetImageResource(Resource.Drawable.ic_bookmark_black_24dp);
                        img_main_card42_bookmark.StartAnimation(alphaAnimationShowIcon);
                        isBookmark42Clicked = true;
                    }
                    else
                    {
                        img_main_card42_bookmark.SetImageResource(Resource.Drawable.ic_bookmark_border_black_24dp);
                        img_main_card42_bookmark.StartAnimation(alphaAnimationShowIcon);
                        isBookmark42Clicked = false;
                    }
                    break;

                case Resource.Id.img_main_card2_share:
                    break;

                case Resource.Id.img_main_card41_share:
                    break;

                case Resource.Id.img_main_card42_share:
                    break;

                case Resource.Id.ll_card_main3_rate:
                    break;

                case Resource.Id.card_main_1_1:
                    break;

                case Resource.Id.card_main_1_2:
                    break;

                case Resource.Id.card_main_1_3:
                    break;

                case Resource.Id.card_main_1_4_1:
                    break;

                case Resource.Id.card_main_1_4_2:
                    break;
            }
        }
        public bool OnTouch(View view, MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    ObjectAnimator upAnim = ObjectAnimator.OfFloat(view, "translationZ", 16);
                    upAnim.SetDuration(150);
                    upAnim.SetInterpolator(new DecelerateInterpolator());
                    upAnim.Start();
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    ObjectAnimator downAnim = ObjectAnimator.OfFloat(view, "translationZ", 0);
                    downAnim.SetDuration(150);
                    downAnim.SetInterpolator(new AccelerateInterpolator());
                    downAnim.Start();
                    break;
            }
            return false;
        }
    }
}