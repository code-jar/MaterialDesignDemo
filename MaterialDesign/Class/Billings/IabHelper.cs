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
    public class IabHelper
    {
        // Is debug logging enabled?
        bool mDebugLog = false;
        String mDebugTag = "IabHelper";

        // Is setup done?
        bool mSetupDone = false;

        // Has this object been disposed of? (If so, we should ignore callbacks, etc)
        bool mDisposed = false;

        // Do we need to dispose this object after an in-progress asynchronous operation?
        bool mDisposeAfterAsync = false;

        // Are subscriptions supported?
        bool mSubscriptionsSupported = false;

        // Is subscription update supported?
        bool mSubscriptionUpdateSupported = false;

        // Is an asynchronous operation in progress?
        // (only one at a time can be in progress)
        bool mAsyncInProgress = false;

        // Ensure atomic access to mAsyncInProgress and mDisposeAfterAsync.
        private Object mAsyncInProgressLock = new Object();

        // (for logging/debugging)
        // if mAsyncInProgress == true, what asynchronous operation is in progress?
        String mAsyncOperation = "";

        // Context we were passed during initialization
        Context mContext;

        // Connection to the service
        //IInAppBillingService mService;
        Android.Content.IServiceConnection mServiceConn;

        // The request code used to launch purchase flow
        int mRequestCode;

        // The item type of the current purchase flow
        String mPurchasingItemType;

        // Public key for verifying signature, in base64 encoding
        String mSignatureBase64 = null;

        // Billing response codes
        public static int BILLING_RESPONSE_RESULT_OK = 0;
        public static int BILLING_RESPONSE_RESULT_USER_CANCELED = 1;
        public static int BILLING_RESPONSE_RESULT_SERVICE_UNAVAILABLE = 2;
        public static int BILLING_RESPONSE_RESULT_BILLING_UNAVAILABLE = 3;
        public static int BILLING_RESPONSE_RESULT_ITEM_UNAVAILABLE = 4;
        public static int BILLING_RESPONSE_RESULT_DEVELOPER_ERROR = 5;
        public static int BILLING_RESPONSE_RESULT_ERROR = 6;
        public static int BILLING_RESPONSE_RESULT_ITEM_ALREADY_OWNED = 7;
        public static int BILLING_RESPONSE_RESULT_ITEM_NOT_OWNED = 8;

        // IAB Helper error codes
        public static int IABHELPER_ERROR_BASE = -1000;
        public static int IABHELPER_REMOTE_EXCEPTION = -1001;
        public static int IABHELPER_BAD_RESPONSE = -1002;
        public static int IABHELPER_VERIFICATION_FAILED = -1003;
        public static int IABHELPER_SEND_INTENT_FAILED = -1004;
        public static int IABHELPER_USER_CANCELLED = -1005;
        public static int IABHELPER_UNKNOWN_PURCHASE_RESPONSE = -1006;
        public static int IABHELPER_MISSING_TOKEN = -1007;
        public static int IABHELPER_UNKNOWN_ERROR = -1008;
        public static int IABHELPER_SUBSCRIPTIONS_NOT_AVAILABLE = -1009;
        public static int IABHELPER_INVALID_CONSUMPTION = -1010;
        public static int IABHELPER_SUBSCRIPTION_UPDATE_NOT_AVAILABLE = -1011;

        // Keys for the responses from InAppBillingService
        public static string RESPONSE_CODE = "RESPONSE_CODE";
        public static string RESPONSE_GET_SKU_DETAILS_LIST = "DETAILS_LIST";
        public static string RESPONSE_BUY_INTENT = "BUY_INTENT";
        public static string RESPONSE_INAPP_PURCHASE_DATA = "INAPP_PURCHASE_DATA";
        public static string RESPONSE_INAPP_SIGNATURE = "INAPP_DATA_SIGNATURE";
        public static string RESPONSE_INAPP_ITEM_LIST = "INAPP_PURCHASE_ITEM_LIST";
        public static string RESPONSE_INAPP_PURCHASE_DATA_LIST = "INAPP_PURCHASE_DATA_LIST";
        public static string RESPONSE_INAPP_SIGNATURE_LIST = "INAPP_DATA_SIGNATURE_LIST";
        public static string INAPP_CONTINUATION_TOKEN = "INAPP_CONTINUATION_TOKEN";

        // Item types
        public static string ITEM_TYPE_INAPP = "inapp";
        public static string ITEM_TYPE_SUBS = "subs";

        // some fields on the getSkuDetails response bundle
        public static string GET_SKU_DETAILS_ITEM_LIST = "ITEM_ID_LIST";
        public static string GET_SKU_DETAILS_ITEM_TYPE_LIST = "ITEM_TYPE_LIST";


        public IabHelper(Context ctx, String base64PublicKey)
        {
            mContext = ctx.ApplicationContext;
            mSignatureBase64 = base64PublicKey;
            //logDebug("IAB helper created.");
        }

        public void EnableDebugLogging(bool enable)
        {
            //checkNotDisposed();
            mDebugLog = enable;
        }

        public interface IOnIabSetupFinishedListener
        {
            void OnIabSetupFinished(IabResult result);
        }

        public void startSetup(IOnIabSetupFinishedListener listener)
        {
            //checkNotDisposed();
            if (mSetupDone) throw new Java.Lang.IllegalStateException("IAB helper is already set up.");
            // Connection to IAB service
            logDebug("Starting in-app billing setup.");

            mServiceConn = new ServiceConnection();

        }
        class ServiceConnection : Java.Lang.Object, IServiceConnection
        {
            public void OnServiceConnected(ComponentName name, IBinder service)
            {
                throw new NotImplementedException();
            }

            public void OnServiceDisconnected(ComponentName name)
            {
                //logDebug("Billing service disconnected.");
                //mService = null;
            }
        }

        public void dispose()
        {
            lock (mAsyncInProgressLock)
            {
                if (mAsyncInProgress)
                {
                    throw new Exception($"Can't dispose because an async operation {mAsyncOperation} is in progress.");
                }
            }
            logDebug("Disposing.");
            mSetupDone = false;
            if (mServiceConn != null)
            {
                logDebug("Unbinding from service.");
                if (mContext != null) mContext.UnbindService(mServiceConn);
            }
            mDisposed = true;
            mContext = null;
            mServiceConn = null;
            //mService = null;
            mPurchaseListener = null;
        }

        public interface IOnIabPurchaseFinishedListener
        {
            void OnIabPurchaseFinished(IabResult result, Purchase info);
        }
        IOnIabPurchaseFinishedListener mPurchaseListener;
        public void launchPurchaseFlow(Activity act, String sku, int requestCode, IOnIabPurchaseFinishedListener listener)
        {
            launchPurchaseFlow(act, sku, requestCode, listener, "");
        }

        public void launchPurchaseFlow(Activity act, String sku, int requestCode, IOnIabPurchaseFinishedListener listener, String extraData)
        {
            //launchPurchaseFlow(act, sku, ITEM_TYPE_INAPP, null, requestCode, listener, extraData);
        }

        public void launchSubscriptionPurchaseFlow(Activity act, String sku, int requestCode, IOnIabPurchaseFinishedListener listener)
        {
            launchSubscriptionPurchaseFlow(act, sku, requestCode, listener, "");
        }

        public void launchSubscriptionPurchaseFlow(Activity act, String sku, int requestCode, IOnIabPurchaseFinishedListener listener, String extraData)
        {
            //launchPurchaseFlow(act, sku, ITEM_TYPE_SUBS, null, requestCode, listener, extraData);
        }

        void logDebug(String msg)
        {
            if (mDebugLog)
                Android.Util.Log.Debug(mDebugTag, msg);
        }

        void logError(String msg)
        {
            Android.Util.Log.Error(mDebugTag, "In-app billing error: " + msg);
        }

        void logWarn(String msg)
        {
            Android.Util.Log.Warn(mDebugTag, "In-app billing warning: " + msg);
        }


    }
}