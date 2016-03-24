using FastBar.MobileChallenge.Utilities;
using Foundation;
using Security;

namespace FastBar.MobileChallenge.iOS.Utilities
{
    internal class CredentialUtility : ICredentialUtility
    {
        private readonly string _appName;

        public CredentialUtility(string appName)
        {
            _appName = appName;
        }

        public AppCredential GetCredentials(string resource)
        {
            var rec = GetRecord(resource);
            return rec != null ? new AppCredential(rec.Account, rec.ValueData.ToString()) : null;
        }

        public void SaveCredentials(string resource, string username, string password)
        {
            DeleteCredentials(resource);

            var s = new SecRecord(SecKind.GenericPassword)
            {
                Label = $"{_appName} Credential ({resource})",
                Description = $"Credentials from the {_appName} app",
                Account = username,
                Service = _appName,
                ValueData = NSData.FromString(password),
                Generic = NSData.FromString(resource)
            };
            SecKeyChain.Add(s);
        }

        public void DeleteCredentials(string resource)
        {
            var record = GetRecord(resource);
            if (record != null)
            {
                SecKeyChain.Remove(record);
            }
        }

        private SecRecord GetRecord(string resource)
        {
            var rec = new SecRecord(SecKind.GenericPassword) {Generic = NSData.FromString(resource)};

            SecStatusCode res;
            return SecKeyChain.QueryAsRecord(rec, out res);
        }
    }
}