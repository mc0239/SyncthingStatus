using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncthingStatus
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ApiKey = textBoxApiKey.Text;
            Properties.Settings.Default.UsingCustomStAddress = checkBoxAddress.Checked;
            Properties.Settings.Default.StAddress = textBoxAddress.Text;
            Properties.Settings.Default.Save();
            buttonSave.Text = "Settings saved!";
            Task.Delay(2000).ContinueWith(t => {
                if (!this.IsDisposed && InvokeRequired)
                {
                    Invoke(new Action(() => {
                        buttonSave.Text = "Save settings";
                    }));
                }
            });
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBoxApiKey.Text = Properties.Settings.Default.ApiKey;
            checkBoxAddress.Checked = Properties.Settings.Default.UsingCustomStAddress;
            textBoxAddress.Text = Properties.Settings.Default.StAddress;

            labelVersion.Text = Util.GetAboutString();

            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://github.com/mc0239/SyncthingStatus";
            linkLabelHomepage.Links.Add(link);
        }

        private void textBoxApiKey_Enter(object sender, EventArgs e)
        {
            textBoxApiKey.Select(0, 0);
        }

        private void linkLabelHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Util.OpenUrl(e.Link.LinkData.ToString());
        }

        private void checkBoxAddress_CheckedChanged(object sender, EventArgs e)
        {
            textBoxAddress.Enabled = checkBoxAddress.Checked;
        }
    }
}
