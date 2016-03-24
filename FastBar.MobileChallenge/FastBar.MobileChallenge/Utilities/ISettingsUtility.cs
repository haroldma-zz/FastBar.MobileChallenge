namespace FastBar.MobileChallenge.Utilities
{
    /// <summary>
    ///     Helper class to facilitate access to the application's settings
    /// </summary>
    public interface ISettingsUtility
    {
        bool Exists(string key);
        void Remove(string key);
        void Write<T>(string key, T value);
        T Read<T>(string key, T otherwise = default(T));
    }
}