using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using MaterialDesign.Class.Activitys;
using MaterialDesign.Class.Interfaces;
using static Android.Resource;

namespace MaterialDesign.Class.Adapters
{
    public class RecyclerViewAdapter : RecyclerView.Adapter, IOnMoveAndSwipedListener
    {
        private Context context;
        private List<string> mItems;
        private int color = 0;
        private View parentView;

        private int TYPE_NORMAL = 1;
        private int TYPE_FOOTER = 2;
        private string FOOTER = "footer";

        public RecyclerViewAdapter(Context context)
        {
            this.context = context;
            mItems = new List<string>();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is RecyclerViewHolder)
            {
                RecyclerViewHolder recyclerViewHolder = (RecyclerViewHolder)holder;
                Android.Views.Animations.Animation animation = AnimationUtils.LoadAnimation(context, Resource.Animation.anim_recycler_item_show);
                recyclerViewHolder.mView.StartAnimation(animation);

                AlphaAnimation aa1 = new AlphaAnimation(1.0f, 0.1f);
                aa1.Duration = 400;
                recyclerViewHolder.rela_round.StartAnimation(aa1);

                AlphaAnimation aa = new AlphaAnimation(0.1f, 1.0f);
                aa.Duration = 400;

                if (color == 1)
                {
                    recyclerViewHolder.rela_round.BackgroundTintList = ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(context, Resource.Color.google_blue)));
                }
                else if (color == 2)
                {
                    recyclerViewHolder.rela_round.BackgroundTintList = ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(context, Resource.Color.google_green)));
                }
                else if (color == 3)
                {
                    recyclerViewHolder.rela_round.BackgroundTintList = ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(context, Resource.Color.google_yellow)));
                }
                else if (color == 4)
                {
                    recyclerViewHolder.rela_round.BackgroundTintList = ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(context, Resource.Color.google_red)));
                }
                else
                {
                    recyclerViewHolder.rela_round.BackgroundTintList = ColorStateList.ValueOf(new Android.Graphics.Color(Utils.AppUtils.GetColor(context, Resource.Color.gray)));

                }

                recyclerViewHolder.rela_round.StartAnimation(aa);

                recyclerViewHolder.mView.SetOnClickListener(new RecycleViewHolderClick(context, recyclerViewHolder, color));

            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            parentView = parent;
            if (viewType == TYPE_NORMAL)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_recycler_view, parent, false);
                return new RecyclerViewHolder(view);
            }
            else
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_recycler_footer, parent, false);
                return new FooterViewHolder(view);
            }
        }

        public override int GetItemViewType(int position)
        {
            string s = mItems[position];
            if (s.Equals(FOOTER))
            {
                return TYPE_FOOTER;
            }
            else
            {
                return TYPE_NORMAL;
            }
        }
        public override int ItemCount => mItems.Count;
        public bool OnItemMove(int fromPosition, int toPosition)
        {
            var temp = mItems[fromPosition];
            mItems[fromPosition] = mItems[toPosition];
            mItems[toPosition] = temp;

            NotifyItemMoved(fromPosition, toPosition);
            return true;
        }

        public void OnItemDismiss(int position)
        {
            mItems.RemoveAt(position);
            NotifyItemRemoved(position);


            Snackbar.Make(parentView, context.GetString(Resource.String.item_swipe_dismissed), Snackbar.LengthShort)
                    .SetAction(context.GetString(Resource.String.item_swipe_undo), new SnackbarOnClick(this, position, mItems)).Show();
        }

        public void SetItems(List<string> data)
        {
            mItems.AddRange(data);
            NotifyDataSetChanged();
        }

        public void AddItem(int position, string insertData)
        {
            mItems.Insert(position, insertData);
            NotifyItemInserted(position);
        }

        public void AddItems(IEnumerable<string> data)
        {
            mItems.AddRange(data);
            NotifyItemInserted(mItems.Count - 1);
        }

        public void AddFooter()
        {
            mItems.Add(FOOTER);
            NotifyItemInserted(mItems.Count - 1);
        }

        public void RemoveFooter()
        {
            mItems.RemoveAt(mItems.Count - 1);
            NotifyItemRemoved(mItems.Count);
        }

        public void SetColor(int color)
        {
            this.color = color;
            NotifyDataSetChanged();
        }

        public class RecyclerViewHolder : RecyclerView.ViewHolder
        {
            public View mView;
            public RelativeLayout rela_round;

            public RecyclerViewHolder(View itemView) : base(itemView)
            {
                mView = itemView;
                rela_round = itemView.FindViewById<RelativeLayout>(Resource.Id.rela_round);
            }
        }

        public class FooterViewHolder : RecyclerView.ViewHolder
        {
            public ProgressBar progress_bar_load_more;

            public FooterViewHolder(View itemView) : base(itemView)
            {
                progress_bar_load_more = itemView.FindViewById<ProgressBar>(Resource.Id.progress_bar_load_more);
            }
        }

        class SnackbarOnClick : Java.Lang.Object, View.IOnClickListener
        {
            RecyclerViewAdapter adapter;
            private int position;
            private List<string> mItems;
            public SnackbarOnClick(RecyclerViewAdapter adapter, int position, List<string> mItems)
            {
                this.adapter = adapter;
                this.position = position;
                this.mItems = mItems;
            }

            void View.IOnClickListener.OnClick(View v)
            {
                adapter.AddItem(position, mItems[position]);
            }
        }

        class RecycleViewHolderClick : Java.Lang.Object, View.IOnClickListener
        {
            private Context currentContext;
            private RecyclerViewHolder holder;
            private int color;
            public RecycleViewHolderClick(Context ctx, RecyclerViewHolder holder, int color)
            {
                this.currentContext = ctx;
                this.holder = holder;
                this.color = color;
            }

            public void OnClick(View v)
            {
                Intent intent = new Intent(currentContext, typeof(ShareViewActivity));
                intent.PutExtra("color", color);
                currentContext.StartActivity(intent, ActivityOptions.MakeSceneTransitionAnimation
                        ((Activity)currentContext, holder.rela_round, "shareView").ToBundle());
            }
        }

    }
}