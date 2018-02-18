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
    public class Inventory
    {
        Dictionary<string, SkuDetails> mSkuMap = new Dictionary<string, SkuDetails>();
        Dictionary<string, Purchase> mPurchaseMap = new Dictionary<string, Purchase>();

        Inventory() { }


        public SkuDetails GetSkuDetails(String sku)
        {
            return mSkuMap[sku];
        }

        /** Returns purchase information for a given product, or null if there is no purchase. */
        public Purchase GetPurchase(string sku)
        {
            return mPurchaseMap[sku];
        }

        /** Returns whether or not there exists a purchase of the given product. */
        public bool HasPurchase(String sku)
        {
            return mPurchaseMap.ContainsKey(sku);
        }

        /** Return whether or not details about the given product are available. */
        public bool HasDetails(String sku)
        {
            return mSkuMap.ContainsKey(sku);
        }

        public void ErasePurchase(String sku)
        {
            if (mPurchaseMap.ContainsKey(sku)) mPurchaseMap.Remove(sku);
        }

        /** Returns a list of all owned product IDs. */
        List<string> GetAllOwnedSkus()
        {
            return mPurchaseMap.Select(item => item.Key).ToList();
        }

        List<String> GetAllOwnedSkus(string itemType)
        {
            List<string> result = new List<string>();

            var values = mPurchaseMap.Select(item => item.Value);
            values.ToList().ForEach(item =>
            {

                if (item.GetType().ToString() == itemType)
                {
                    result.Add(item.getSku());
                }

            });

            return result;
        }

        /** Returns a list of all purchases. */
        List<Purchase> GetAllPurchases()
        {
            return mPurchaseMap.Select(item => item.Value).ToList();
        }

        void AddSkuDetails(SkuDetails d)
        {
            mSkuMap.Add(d.getSku(), d);
        }

        void AddPurchase(Purchase p)
        {
            mPurchaseMap.Add(p.getSku(), p);
        }

    }
}