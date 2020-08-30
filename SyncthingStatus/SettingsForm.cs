using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncthingStatus
{
    public partial class SettingsForm : Form
    {
        private readonly StatusChecker statusChecker;

        public SettingsForm(StatusChecker statusChecker)
        {
            this.statusChecker = statusChecker;
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
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

            ApiClient.Initialize();
            statusChecker.CheckNow();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBoxApiKey.Text = Properties.Settings.Default.ApiKey;
            checkBoxAddress.Checked = Properties.Settings.Default.UsingCustomStAddress;
            textBoxAddress.Text = Properties.Settings.Default.StAddress;

            labelVersion.Text = Util.GetAboutString();

            LinkLabel.Link link = new LinkLabel.Link
            {
                LinkData = "https://github.com/mc0239/SyncthingStatus"
            };
            linkLabelHomepage.Links.Add(link);
        }

        private void TextBoxApiKey_Enter(object sender, EventArgs e)
        {
            textBoxApiKey.Select(0, 0);
        }

        private void LinkLabelHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Util.OpenUrl(e.Link.LinkData.ToString());
        }

        private void CheckBoxAddress_CheckedChanged(object sender, EventArgs e)
        {
            textBoxAddress.Enabled = checkBoxAddress.Checked;
        }
    }
}
