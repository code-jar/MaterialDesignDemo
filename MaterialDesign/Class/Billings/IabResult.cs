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
    public class IabResult
    {
        int mResponse;
        string mMessage;

        public IabResult(int response, String message)
        {
            mResponse = response;
            if (message == null || message.Trim().Length == 0)
            {
                //mMessage = IabHelper.getResponseDesc(response);
            }
            else
            {
                //mMessage = message + " (response: " + IabHelper.getResponseDesc(response) + ")";
            }
        }
        public int getResponse() { return mResponse; }
        public String getMessage() { return mMessage; }
        public bool isSuccess() { return mResponse == IabHelper.BILLING_RESPONSE_RESULT_OK; }
        public bool isFailure() { return !isSuccess(); }
        public String toString() { return "IabResult: " + getMessage(); }


    }
}