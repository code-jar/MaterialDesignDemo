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
using Org.Json;

namespace MaterialDesign.Class.Billings
{
    public class Purchase
    {
        String mItemType;  // ITEM_TYPE_INAPP or ITEM_TYPE_SUBS
        String mOrderId;
        String mPackageName;
        String mSku;
        long mPurchaseTime;
        int mPurchaseState;
        String mDeveloperPayload;
        String mToken;
        String mOriginalJson;
        String mSignature;
        bool mIsAutoRenewing;

        public Purchase(String itemType, String jsonPurchaseInfo, String signature)
        {
            mItemType = itemType;
            mOriginalJson = jsonPurchaseInfo;
            JSONObject o = new JSONObject(mOriginalJson);
            mOrderId = o.OptString("orderId");
            mPackageName = o.OptString("packageName");
            mSku = o.OptString("productId");
            mPurchaseTime = o.OptLong("purchaseTime");
            mPurchaseState = o.OptInt("purchaseState");
            mDeveloperPayload = o.OptString("developerPayload");
            mToken = o.OptString("token", o.OptString("purchaseToken"));
            mIsAutoRenewing = o.OptBoolean("autoRenewing");
            mSignature = signature;
        }

        public string getItemType() { return mItemType; }
        public string getOrderId() { return mOrderId; }
        public string getPackageName() { return mPackageName; }
        public string getSku() { return mSku; }
        public long getPurchaseTime() { return mPurchaseTime; }
        public int getPurchaseState() { return mPurchaseState; }
        public string getDeveloperPayload() { return mDeveloperPayload; }
        public string getToken() { return mToken; }
        public string getOriginalJson() { return mOriginalJson; }
        public string getSignature() { return mSignature; }
        public bool isAutoRenewing() { return mIsAutoRenewing; }

        public override string ToString()
        {
            return "PurchaseInfo(type:" + mItemType + "):" + mOriginalJson;
        }
    }
}