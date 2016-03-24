using Android.Content;
using FastBar.MobileChallenge.Utilities;
using Newtonsoft.Json;

namespace FastBar.MobileChallenge.Droid.Utilities
{
    public class SettingsUtility : ISettingsUtility
    {
        public SettingsUtility(ISharedPreferences preferences)
        {
            LocalSettings = preferences;
        }

        public ISharedPreferences LocalSettings { get; set; }

        public bool Exists(string key)
        {
            return LocalSettings.Contains(key);
        }

        public T Read<T>(string key, T otherwise)
        {
            var type = typeof (T);
            var defaultObject = (object)otherwise;
            object returnValue;

            if (type == typeof (int))
            {
                returnValue = LocalSettings.GetInt(key, (int)defaultObject);
            }
            else if (type == typeof (long))
            {
                returnValue = LocalSettings.GetLong(key, (long)defaultObject);
            }
            else if (type == typeof (float))
            {
                returnValue = LocalSettings.GetFloat(key, (float)defaultObject);
            }
            else if (type == typeof (bool))
            {
                returnValue = LocalSettings.GetBoolean(key, (bool)defaultObject);
            }
            else if (type == typeof (string))
            {
                returnValue = LocalSettings.GetString(key, (string)defaultObject);
            }
            else
            {
                var json = LocalSettings.GetString(key, null);
                returnValue = JsonConvert.DeserializeObject<T>(json);
            }

            return (T)returnValue;
        }

        public void Remove(string key)
        {
            LocalSettings.Edit().Remove(key);
        }

        public void Write<T>(string key, T value)
        {
            var type = value.GetType();
            var editor = LocalSettings.Edit();
            var valueObj = (object)value;

            if (type == typeof (int))
            {
                editor.PutInt(key, (int)valueObj);
            }
            else if (type == typeof (long))
            {
                editor.PutLong(key, (long)valueObj);
            }
            else if (type == typeof (float))
            {
                editor.PutFloat(key, (float)valueObj);
            }
            else if (type == typeof (bool))
            {
                editor.PutBoolean(key, (bool)valueObj);
            }
            else if (type == typeof (string))
            {
                editor.PutString(key, (string)valueObj);
            }
            else
            {
                var json = JsonConvert.SerializeObject(value);
                editor.PutString(key, json);
            }

            editor.Commit();
        }
    }
}