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
    public class SkuDetails
    {
        private String mItemType;
        private String mSku;
        private String mType;
        private String mPrice;
        private long mPriceAmountMicros;
        private String mPriceCurrencyCode;
        private String mTitle;
        private String mDescription;
        private String mJson;

        public SkuDetails(String jsonSkuDetails) : this(IabHelper.ITEM_TYPE_INAPP, jsonSkuDetails)
        {
        }
        public SkuDetails(String itemType, String jsonSkuDetails)
        {
            mItemType = itemType;
            mJson = jsonSkuDetails;
            JSONObject o = new JSONObject(mJson);
            mSku = o.OptString("productId");
            mType = o.OptString("type");
            mPrice = o.OptString("price");
            mPriceAmountMicros = o.OptLong("price_amount_micros");
            mPriceCurrencyCode = o.OptString("price_currency_code");
            mTitle = o.OptString("title");
            mDescription = o.OptString("description");
        }
        public String getSku() { return mSku; }
        public String getType() { return mType; }
        public String getPrice() { return mPrice; }
        public long getPriceAmountMicros() { return mPriceAmountMicros; }
        public String getPriceCurrencyCode() { return mPriceCurrencyCode; }
        public String getTitle() { return mTitle; }
        public String getDescription() { return mDescription; }

        public override string ToString()
        {
            return "SkuDetails:" + mJson;
        }


    }
}