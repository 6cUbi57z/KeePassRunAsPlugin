using System.Windows.Forms;

namespace RunAsPlugin.Utils
{
    internal static class UIUtils
    {
        public static TControl GetControlByName<TControl>(Form parentForm, string controlName) where TControl : Control
        {
            Control[] matchedControls = parentForm.Controls.Find(controlName, true);
            if (matchedControls.Length == 0)
            {
                throw new System.Exception(string.Concat("The control '", controlName, "' could not be found."));
            }

            if (matchedControls.Length > 1)
            {
                throw new System.Exception(string.Concat("Multiple controls named '", controlName, "' were found."));
            }

            if (!(matchedControls[0] is TControl))
            {
                throw new System.Exception(string.Concat("The control named '", controlName, "' which was found is not of type '", typeof(TControl).Name, "'."));
            }

            return (TControl)matchedControls[0];
        }
    }
}
