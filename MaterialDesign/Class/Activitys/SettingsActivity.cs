using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace MaterialDesign.Class.Activitys
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : AppCompatPreferenceActivity,
        Android.Preferences.Preference.IOnPreferenceChangeListener,
        Android.Preferences.Preference.IOnPreferenceClickListener
    {
        public bool OnPreferenceChange(Preference preference, Java.Lang.Object newValue)
        {
            string stringValue = newValue.ToString();

            if (preference is ListPreference)
            {
                ListPreference listPreference = preference as ListPreference;
                int index = listPreference.FindIndexOfValue(stringValue);

                // Set the summary to reflect the new value.
                preference.Summary = index >= 0 ? listPreference.GetEntries()[index] : null;
            }
            else if (preference is RingtonePreference)
            {
                if (Android.Text.TextUtils.IsEmpty(stringValue))
                {
                    // Empty values correspond to 'silent' (no ringtone).
                    preference.SetSummary(Resource.String.pref_ringtone_silent);
                }
                else
                {
                    Ringtone ringtone = RingtoneManager.GetRingtone(preference.Context, Android.Net.Uri.Parse(stringValue));

                    if (ringtone == null)
                    {
                        // Clear the summary if there was a lookup error.
                        preference.Summary = null;
                    }
                    else
                    {
                        // Set the summary to reflect the new ringtone display name.
                        string name = ringtone.GetTitle(preference.Context);
                        preference.Summary = name;
                    }
                }
            }
            else
            {
                preference.Summary = stringValue;
            }

            return true;
        }
        private void BindPreferenceSummaryToValue(Preference preference)
        {
            // Set the listener to watch for value changes.
            preference.OnPreferenceChangeListener = this;

            // Trigger the listener immediately with the preference's current value.
            preference.OnPreferenceChangeListener.OnPreferenceChange(preference,
                    PreferenceManager.GetDefaultSharedPreferences(preference.Context).GetString(preference.Key, ""));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Android.Support.V7.App.ActionBar actionBar = GetSupportActionBar();
            if (actionBar != null)
            {
                actionBar.SetDisplayHomeAsUpEnabled(true);
            }

            AddPreferencesFromResource(Resource.Xml.preferences_settings);

            BindPreferenceSummaryToValue(FindPreference("example_text"));
            BindPreferenceSummaryToValue(FindPreference("notifications_new_message_ringtone"));
            BindPreferenceSummaryToValue(FindPreference("sync_frequency"));

            Preference pref = FindPreference("clear_cache");
            pref.OnPreferenceClickListener = this;
        }
        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Android.Resource.Id.Home)
            {
                if (!base.OnMenuItemSelected(featureId, item))
                {
                    Android.Support.V4.App.NavUtils.NavigateUpFromSameTask(this);
                    //startActivity(new Intent(this, SettingsActivity.class));
                    //OR USING finish();
                }
                return true;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        public bool OnPreferenceClick(Preference preference)
        {
            Toast.MakeText(this, GetString(Resource.String.pref_on_preference_click), ToastLength.Short).Show();
            return false;
        }
    }
}