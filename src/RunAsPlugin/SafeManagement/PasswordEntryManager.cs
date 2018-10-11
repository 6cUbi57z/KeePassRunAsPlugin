using KeePass.Forms;
using KeePassLib;
using KeePassLib.Collections;
using KeePassLib.Security;
using RunAsPlugin.Models;

namespace RunAsPlugin.SafeManagement
{
    public class PasswordEntryManager
    {
        private readonly PwDatabase database;
        private readonly ProtectedStringDictionary entryStrings;

        internal PasswordEntryManager(PwDatabase database, PwEntryForm entryForm)
        {
            this.database = database;
            this.entryStrings = entryForm.EntryStrings;
        }

        internal PasswordEntryManager(PwEntry entry)
        {
            this.entryStrings = entry.Strings;
        }

        internal RunAsEntrySettings GetRunAsSettings()
        {
            bool isEnabled = this.GetBool(FieldNames.RunAs.IsEnabled);
            string application = this.GetString(FieldNames.RunAs.Application);
            bool isNetOnly = this.GetBool(FieldNames.RunAs.NetOnly);

            return new RunAsEntrySettings()
            {
                IsEnabled = isEnabled,
                Application = application,
                IsNetOnly = isNetOnly
            };
        }

        internal void SetRunAsSettings(RunAsEntrySettings settings)
        {
            this.SetString(FieldNames.RunAs.IsEnabled, settings.IsEnabled);
            this.SetString(FieldNames.RunAs.Application, settings.Application);
            this.SetString(FieldNames.RunAs.NetOnly, settings.IsNetOnly);
        }

        internal string GetTitle()
        {
            return this.entryStrings.ReadSafe(FieldNames.Title);
        }

        internal ImpersonationSettings GetImpersonationSettings()
        {
            return new ImpersonationSettings()
            {
                FullUsername = this.entryStrings.ReadSafe(FieldNames.Username),
                Password = this.entryStrings.Get(FieldNames.Password),
                NetOnly = this.GetBool(FieldNames.RunAs.NetOnly)
            };
        }

        private void SetString(string field, object value)
        {
            ProtectedString protectedString = new ProtectedString(false, value.ToString());
            this.entryStrings.Set(field, protectedString);
        }

        private string GetString(string field)
        {
            return this.entryStrings.Get(field)?.ReadString();
        }

        private bool GetBool(string field)
        {
            string stringValue = this.entryStrings.Get(field)?.ReadString();
            bool.TryParse(stringValue, out bool boolValue);
            return boolValue;
        }
    }
}
