using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using MaterialDesign.Class.Interfaces;
using MaterialDesign.Class.Utils;
using static Android.Provider.CalendarContract;

namespace MaterialDesign.Class.Views
{
    public class ItemTouchHelperCallback : ItemTouchHelper.Callback
    {
        private IOnMoveAndSwipedListener moveAndSwipedListener;
        public ItemTouchHelperCallback(IOnMoveAndSwipedListener listener)
        {
            this.moveAndSwipedListener = listener;
        }
        public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
        {
            //Instances
            if (ObjectUtility.CheckInherit(recyclerView.GetLayoutManager().GetType(), typeof(GridLayoutManager)))
            {
                // for recyclerView with gridLayoutManager, support drag all directions, not support swipe
                int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down | ItemTouchHelper.Left | ItemTouchHelper.Right;
                int swipeFlags = 0;
                return MakeMovementFlags(dragFlags, swipeFlags);
            }
            else
            {
                // for recyclerView with linearLayoutManager, support drag up and down, and swipe lift and right
                int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
                int swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
                return MakeMovementFlags(dragFlags, swipeFlags);
            }
        }

        public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
        {
            if (viewHolder.ItemViewType != target.ItemViewType)
            {
                return false;
            }
            moveAndSwipedListener.OnItemMove(viewHolder.AdapterPosition, target.AdapterPosition);
            return true;
        }

        public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
        {
            moveAndSwipedListener.OnItemDismiss(viewHolder.AdapterPosition);
        }
    }
}