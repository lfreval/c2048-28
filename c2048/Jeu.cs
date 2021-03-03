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

        private int[,] _case = new int[4, 4];

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
            _case[1, 1] = 2;
            _case[2, 3] = 4;
            _case[3, 2] = 2;
            Affiche();
        }

        private void Affiche()
        {
            for (int i = 0; i < 4; i += 1)
            {
                for (int j = 0; j < 4; j += 1)
                { 
                    Affiche(i, j);
                }
            }
        }

        private void AfficheTest()
        {
            double puissance = 0;
            for (int i = 0; i < 4; i += 1)
            {
                for (int j = 0; j < 4; j += 1)
                {
                    _case[i, j] = (int) Math.Pow(2, puissance += 1);
                    Affiche(i, j);
                }
            }
        }

        private void Affiche(int x, int y)
        {
            var ctrl = Grille.Controls.Find($"Case{x}{y}", true)[0];
            ctrl.Text = _case[x, y].ToString();
            var coul = Outils.Couleurs(_case[x, y]);
            ctrl.BackColor = coul["fond"];
            ctrl.ForeColor = coul["fonte"];
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
            e.Cancel = MessageBox.Show(String.Format("Fermeture de l'application pour {0}. Voulez-vous {1} ?", e.CloseReason, "quitter"), "Fermeture", MessageBoxButtons.YesNo) == DialogResult.No;
        }
    }
}
