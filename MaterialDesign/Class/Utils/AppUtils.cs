using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MaterialDesign.Class.Utils
{
    public class AppUtils
    {
        public static bool CkeckAppInstalled(Context context, String packageName)
        {
            try
            {
                var manager = context.PackageManager;
                manager.GetPackageInfo(packageName, PackageInfoFlags.Activities);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static String GetVersionName(Context context)
        {
            try
            {
                var manager = context.PackageManager;
                PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);
                String version = info.VersionName;
                return context.GetString(Resource.String.about_version) + " " + version;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static int GetColor(Context context, int id)
        {
            BuildVersionCodes version = Build.VERSION.SdkInt;
            if ((int)version >= 23)
            {
                return Android.Support.V4.Content.ContextCompat.GetColor(context, id);
            }
            else
            {
                return context.Resources.GetColor(id);
            }
        }
    }
}