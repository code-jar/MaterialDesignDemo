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

namespace MaterialDesign.Class.Utils
{
    public class ObjectUtility
    {
        public static bool CheckInherit(Type child, Type parent)
        {
            if (child == null || parent == null || child == parent || child.BaseType == null)
                return false;

            if (parent.IsInterface)
                return child.GetInterfaces().Where(item => item == parent).Count() > 0;

            do
            {
                if (child.BaseType == parent)
                    return true;
                child = child.BaseType;
            } while (child != null);

            return false;
        }
    }
}