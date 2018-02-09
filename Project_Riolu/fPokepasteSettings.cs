using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Riolu
{
    public partial class fPokepasteSettings : Form
    {
        String paste;
        public fPokepasteSettings(String data)
        {
            InitializeComponent();
            paste = data;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            String url = Export.exportToPokepaste(title.Text, author.Text, notes.Text, paste);
            System.Diagnostics.Process.Start(url);
            this.Close();
        }
    }
}
