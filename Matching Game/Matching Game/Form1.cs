using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_Game
{
    public partial class Form1 : Form
    {
        //Use this Random object to choose random icons for the squares
        Random random = new Random();

        // Each of these letters is an interesting icon
        // in the Webdings font,
        // and each icon appears twice in this list
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "w", "w", "v", "v", "z", "z",
        };

        // firstClicked points to the first Label control
        // that the player clicks, but it will be null
        // if the player hasn`t clicked a label yet 
        Label firstClicked = null;

        // secondClicked point to the second Label control
        // that the player clicks
        Label seconClicked = null;

        private void AssignIconsToSquares()
        // The TableLayoutPanel has 16 labels,
        // and the icon list has 16 icons,
        // so an icon is pulled at random from the list,
        // and added to each label
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        public Form1()
        { 
            InitializeComponent();

            AssignIconsToSquares();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        { 
            // The timer is only on after two non-matching
            // icons have been show to the player,
            // so ignore any clicks if the timer is running
            if (timer1.Enabled == true)
                return;

            Label clickeLabel = sender as Label;

            // if secondClicked is not null, the player has already
            // clicked twice and the game has not yet reser --
            // gnore the click
            if (seconClicked != null)
                return;

            if (clickeLabel != null)
            {
                // if the clicklabel is black, the player clicked
                // an icon that`s already been revealed --
                // ignore the click 
                if (clickeLabel.ForeColor == Color.Black)
                    return;

                // if firstClicked is null, this is the first icon
                // in the pair that the player clicked
                // so set firstClicked to the label that the player
                // clicked, change its color to black, and return
                if (firstClicked == null)
                {
                    firstClicked = clickeLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // if the player gets this far, the timer isn`t
                // running and firstClicked isn`t null,
                // so this must be the second icon the playear clicked
                // Set its color to black
                seconClicked = clickeLabel;
                seconClicked.ForeColor = Color.Black;

                // If the player clicked two matchingg icons, keep them
                // black and reset firstClicked and seconClicked
                // so the player can click another icon

                //Check to see if the player won
                CheckForWinner();

            if (firstClicked.Text == seconClicked.Text)
                {
                    firstClicked = null;
                    seconClicked = null;
                    return;
                }

                // If the player gets this far, the player
                // clicked two different icons, so start the
                // timer (which will wait three quarters of
                // a swcond, and then hide the icons)
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            timer1.Stop();

            // Hide both icons 
            firstClicked.ForeColor = firstClicked.BackColor;
            seconClicked.ForeColor = seconClicked.BackColor;

            // Reset firstClicked and secondClicked
            // so the next time a label is
            // clicked, the program knows it`s the first click
            firstClicked = null;
            seconClicked = null;
        }

        /// <summary>
        /// Checl every icon to see if it is matched, by
        /// comparing its foreground color to its background color.
        /// if all od the icons are matched, the player win
        /// </summary>
        private void CheckForWinner()
        { 
        // Go through all of the labels in the TableLayoutPanel
        // checking each one to sww if its icon matched
        foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLable = control as Label;

                if (iconLable != null)
                {
                    if (iconLable.ForeColor == iconLable.BackColor)
                        return;
                }
            }
            // if the loop didn`t return, it didn`t find
            // ane unmatched icons
            // that means the user won. Show a massage and close the form
            MessageBox.Show("You matched all the icons!", "Congatulations");
            Close();
        }
    }
}
