using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoUpdater.Forms
{
    public partial class MainForm : Form
    {
        private static Log Log;

        public MainForm()
        {
            Log = new Log(nameof(MainForm));
            Log.Write("Starting...");

            InitializeComponent();
            Resize += MainForm_Resize;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            FormClosed += MainForm_Closed;

            if (Config.Instance.StartMinimized)
                notifyIcon1.Visible = true;

            var result = Updater.ReadTempFile();
            if (result != null)
            {
                switch (result.Item1)
                {
                    case UpdateResult.AlreadyUpdated:
                        status.Text = "Up to date";
                        status.ForeColor = Color.Green;
                        break;

                    case UpdateResult.Failure:
                        status.Text = "Failure";
                        status.ForeColor = Color.Red;
                        break;

                    case UpdateResult.Updated:
                        status.Text = "Successful";
                        status.ForeColor = Color.Green;
                        lastUpdatedLabel.Text = $"Last Updated: {result.Item2.ToString()}";
                        break;
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            Log.Write("Closing...");

            Log.Dispose();

            Application.Exit();
        }

        private void config_Click(object sender, EventArgs e)
        {
            using (var config = new ConfigForm())
            {
                config.ShowDialog(this);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            Log.Write("Start update.");

            var result = Updater.Update();
            Updater.WriteTempFile(result, DateTime.Now);

            switch (result)
            {
                case UpdateResult.AlreadyUpdated:
                    status.Text = "Up to date";
                    status.ForeColor = Color.Green;
                    break;

                case UpdateResult.Failure:
                    status.Text = "Failure";
                    status.ForeColor = Color.Red;
                    break;

                case UpdateResult.Updated:
                    status.Text = "Successful";
                    status.ForeColor = Color.Green;
                    lastUpdatedLabel.Text = $"Last Updated: {DateTime.Now.ToString()}";
                    break;
            }
        }
    }
}
