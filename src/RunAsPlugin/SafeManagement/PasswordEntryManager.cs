using KeePass.Forms;
using KeePassLib;
using KeePassLib.Collections;
using KeePassLib.Security;
using RunAsPlugin.Models;

namespace RunAsPlugin.SafeManagement
{
    /// <summary>
    /// The manager used for interacting with the open password entry.
    /// </summary>
    public class PasswordEntryManager
    {
        /// <summary>
        /// The password database containing the entry.
        /// </summary>
        private readonly PwDatabase database;

        /// <summary>
        /// The dictionary containing the string for the entry.
        /// </summary>
        private readonly ProtectedStringDictionary entryStrings;

        /// <summary>
        /// Constructor for use with the password entry form.
        /// </summary>
        /// <remarks>
        /// This uses the strings dictionary provided by the password entry form to ensure it is only updated when the form is saved.
        /// </remarks>
        /// <param name="database">The password database containing the entry.</param>
        /// <param name="entryForm">The password entry form editing the password entry.</param>
        internal PasswordEntryManager(PwDatabase database, PwEntryForm entryForm)
        {
            this.database = database;
            this.entryStrings = entryForm.EntryStrings;
        }

        /// <summary>
        /// Constructor for general interaction with a password entry.
        /// </summary>
        /// <param name="entry">The password entry to interact with.</param>
        internal PasswordEntryManager(PwEntry entry)
        {
            this.entryStrings = entry.Strings;
        }

        /// <summary>
        /// Gets the run as settings from the password entry.
        /// </summary>
        /// <returns>The run as settings for the entry.</returns>
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

        /// <summary>
        /// Saves the run as settings to the password entry.
        /// </summary>
        /// <param name="settings">The run as settings for the entry.</param>
        internal void SetRunAsSettings(RunAsEntrySettings settings)
        {
            this.SetString(FieldNames.RunAs.IsEnabled, settings.IsEnabled);
            this.SetString(FieldNames.RunAs.Application, settings.Application);
            this.SetString(FieldNames.RunAs.NetOnly, settings.IsNetOnly);
        }

        /// <summary>
        /// Gets the name of the password entry.
        /// </summary>
        /// <returns>The name of the password entry.</returns>
        internal string GetTitle()
        {
            return this.entryStrings.ReadSafe(FieldNames.Title);
        }

        /// <summary>
        /// Gets the execution settings from the password entry.
        /// </summary>
        /// <returns>The execution settings for the entry.</returns>
        internal ExecutionSettings GetExecutionSettings()
        {
            return new ExecutionSettings()
            {
                FullUsername = this.entryStrings.ReadSafe(FieldNames.Username),
                Password = this.entryStrings.Get(FieldNames.Password),
                NetOnly = this.GetBool(FieldNames.RunAs.NetOnly)
            };
        }

        /// <summary>
        /// Sets a string value on the password entry.
        /// </summary>
        /// <param name="field">The name of the string field.</param>
        /// <param name="value">The value of the string field.</param>
        private void SetString(string field, object value)
        {
            ProtectedString protectedString = new ProtectedString(false, value.ToString());
            this.entryStrings.Set(field, protectedString);
        }

        /// <summary>
        /// Obtains a string from the password entry. Returns null if the string doesn't exist.
        /// </summary>
        /// <param name="field">The name of the string field.</param>
        /// <returns>The value of the string field.</returns>
        private string GetString(string field)
        {
            return this.entryStrings.Get(field)?.ReadString();
        }

        /// <summary>
        /// Obtains a bool from the password entry. Return null if the string doesn't exist or is not a valid bool.
        /// </summary>
        /// <param name="field">The name of the bool field.</param>
        /// <returns>The value of the bool field.</returns>
        private bool GetBool(string field)
        {
            string stringValue = this.entryStrings.Get(field)?.ReadString();
            bool.TryParse(stringValue, out bool boolValue);
            return boolValue;
        }
    }
}
