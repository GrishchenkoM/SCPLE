using System;
using System.Windows.Forms;

namespace Scple
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void OK_btn_Click(object sender, EventArgs e)
        {
            //this.Close();
            this.DialogResult = DialogResult.OK;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Links[linkLabel1.Links.IndexOf(e.Link)].Visited = true;            
            Clipboard.SetText("anakonda3247gm@rambler.ru");
            MessageBox.Show("Адрес помещен в буфер обмена");
        }
    }
}
