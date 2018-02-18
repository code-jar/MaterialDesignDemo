using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MaterialDesign.Class.Billings
{
    public class IabBroadcastReceiver : BroadcastReceiver
    {
        public interface IabBroadcastListener
        {
            void receivedBroadcast();
        }
        public static String ACTION = "com.android.vending.billing.PURCHASES_UPDATED";

        private IabBroadcastListener mListener;

        public IabBroadcastReceiver(IabBroadcastListener listener)
        {
            mListener = listener;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            if (mListener != null)
            {
                mListener.receivedBroadcast();
            }
        }
    }
}