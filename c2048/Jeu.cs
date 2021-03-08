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
        private int _score = 0;

        private int[,] _case = new int[4, 4];
        public enum Sens
        {
            Haut,
            Bas,
            Droite,
            Gauche,
            Autre
        }


        public event EventHandler Fusion2048;
        public event EventHandler GameOver;

        protected virtual void OnFusion2048(EventArgs e)
        {
            Fusion2048?.Invoke(this, e);
        }
        protected virtual void OnGameOver(EventArgs e)
        {
            GameOver?.Invoke(this, e);
        }

        public Jeu()
        {
            InitializeComponent();
            Fusion2048 += new System.EventHandler(Gagnant);
            GameOver += new System.EventHandler(Perdant);
        }

        private void Gagnant(object sender, EventArgs e)
        {
            Affiche();
            MessageBox.Show("Gagné !");
        }
        private void Perdant(object sender, EventArgs e)
        {
            Affiche();
            MessageBox.Show("Perdu !");
        }

        private void LabelEtat_Click(object sender, EventArgs e)
        {
            MessageEtat("Version 1.0");
        }

        private void Init()
        {
            /* for (int i = 0; i < 4; i += 1)
            {
                for (int j = 0; j < 4; j += 1)
                {
                    _case[i, j] = 0;
                }
            } */
            _case = new int[4, 4];
            _mouvements = 0;
            _score = 0;
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageEtat("Nouveau Jeu");
            Init();
            /* _case[1, 1] = 2;
            _case[2, 3] = 4;
            _case[3, 2] = 2; */
            // _case = Tests.TestP9();
            _case = Tests.Test2048();
            AjouteValeur();
            Affiche();
        }

        
        private void Affiche()
        {
            LabelMouvements.Text = _mouvements.ToString();
            LabelScore.Text = _score.ToString();
            // bool gagné = false;
            for (int i = 0; i < 4; i += 1)
            {
                for (int j = 0; j < 4; j += 1)
                {
                    Affiche(i, j);
                  /*  if (_case[i, j] == 2048)
                    {
                        gagné = true;
                    }*/
                }
            }
            // if (gagné) { MessageBox.Show("you win!"); }
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


        private void AjouteValeur()
        {
            (int x, int y, int v) = Outils.DeuxOuQuatre(_case);
            _case[x, y] = v;
        }

        private void Jeu_KeyDown(object sender, KeyEventArgs e)
        {
            Sens touche = Direction(e);
            MessageEtat($"Touche {touche}");
            (bool changement, int points) = Bouge(touche);
            if (changement)
            {
                _score += points;
                _mouvements += 1;
                AjouteValeur();
                Affiche();
                Blocage();
            }
        }


        private void Jeu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = MessageBox.Show(String.Format("Fermeture de l'application pour {0}. Voulez-vous {1} ?", e.CloseReason, "quitter"), "Fermeture", MessageBoxButtons.YesNo) == DialogResult.No;
        }
    }
}
