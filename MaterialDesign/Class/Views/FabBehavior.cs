using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.View.Animation;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Lang;

namespace MaterialDesign.Class.Views
{
    public class FabBehavior : CoordinatorLayout.Behavior
    {
        private static readonly IInterpolator INTERPOLATOR = new FastOutSlowInInterpolator();

        protected static float viewY; // Distance from fab to bottom of parent
        protected static bool isAnimate;

        public FabBehavior(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool OnStartNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View directTargetChild, View target, int axes)
        {
            var childView = (FloatingActionButton)child;
            if (childView.Visibility == ViewStates.Visible && viewY == 0)
            {
                viewY = coordinatorLayout.Height - childView.GetY();
            }

            return axes == ViewCompat.ScrollAxisVertical || base.OnStartNestedScroll(coordinatorLayout, child, directTargetChild, target, axes);
        }
        public override void OnNestedPreScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View target, int dx, int dy, int[] consumed)
        {
            var childView = (FloatingActionButton)child;
            if (dy >= 0 && !isAnimate && childView.Visibility == ViewStates.Visible)
            {
                Hide(childView);
            }
            else if (dy < 0 && !isAnimate && childView.Visibility == ViewStates.Invisible)
            {
                Show(childView);
            }
        }


        protected static void Show(View view)
        {
            ViewPropertyAnimator animator = view.Animate().TranslationY(0).SetInterpolator(INTERPOLATOR).SetDuration(300);

            animator.SetListener(new ShowAnimatorListener(view));

            animator.Start();
        }
        protected static void Hide(View view)
        {
            ViewPropertyAnimator animator = view.Animate().TranslationY(viewY).SetInterpolator(INTERPOLATOR).SetDuration(300);

            animator.SetListener(new HideAnimatorListener(view));

            animator.Start();

        }
        class ShowAnimatorListener : Java.Lang.Object, Animator.IAnimatorListener
        {
            private View view;
            public ShowAnimatorListener(View view)
            {
                this.view = view;
            }

            public void OnAnimationCancel(Animator animation)
            {
                Hide(view);
            }

            public void OnAnimationEnd(Animator animation)
            {
                isAnimate = false;
            }

            public void OnAnimationRepeat(Animator animation)
            {
            }

            public void OnAnimationStart(Animator animation)
            {
                view.Visibility = ViewStates.Visible;
                isAnimate = true;
            }
        }
        class HideAnimatorListener : Java.Lang.Object, Animator.IAnimatorListener
        {
            private View view;
            public HideAnimatorListener(View view)
            {
                this.view = view;
            }

            public void OnAnimationCancel(Animator animation)
            {
                Show(view);
            }

            public void OnAnimationEnd(Animator animation)
            {
                view.Visibility = ViewStates.Invisible;
                isAnimate = false;
            }

            public void OnAnimationRepeat(Animator animation)
            {
            }

            public void OnAnimationStart(Animator animation)
            {
                isAnimate = true;
            }
        }
    }

}