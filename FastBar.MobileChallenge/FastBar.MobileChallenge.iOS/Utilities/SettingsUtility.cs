using FastBar.MobileChallenge.Utilities;
using Foundation;

namespace FastBar.MobileChallenge.iOS.Utilities
{
    internal class SettingsUtility : ISettingsUtility
    {
        private readonly NSUserDefaults _defaults = NSUserDefaults.StandardUserDefaults;

        public bool Exists(string key)
        {
            return _defaults.ValueForKey(new NSString(key)) != null;
        }

        public T Read<T>(string key, T otherwise = default(T))
        {
            // strategy is ignored, ios doesn't have a roaming settings option
            object returnValue = _defaults.ValueForKey(new NSString(key));

            if (returnValue == null)
            {
                return otherwise;
            }

            return (T)returnValue;
        }

        public void Remove(string key)
        {
            _defaults.RemoveObject(key);
        }

        public void Write<T>(string key, T value)
        {
            var valueObject = (object)value;
            if (value is int)
            {
                _defaults.SetInt((int)valueObject, key);
            }
            else if (value is long || value is float || value is double)
            {
                _defaults.SetFloat((long)valueObject, key);
            }
            else if (value is bool)
            {
                _defaults.SetBool((bool)valueObject, key);
            }
            else
            {
                _defaults.SetString((string)valueObject, key);
            }
        }
    }
}