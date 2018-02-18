using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : AppCompatActivity, View.IOnClickListener
    {
        private static readonly String[] DUMMY_CREDENTIALS = new String[] { "foo@example.com:hello", "bar@example.com:world" };

        private UserLoginTask mAuthTask = null;

        private AutoCompleteTextView mUserNameView;
        private TextInputLayout input_user_name, input_password;
        private EditText mPasswordView;
        private View mProgressView;
        private View mLoginFormView;
        private Button login_button;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);

            InitView();
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.btn_login:
                    AttemptLogin();
                    break;

                case Resource.Id.btn_forgot_password:
                    Snackbar.Make(v, GetString(Resource.String.snackbar_forgot_password), Snackbar.LengthLong)
                            .SetAction("^_^", (view) => { }).Show();
                    break;

                case Resource.Id.btn_forgot_register:
                    Snackbar.Make(v, GetString(Resource.String.snackbar_register), Snackbar.LengthLong)
                            .SetAction("^_^", (view) => { }).Show();
                    break;
            }
        }

        private void InitView()
        {
            mLoginFormView = FindViewById<View>(Resource.Id.form_login);
            mProgressView = FindViewById<View>(Resource.Id.progress_login);
            mUserNameView = FindViewById<AutoCompleteTextView>(Resource.Id.tv_user_name);
            mPasswordView = FindViewById<EditText>(Resource.Id.tv_password);
            input_user_name = FindViewById<TextInputLayout>(Resource.Id.input_user_name);
            input_password = FindViewById<TextInputLayout>(Resource.Id.input_password);

            mPasswordView.SetOnEditorActionListener(new OnEditorActionListener(this));

            login_button = FindViewById<Button>(Resource.Id.btn_login);
            login_button.SetOnClickListener(this);
            Button forgot_password = FindViewById<Button>(Resource.Id.btn_forgot_password);
            forgot_password.SetOnClickListener(this);
            Button register = FindViewById<Button>(Resource.Id.btn_forgot_register);
            register.SetOnClickListener(this);
        }
        private void AttemptLogin()
        {
            if (mAuthTask != null)
            {
                return;
            }
            input_user_name.Error = null;
            input_password.Error = null;

            String userName = mUserNameView.Text;
            String password = mPasswordView.Text;

            bool cancel = false;
            View focusView = null;

            if (Android.Text.TextUtils.IsEmpty(userName))
            {
                input_user_name.Error = GetString(Resource.String.error_no_name);
                focusView = mUserNameView;
                cancel = true;
            }
            else if (!IsPhoneValid(userName) && !IsEmailValid(userName))
            {
                input_user_name.Error = GetString(Resource.String.error_invalid_name);
                focusView = mUserNameView;
                cancel = true;
            }
            else if (!Android.Text.TextUtils.IsEmpty(password) && !IsPasswordValid(password))
            {
                input_password.Error = GetString(Resource.String.error_invalid_password);
                focusView = mPasswordView;
                cancel = true;
            }
            else if ((IsPhoneValid(userName) || IsEmailValid(userName)) && Android.Text.TextUtils.IsEmpty(password))
            {
                input_password.Error = GetString(Resource.String.error_no_password);
                focusView = mPasswordView;
                cancel = true;
            }
            if (cancel)
            {
                focusView.RequestFocus();
            }
            else
            {
                HideInput(login_button);
                ShowProgress(true);
                mAuthTask = new UserLoginTask(this, userName, password);
                mAuthTask.Execute();
            }

        }
        private bool IsPhoneValid(string userName)
        {
            var reg = new System.Text.RegularExpressions.Regex("[0-9]*");
            return reg.IsMatch(userName) && userName.Length >= 7 && userName.Length <= 12;
        }
        private bool IsEmailValid(string userName)
        {
            return userName.Contains("@");
        }
        private bool IsPasswordValid(string password)
        {
            return password.Length >= 4 && password.Length <= 20;
        }
        private void HideInput(View view)
        {
            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            if (inputMethodManager != null)
            {
                inputMethodManager.HideSoftInputFromWindow(view.WindowToken, 0);
            }
        }
        private void ShowProgress(bool show)
        {
            int shortAnimTime = Resources.GetInteger(Android.Resource.Integer.ConfigShortAnimTime);
            mLoginFormView.Visibility = (show ? ViewStates.Gone : ViewStates.Visible);
            mLoginFormView.Animate().SetDuration(shortAnimTime).Alpha(show ? 0 : 1).SetListener(new AnimatorListener(this, show));
            mLoginFormView.Visibility = show ? ViewStates.Visible : ViewStates.Gone;
            mProgressView.Animate().SetDuration(shortAnimTime).Alpha(show ? 1 : 0).SetListener(new AnimatorListener1(this, show));
        }


        private class UserLoginTask : AsyncTask<object, object, bool>
        {
            private readonly String mPhone;
            private readonly String mPassword;
            private readonly LoginActivity currentActivity;

            public UserLoginTask(LoginActivity activity, String userName, String password)
            {
                currentActivity = activity;
                mPhone = userName;
                mPassword = password;
            }

            protected override bool RunInBackground(params object[] @params)
            {
                System.Threading.Tasks.Task.Delay(2000);
                //DUMMY_CREDENTIALS

                foreach (var item in DUMMY_CREDENTIALS)
                {
                    var spResult = item.Split(':');
                    if (spResult[0].Equals(mPhone))
                    {
                        return spResult[1].Equals(mPassword);
                    }
                }
                return true;
            }
            protected override void OnPostExecute(bool result)
            {
                currentActivity.mAuthTask = null;
                currentActivity.ShowProgress(false);

                if (result)
                {
                    currentActivity.Finish();
                }
                else
                {
                    currentActivity.input_password.Error = currentActivity.GetString(Resource.String.error_incorrect_password);
                    currentActivity.mPasswordView.RequestFocus();
                }
            }
            protected override void OnCancelled()
            {
                currentActivity.mAuthTask = null;
                currentActivity.ShowProgress(false);
            }
        }
        class AnimatorListener : Java.Lang.Object, Android.Animation.Animator.IAnimatorListener
        {
            private readonly LoginActivity currentActivity;
            private readonly bool Show;
            public AnimatorListener(LoginActivity activity, bool show)
            {
                this.currentActivity = activity;
                Show = show;
            }
            public void OnAnimationCancel(Animator animation)
            {
            }

            public void OnAnimationEnd(Animator animation)
            {
                currentActivity.mLoginFormView.Visibility = Show ? ViewStates.Gone : ViewStates.Visible;
            }

            public void OnAnimationRepeat(Animator animation)
            {
            }

            public void OnAnimationStart(Animator animation)
            {
            }
        }
        class AnimatorListener1 : Java.Lang.Object, Android.Animation.Animator.IAnimatorListener
        {
            private readonly LoginActivity currentActivity;
            private readonly bool Show;
            public AnimatorListener1(LoginActivity activity, bool show)
            {
                this.currentActivity = activity;
                Show = show;
            }
            public void OnAnimationCancel(Animator animation)
            {
            }

            public void OnAnimationEnd(Animator animation)
            {
                currentActivity.mLoginFormView.Visibility = Show ? ViewStates.Visible : ViewStates.Gone;
            }

            public void OnAnimationRepeat(Animator animation)
            {
            }

            public void OnAnimationStart(Animator animation)
            {
            }
        }
        class OnEditorActionListener : Java.Lang.Object, TextView.IOnEditorActionListener
        {
            private readonly LoginActivity currentActivity;
            private readonly bool Show;
            public OnEditorActionListener(LoginActivity activity)
            {
                this.currentActivity = activity;
            }
            public bool OnEditorAction(TextView v, [GeneratedEnum] ImeAction actionId, KeyEvent e)
            {
                if ((int)actionId == Resource.Id.login || actionId == ImeAction.ImeNull)
                {
                    currentActivity.AttemptLogin();
                    return true;
                }
                return false;
            }
        }
    }
}