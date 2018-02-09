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
    public partial class fPokepasteResponse : Form
    {
        String link;
        public fPokepasteResponse(String data)
        {
            InitializeComponent();
            link = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(link);
        }
    }
}
