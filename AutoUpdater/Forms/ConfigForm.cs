using BaseLib.Registry;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace AutoUpdater.Forms
{
    public partial class ConfigForm : Form
    {
        private static Log Log;

        public ConfigForm()
        {
            Log = new Log(nameof(ConfigForm));
            Log.Write("Starting...");

            InitializeComponent();
            FormClosed += ConfigForm_Closed;

            autoStartCheckBox.Checked = Config.Instance.AutoStart;
            startMinimizedCheckBox.Checked = Config.Instance.StartMinimized;
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(Config.Instance.UpdateInterval.ToString());
        }

        private void ConfigForm_Closed(object sender, EventArgs e)
        {
            Log.Write("Closing...");

            Log.Dispose();
        }

        private void save_Click(object sender, System.EventArgs e)
        {
            Config.Instance.AutoStart = autoStartCheckBox.Checked;
            Config.Instance.StartMinimized = startMinimizedCheckBox.Checked;
            uint oldInterval = Config.Instance.UpdateInterval;
            Config.Instance.UpdateInterval = uint.Parse(comboBox1.SelectedItem.ToString());

            Config.Instance.Save();

            if (oldInterval != Config.Instance.UpdateInterval)
                Updater.UpdateInterval();

            if (Config.Instance.AutoStart && !Registry.StartupExists(Program.AssemblyName))
            {
                Log.Write("Adding assembly to startup...");
                Registry.SetStartup(Program.AssemblyName, Assembly.GetExecutingAssembly().Location);
            }
            else if (!Config.Instance.AutoStart && Registry.StartupExists(Program.AssemblyName))
            {
                Log.Write("Removing assembly from startup...");
                Registry.RemoveStartup(Program.AssemblyName);
            }

            Close();
        }
    }
}
