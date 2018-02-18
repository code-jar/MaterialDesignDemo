using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;

namespace MaterialDesign.Class.fragment
{
    public class DialogsFragment : Android.Support.V4.App.Fragment,
        View.IOnClickListener,
        DatePickerDialog.IOnDateSetListener,
        TimePickerDialog.IOnTimeSetListener,
        PopupMenu.IOnMenuItemClickListener
    {
        private Button btn_dialog_1, btn_dialog_2, btn_dialog_3, btn_dialog_4, btn_dialog_5,
            btn_dialog_6, btn_dialog_7, btn_dialog_8, btn_dialog_9, btn_dialog_10, btn_dialog_11;
        static Calendar calendar;
        DateTime currentDate;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            NestedScrollView nestedScrollView = (NestedScrollView)inflater.Inflate(Resource.Layout.fragment_dialogs, container, false);

            btn_dialog_1 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_1);
            btn_dialog_2 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_2);
            btn_dialog_3 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_3);
            btn_dialog_4 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_4);
            btn_dialog_5 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_5);
            btn_dialog_6 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_6);
            btn_dialog_7 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_7);
            btn_dialog_8 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_8);
            btn_dialog_9 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_9);
            btn_dialog_10 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_10);
            btn_dialog_11 = nestedScrollView.FindViewById<Button>(Resource.Id.btn_dialog_11);

            return nestedScrollView;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            calendar = new ChineseLunisolarCalendar();
            currentDate = DateTime.Now;

            btn_dialog_1.SetOnClickListener(this);
            btn_dialog_2.SetOnClickListener(this);
            btn_dialog_3.SetOnClickListener(this);
            btn_dialog_4.SetOnClickListener(this);
            btn_dialog_5.SetOnClickListener(this);
            btn_dialog_6.SetOnClickListener(this);
            btn_dialog_7.SetOnClickListener(this);
            btn_dialog_8.SetOnClickListener(this);
            btn_dialog_9.SetOnClickListener(this);
            btn_dialog_10.SetOnClickListener(this);
            btn_dialog_11.SetOnClickListener(this);
        }

        public void OnClick(View view)
        {
            switch (view.Id)
            {
                case Resource.Id.btn_dialog_1:
                    #region 简单对话框
                    new AlertDialog.Builder(view.Context)
                            .SetMessage(view.Context.GetString(Resource.String.main_dialog_simple_title))
                            .SetPositiveButton(view.Context.GetString(Resource.String.dialog_ok), (sender, args) => { })
                            .Show();
                    #endregion
                    break;
                case Resource.Id.btn_dialog_2:
                    #region 简单对话框
                    new AlertDialog.Builder(view.Context)
                            .SetTitle(view.Context.GetString(Resource.String.main_dialog_simple_title))
                            .SetMessage(view.Context.GetString(Resource.String.main_dialog_simple_message))
                            .SetPositiveButton(view.Context.GetString(Resource.String.dialog_ok), (sender, args) => { })
                            .SetNegativeButton(view.Context.GetString(Resource.String.dialog_cancel), (sender, args) => { })
                            .SetNeutralButton(view.Context.GetString(Resource.String.dialog_neutral), (sender, args) => { })
                            .Show();
                    #endregion
                    break;
                case Resource.Id.btn_dialog_3:
                    #region 单选对话框
                    String[] singleChoiceItems = Resources.GetStringArray(Resource.Array.dialog_choice_array);
                    int itemSelected = 0;
                    new AlertDialog.Builder(view.Context)
                            .SetTitle(view.Context.GetString(Resource.String.main_dialog_single_choice))
                            .SetSingleChoiceItems(singleChoiceItems, itemSelected, new DialogClickListener())
                            .SetNegativeButton(view.Context.GetString(Resource.String.dialog_cancel), (sender, args) => { })
                            .Show();
                    #endregion
                    break;
                case Resource.Id.btn_dialog_4:
                    #region 多选对话框
                    String[] multiChoiceItems = Resources.GetStringArray(Resource.Array.dialog_choice_array);
                    bool[] checkedItems = { true, false, false, false, false };
                    new AlertDialog.Builder(view.Context)
                            .SetTitle(view.Context.GetString(Resource.String.main_dialog_multi_choice))
                            .SetMultiChoiceItems(multiChoiceItems, checkedItems, (sender, args) => { })
                            .SetPositiveButton(view.Context.GetString(Resource.String.dialog_ok), (sender, args) => { })
                            .SetNegativeButton(view.Context.GetString(Resource.String.dialog_cancel), (sender, args) => { })
                            .Show();
                    #endregion
                    break;
                case Resource.Id.btn_dialog_5:
                    #region 进度条对话框
                    ProgressDialog progressDialog = new ProgressDialog(view.Context);
                    progressDialog.SetMessage(view.Context.GetString(Resource.String.main_dialog_progress_title));
                    progressDialog.Show();
                    #endregion
                    break;
                case Resource.Id.btn_dialog_6:
                    #region 进度条对话框
                    ProgressDialog horizontalProgressDialog = new ProgressDialog(view.Context);
                    horizontalProgressDialog.SetProgressStyle(ProgressDialogStyle.Horizontal);
                    horizontalProgressDialog.SetMessage(view.Context.GetString(Resource.String.main_dialog_progress_title));
                    horizontalProgressDialog.SetCancelable(false);
                    horizontalProgressDialog.Max = 100;
                    horizontalProgressDialog.Show();

                    Task.Run(() =>
                    {
                        new Java.Lang.Runnable(() =>
                        {

                            int progress = 0;
                            while (progress <= 100)
                            {
                                horizontalProgressDialog.Progress = progress;
                                if (progress == 100)
                                {
                                    horizontalProgressDialog.Dismiss();
                                }
                                try
                                {
                                    Task.Delay(35);
                                }
                                catch (Exception e)
                                {
                                    throw e;
                                }
                                progress++;
                            }

                        }).Run();
                    });
                    #endregion
                    break;
                case Resource.Id.btn_dialog_7:
                    #region 日期对话框
                    DatePickerDialog datePickerDialog = new DatePickerDialog(view.Context,
                        this, currentDate.Year, currentDate.Month - 1, currentDate.Day);

                    datePickerDialog.Show();
                    #endregion
                    break;
                case Resource.Id.btn_dialog_8:
                    #region 时间对话框
                    TimePickerDialog timePickerDialog = new TimePickerDialog(view.Context, this,
                        currentDate.Hour, currentDate.Minute, true);
                    timePickerDialog.Show();
                    #endregion
                    break;
                case Resource.Id.btn_dialog_9:
                    #region 底部对话框
                    BottomSheetDialog mBottomSheetDialog = new BottomSheetDialog(view.Context);
                    View dialogView = Activity.LayoutInflater.Inflate(Resource.Layout.dialog_bottom_sheet, null);
                    Button btn_dialog_bottom_sheet_ok = dialogView.FindViewById<Button>(Resource.Id.btn_dialog_bottom_sheet_ok);
                    Button btn_dialog_bottom_sheet_cancel = dialogView.FindViewById<Button>(Resource.Id.btn_dialog_bottom_sheet_cancel);
                    ImageView img_bottom_dialog = dialogView.FindViewById<ImageView>(Resource.Id.img_bottom_dialog);

                    Glide.With(this).Load(Resource.Drawable.bottom_dialog).Into(img_bottom_dialog);
                    mBottomSheetDialog.SetContentView(dialogView);

                    btn_dialog_bottom_sheet_ok.SetOnClickListener(new DialogOnClick(mBottomSheetDialog));

                    btn_dialog_bottom_sheet_cancel.SetOnClickListener(new DialogOnClick(mBottomSheetDialog));

                    mBottomSheetDialog.Show();
                    #endregion
                    break;
                case Resource.Id.btn_dialog_10:
                    #region 全屏对话框
                    Dialog fullscreenDialog = new Dialog(view.Context, Resource.Style.DialogFullscreen);
                    fullscreenDialog.SetContentView(Resource.Layout.dialog_fullscreen);
                    ImageView img_full_screen_dialog = fullscreenDialog.FindViewById<ImageView>(Resource.Id.img_full_screen_dialog);
                    Glide.With(this).Load(Resource.Drawable.google_assistant).Into(img_full_screen_dialog);
                    ImageView img_dialog_fullscreen_close = fullscreenDialog.FindViewById<ImageView>(Resource.Id.img_dialog_fullscreen_close);
                    img_dialog_fullscreen_close.SetOnClickListener(new DialogOnClick(fullscreenDialog));
                    fullscreenDialog.Show();
                    #endregion
                    break;
                case Resource.Id.btn_dialog_11:
                    #region POPUP MENU
                    PopupMenu popupMenu = new PopupMenu(view.Context, btn_dialog_11);
                    popupMenu.MenuInflater.Inflate(Resource.Menu.popup_menu_main, popupMenu.Menu);
                    popupMenu.SetOnMenuItemClickListener(this);
                    popupMenu.Show();
                    #endregion
                    break;
            }
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            currentDate = new DateTime(year, month + 1, dayOfMonth);

            btn_dialog_7.SetText(currentDate.ToShortDateString(), TextView.BufferType.Normal);
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, hourOfDay, minute, currentDate.Second);

            btn_dialog_8.SetText(currentDate.ToShortTimeString(), TextView.BufferType.Normal);
        }

        public bool OnMenuItemClick(IMenuItem item)
        {
            return false;
        }

        class DialogClickListener : Java.Lang.Object, IDialogInterfaceOnClickListener
        {
            public void OnClick(IDialogInterface dialog, int which)
            {
                dialog.Dismiss();
            }
        }
        class DialogOnClick : Java.Lang.Object, View.IOnClickListener
        {
            private Dialog dialog;
            public DialogOnClick(Dialog dialog)
            {
                this.dialog = dialog;
            }
            public void OnClick(View v)
            {
                dialog.Dismiss();
            }
        }
    }
}