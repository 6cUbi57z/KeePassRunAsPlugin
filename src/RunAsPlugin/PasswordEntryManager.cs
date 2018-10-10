using KeePass.Forms;
using KeePassLib;
using KeePassLib.Security;
using RunAsPlugin.Models;

namespace RunAsPlugin
{
    public class PasswordEntryManager
    {
        private readonly PwDatabase database;
        private readonly PwEntryForm entryForm;

        internal PasswordEntryManager(PwDatabase database, PwEntryForm entryForm)
        {
            this.database = database;
            this.entryForm = entryForm;
        }

        internal RunAsEntrySettings GetRunAsSettings()
        {
            bool isEnabled = this.GetBool(FieldNames.IsEnabled);
            string application = this.GetString(FieldNames.Application);
            bool isNetOnly = this.GetBool(FieldNames.NetOnly);

            return new RunAsEntrySettings()
            {
                IsEnabled = isEnabled,
                Application = application,
                IsNetOnly = isNetOnly
            };
        }

        internal void SetRunAsSettings(RunAsEntrySettings settings)
        {
            this.SetString(FieldNames.IsEnabled, settings.IsEnabled);
            this.SetString(FieldNames.Application, settings.Application);
            this.SetString(FieldNames.NetOnly, settings.IsNetOnly);
        }

        private void SetString(string field, object value)
        {
            ProtectedString protectedString = new ProtectedString(false, value.ToString());
            this.entryForm.EntryStrings.Set(field, protectedString);
        }

        private string GetString(string field)
        {
            return this.entryForm.EntryStrings.Get(field)?.ReadString();
        }

        private bool GetBool(string field)
        {
            string stringValue = this.entryForm.EntryStrings.Get(field)?.ReadString();
            bool.TryParse(stringValue, out bool boolValue);
            return boolValue;
        }
    }
}
