using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace MaterialDesign.Class.Views
{
    public class BottomNavigationViewHelper
    {
        public static void disableShiftMode(BottomNavigationView navigationView)
        {

            BottomNavigationMenuView menuView = (BottomNavigationMenuView)navigationView.GetChildAt(0);


            try
            {

                var shiftingMode = typeof(BottomNavigationMenuView).GetField("mShiftingMode");

                //shiftingMode.setAccessible(true);
                //shiftingMode.setBoolean(menuView, false);
                //shiftingMode.SetValue(menuView, false);
                //shiftingMode.setAccessible(false);

                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    BottomNavigationItemView itemView = (BottomNavigationItemView)menuView.GetChildAt(i);
                    itemView.SetShiftingMode(false);
                    itemView.SetChecked(itemView.ItemData.IsChecked);
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}