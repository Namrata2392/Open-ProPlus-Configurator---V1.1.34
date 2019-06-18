using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace OpenProPlusConfigurator
{
    public class RegistryTools
    {
        // Save a value.
        public static void SaveSetting(string app_name, string name, object value)
        {
            RegistryKey reg_key = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey sub_key = reg_key.CreateSubKey(app_name);
            sub_key.SetValue(name, value);
        }
        // Get a value.
        public static object GetSetting(string app_name, string name, object default_value)
        {
            RegistryKey reg_key = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey sub_key = reg_key.CreateSubKey(app_name);
            return sub_key.GetValue(name, default_value);
        }
        // Load all child control settings.
        public static void LoadChildSettings(string app_name, Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                // Restore the child's value.
                switch (child.GetType().Name)
                {
                    case "TextBox":
                    case "ListBox":
                    case "ComboBox":
                        child.Text = GetSetting(app_name, child.Name, child.Text).ToString();
                        break;
                    case "CheckBox":
                        CheckBox chk = child as CheckBox;
                        chk.Checked = bool.Parse(GetSetting(app_name,
                            child.Name, chk.Checked.ToString()).ToString());
                        break;
                    case "RadioButton":
                        RadioButton rad = child as RadioButton;
                        rad.Checked = bool.Parse(GetSetting(app_name,
                            child.Name, rad.Checked.ToString()).ToString());
                        break;
                    case "VScrollBar":
                        VScrollBar vscr = child as VScrollBar;
                        vscr.Value = (int)GetSetting(app_name, child.Name, vscr.Value);
                        break;
                    case "HScrollBar":
                        HScrollBar hscr = child as HScrollBar;
                        hscr.Value = (int)GetSetting(app_name, child.Name, hscr.Value);
                        break;
                    case "NumericUpDown":
                        NumericUpDown nud = child as NumericUpDown;
                        nud.Value = decimal.Parse(GetSetting(app_name, child.Name, nud.Value).ToString());
                        break;
                        // Add other control types here.
                }

                // Recursively restore the child's children.
                LoadChildSettings(app_name, child);
            }
        }
        // Save all child control settings.
        public static void SaveChildSettings(string app_name, Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                // Save the child's value.
                switch (child.GetType().Name)
                {
                    case "TextBox":
                    case "ListBox":
                    case "ComboBox":
                        SaveSetting(app_name, child.Name, child.Text);
                        break;
                    case "CheckBox":
                        CheckBox chk = child as CheckBox;
                        SaveSetting(app_name, child.Name, chk.Checked.ToString());
                        break;
                    case "RadioButton":
                        RadioButton rad = child as RadioButton;
                        SaveSetting(app_name, child.Name, rad.Checked.ToString());
                        break;
                    case "VScrollBar":
                        VScrollBar vscr = child as VScrollBar;
                        SaveSetting(app_name, child.Name, vscr.Value);
                        break;
                    case "HScrollBar":
                        HScrollBar hscr = child as HScrollBar;
                        SaveSetting(app_name, child.Name, hscr.Value);
                        break;
                    case "NumericUpDown":
                        NumericUpDown nud = child as NumericUpDown;
                        SaveSetting(app_name, child.Name, nud.Value);
                        break;
                        // Add other control types here.
                }

                // Recursively save the child's children.
                SaveChildSettings(app_name, child);
            }
        }
        // Delete a value.
        public static void DeleteSetting(string app_name, string name)
        {
            RegistryKey reg_key = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey sub_key = reg_key.CreateSubKey(app_name);
            try
            {
                //Ajay: 28/12/2018
                //sub_key.DeleteValue(name);
                sub_key.DeleteValue(name, false);
            }
            catch
            {
            }
        }

    }
}
