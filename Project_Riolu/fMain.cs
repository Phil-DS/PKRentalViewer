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
    public partial class fMain : Form
    {
        RentalTeam rentalTeam;

        public fMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //Handle QR inputs.

        //Clipboard

        private void QRButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                displayQR.Image = Clipboard.GetImage();
                rentalTeam = QRParser.decryptQRCode(displayQR.Image);
            }
            else
            {
                MessageBox.Show("Please make sure you have a QR code copied.", "Invalid QR Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            updateForm();
        }

        //Handle exporting.

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (rentalTeam == null)
            {
                MessageBox.Show("There is no team to export. Please read a team first.", "Invalid Team", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string data = "";
            foreach (Pokemon p in rentalTeam.team)
            {
                data += p.ToShowdownFormat(false) + "\n\n";
            }

            switch (ExportDestination.Text)
            {
                case "Showdown":
                    Clipboard.SetText(data);
                    MessageBox.Show("Team data has been put on the clipboard.","Success");
                    break;
                case "Pokepaste":
                    Form temp = new fPokepasteSettings(data);
                    temp.ShowDialog();
                    break;
                case "Pokepaste (Quick)":
                    String url = Export.exportToPokepaste("New QR Paste", "QR from Pokemon Global Link", "Created using PKRental Team\nAvailable at https://github.com/Phil-DS/PKRentalViewer", data);
                    System.Diagnostics.Process.Start(url);
                    break;
                default: Clipboard.SetText(data);
                    break;
            }
        }

        /*
         * Update the form with the new information
         * 
         * TODO:
         *  - Add Pokemon pics
         * */

        private void updateForm()
        {
            if (rentalTeam == null)
            {
                return;
            }
            Pokemon[] team = rentalTeam.team.ToArray();
            //Pokemon 1
            Pokemon1Data.Text  = team[0].getStatsData();
            Pokemon1Moves.Text = team[0].getMovesString();
            Pokemon1Sprite.Image = team[0].getPokemonSprite();
            Pokemon1Name.Text = team[0].getPokemonName();
            //Pokemon 2
            Pokemon2Data.Text  = team[1].getStatsData();
            Pokemon2Moves.Text = team[1].getMovesString();
            Pokemon2Sprite.Image = team[1].getPokemonSprite();
            Pokemon2Name.Text = team[1].getPokemonName();
            //Pokemon 3
            Pokemon3Data.Text  = team[2].getStatsData();
            Pokemon3Moves.Text = team[2].getMovesString();
            Pokemon3Sprite.Image = team[2].getPokemonSprite();
            Pokemon3Name.Text = team[2].getPokemonName();
            //Pokemon 4
            Pokemon4Data.Text  = team[3].getStatsData();
            Pokemon4Moves.Text = team[3].getMovesString();
            Pokemon4Sprite.Image = team[3].getPokemonSprite();
            Pokemon4Name.Text = team[3].getPokemonName();
            //Pokemon 5
            Pokemon5Data.Text  = team[4].getStatsData();
            Pokemon5Moves.Text = team[4].getMovesString();
            Pokemon5Sprite.Image = team[4].getPokemonSprite();
            Pokemon5Name.Text = team[4].getPokemonName();
            //Pokemon 6
            Pokemon6Data.Text  = team[5].getStatsData();
            Pokemon6Moves.Text = team[5].getMovesString();
            Pokemon6Sprite.Image = team[5].getPokemonSprite();
            Pokemon6Name.Text = team[5].getPokemonName();
        }
    }
}
