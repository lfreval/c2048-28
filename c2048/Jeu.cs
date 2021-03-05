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

        public Jeu()
        {
            InitializeComponent();
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
            _case = Tests.TestP9();
            Affiche();
        }

        
        private void Affiche()
        {
            LabelMouvements.Text = _mouvements.ToString();
            LabelScore.Text = _score.ToString();
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

        private (bool, int) Bouge(Sens direction)
        {
            bool changement = false;
            bool[,] fusion = new bool[4, 4];
            int points = 0;

            switch (direction)
            {
                case Sens.Droite:
                    // pour chaque ligne
                    for (int j = 0; j < 4; j += 1)
                    {
                        // pour chaque colonne
                        for (int i = 2; i >= 0; i -= 1)
                        {
                            if (_case[i, j] != 0)
                            {
                                // avancer tant que c'est possible
                                int k = i;
                                int liberte = i;
                                do
                                {
                                    if (
                                        (_case[k + 1, j] == 0)
                                        || (
                                                !fusion[k + 1, j] 
                                                && (_case[k + 1, j] == _case[i, j]
                                                )
                                           )
                                      ) { 
                                        liberte = k + 1; 
                                    }
                                    k += 1;
                                } while ((k < 3) && (_case[k, j] == 0));
                                if (liberte != i)
                                {
                                    if (_case[liberte, j] == 0)
                                    {
                                        _case[liberte, j] = _case[i, j];
                                    }
                                    else
                                    {
                                        _case[liberte, j] *= 2;
                                        points += _case[liberte, j];
                                        fusion[liberte, j] = true;
                                    }
                                    _case[i, j] = 0;
                                    changement = true;
                                }
                            }
                        }
                    }
                    break;
                case Sens.Gauche:
                    // pour chaque ligne
                    for (int j = 0; j < 4; j += 1)
                    {
                        // pour chaque colonne
                        for (int i = 1; i < 4; i += 1)
                        {
                            if (_case[i, j] != 0)
                            {
                                // avancer tant que c'est possible
                                int k = i;
                                int liberte = i;
                                do
                                {
                                    if (_case[k - 1, j] == 0) { liberte = k - 1; }
                                    k -= 1;
                                } while ((k > 0) && (_case[k, j] == 0));
                                if (liberte != i)
                                {
                                    _case[liberte, j] = _case[i, j];
                                    _case[i, j] = 0;
                                    changement = true;
                                }
                            }
                        }
                    }
                    break;
                case Sens.Bas:
                    // pour chaque colonne
                    for (int i = 0; i < 4; i += 1)
                    {
                        // pour chaque ligne
                        for (int j = 2; j >= 0; j -= 1)
                        {
                            if (_case[i, j] != 0)
                            {
                                // avancer tant que c'est possible
                                int k = j;
                                int liberte = j;
                                do
                                {
                                    if (_case[i, k + 1] == 0) { liberte = k + 1; }
                                    k += 1;
                                } while ((k < 3) && (_case[i, k] == 0));
                                if (liberte != j)
                                {
                                    _case[i, liberte] = _case[i, j];
                                    _case[i, j] = 0;
                                    changement = true;
                                }
                            }
                        }
                    }
                    break;
                case Sens.Haut:
                    // pour chaque colonne
                    for (int i = 0; i < 4; i += 1)
                    {
                        // pour chaque ligne
                        for (int j = 1; j < 4; j += 1)
                        {
                            if (_case[i, j] != 0)
                            {
                                // avancer tant que c'est possible
                                int k = j;
                                int liberte = j;
                                do
                                {
                                    if (_case[i, k - 1] == 0) { liberte = k - 1; }
                                    k -= 1;
                                } while ((k > 0) && (_case[i, k] == 0));
                                if (liberte != j)
                                {
                                    _case[i, liberte] = _case[i, j];
                                    _case[i, j] = 0;
                                    changement = true;
                                }
                            }
                        }
                    }
                    break;
            }

            return (changement, points);
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
                Affiche();
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
