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

namespace MaterialDesign.Class
{
    public class Constant
    {

        public static String APP_URL = "https://play.google.com/store/apps/details?id=com.eajy.materialdesigndemo";
        private static String DESIGNED_BY = "Designed by Eajy in China";
        public static String SHARE_CONTENT = "A beautiful app designed with Material Design:\n" + APP_URL + "\n- " + DESIGNED_BY;
        public static String EMAIL = "";
        public static String GIT_HUB = "https://github.com/code-jar/MaterialDesignDemo.git";

        public static String MATERIAL_DESIGN_COLOR_URL = "https://play.google.com/store/apps/details?id=com.eajy.materialdesigncolor";
        public static String MATERIAL_DESIGN_COLOR_PACKAGE = "com.eajy.materialdesigncolor";

    }
}