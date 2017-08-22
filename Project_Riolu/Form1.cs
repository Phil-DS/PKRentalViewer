using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using MC;

namespace Project_Riolu
{
    public partial class Form1 : Form
    {
        QRParser qr;
        RentalTeam rentalTeam;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            qr = new QRParser();
        }

        //Handle QR inputs.

        //HTTP Request.

        private void button1_Click(object sender, EventArgs e)
        {
            //First, get and display the QR code
            displayQR.Image = qr.getQRData();
            
            if(displayQR.Image != null)
            {
                rentalTeam = qr.decryptQRCode(displayQR.Image);
            }
            updateForm();
        }

        //Clipboard

        private void QRButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                displayQR.Image = Clipboard.GetImage();
                rentalTeam = qr.decryptQRCode(displayQR.Image);
            }

            updateForm();
        }

        //Handle exporting.

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (rentalTeam == null) return;
            string data = "";
            foreach (Pokemon p in rentalTeam.team)
            {
                data += p.ToShowdownFormat(false) + "\n\n";
            }

            Clipboard.SetText(data);
        }

        //Handle the events of the HTTP request form boxes.

        private void CookieText_TextChanged(object sender, EventArgs e)
        {
            qr.setCookie(CookieText.Text);
        }

        private void SaveIDVal_TextChanged(object sender, EventArgs e)
        {
            qr.setsID(SaveIDVal.Text);
        }

        private void TeamIDVal_TextChanged(object sender, EventArgs e)
        {
            qr.settID(TeamIDVal.Text);
        }

        //Handle Pokemon team when there is a new team to update with.

        private void updateForm()
        {
            Pokemon[] team = rentalTeam.team.ToArray();
            //Pokemon 1
            Pokemon1Data.Text  = team[0].getStatsData();
            Pokemon1Moves.Text = team[0].getMovesString();
            //Pokemon 2
            Pokemon2Data.Text  = team[1].getStatsData();
            Pokemon2Moves.Text = team[1].getMovesString();
            //Pokemon 3
            Pokemon3Data.Text  = team[2].getStatsData();
            Pokemon3Moves.Text = team[2].getMovesString();
            //Pokemon 4
            Pokemon4Data.Text  = team[3].getStatsData();
            Pokemon4Moves.Text = team[3].getMovesString();
            //Pokemon 5
            Pokemon5Data.Text  = team[4].getStatsData();
            Pokemon5Moves.Text = team[4].getMovesString();
            //Pokemon 6
            Pokemon6Data.Text  = team[5].getStatsData();
            Pokemon6Moves.Text = team[5].getMovesString();
        }
    }
}
