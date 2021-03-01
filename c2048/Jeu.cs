using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c2048
{
    public partial class Jeu : Form
    {
        private int _mouvements = 0;

        public enum Sens
        {
            Haut,
            Bas,
            Droite,
            Gauche,
            Autre
        }

        public Jeu()
        {
            InitializeComponent();
        }

        private void LabelEtat_Click(object sender, EventArgs e)
        {
            MessageEtat("Version 1.0");
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageEtat("Nouveau Jeu");
            LabelMouvements.Text = _mouvements.ToString();
        }

        private void MessageEtat(string message)
        {
            LabelEtat.Text = message;
        }

        private void Jeu_KeyDown(object sender, KeyEventArgs e)
        {
            Sens touche = Direction(e);
            MessageEtat($"Touche {touche}");
            if (touche != Sens.Autre)
            {
                _mouvements += 1;
                LabelMouvements.Text = _mouvements.ToString();
            }
        }

        private Sens Direction(KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Down: return Sens.Bas;
                case Keys.Up: return Sens.Haut;
                case Keys.Left: return Sens.Gauche;
                case Keys.Right: return Sens.Droite;
                default: return Sens.Autre;
            }
        }

        private void Jeu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = MessageBox.Show($"Fermeture de l'application pour {e.CloseReason}. Voulez-vous quitter ?", "Fermeture", MessageBoxButtons.YesNo) == DialogResult.No;
        }
    }
}
