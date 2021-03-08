using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c2048
{
    partial class Jeu
    {
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
                                      )
                                    {
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
                                    if (_case[liberte, j] == 2048 && fusion[liberte, j])
                                    {
                                        OnFusion2048(new EventArgs());
                                    }
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
                                    if (
                                        (_case[k - 1, j] == 0)
                                        || (
                                                !fusion[k - 1, j]
                                                && (_case[k - 1, j] == _case[i, j]
                                                )
                                           )
                                      )
                                    {
                                        liberte = k - 1;
                                    }
                                    k -= 1;
                                } while ((k > 0) && (_case[k, j] == 0));
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
                                    if (_case[liberte, j] == 2048 && fusion[liberte, j])
                                    {
                                        OnFusion2048(new EventArgs());
                                    }
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
                                    if (
                                        (_case[i, k + 1] == 0)
                                        || (
                                                !fusion[i, k + 1]
                                                && (_case[i, k + 1] == _case[i, j]
                                                )
                                           )
                                      )
                                    {
                                        liberte = k + 1;
                                    }
                                    k += 1;
                                } while ((k < 3) && (_case[i, k] == 0));
                                if (liberte != j)
                                {
                                    if (_case[i, liberte] == 0)
                                    {
                                        _case[i, liberte] = _case[i, j];
                                    }
                                    else
                                    {
                                        _case[i, liberte] *= 2;
                                        points += _case[i, liberte];
                                        fusion[i, liberte] = true;
                                    }
                                    _case[i, j] = 0;
                                    changement = true;
                                    if (_case[i, liberte] == 2048 && fusion[i, liberte])
                                    {
                                        OnFusion2048(new EventArgs());
                                    }
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
                                    if (
                                        (_case[i, k - 1] == 0)
                                        || (
                                                !fusion[i, k - 1]
                                                && (_case[i, k - 1] == _case[i, j]
                                                )
                                           )
                                      )
                                    {
                                        liberte = k - 1;
                                    }
                                    k -= 1;
                                } while ((k > 0) && (_case[i, k] == 0));
                                if (liberte != j)
                                {
                                    if (_case[i, liberte] == 0)
                                    {
                                        _case[i, liberte] = _case[i, j];
                                    }
                                    else
                                    {
                                        _case[i, liberte] *= 2;
                                        points += _case[i, liberte];
                                        fusion[i, liberte] = true;
                                    }
                                    _case[i, j] = 0;
                                    changement = true;
                                    if (_case[i, liberte] == 2048 && fusion[i, liberte])
                                    {
                                        OnFusion2048(new EventArgs());
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            return (changement, points);
        }

        private Sens Direction(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down: return Sens.Bas;
                case Keys.Up: return Sens.Haut;
                case Keys.Left: return Sens.Gauche;
                case Keys.Right: return Sens.Droite;
                default: return Sens.Autre;
            }
        }

        private void Blocage()
        {
            bool mouvementImpossible = true;

            // horizontalement
            // pour chaque ligne
            for (int j = 0; j < 4; j += 1)
            {
                // pour chaque colonne
                for (int i = 0; i < 3; i += 1)
                {
                    if (_case[i, j] == 0
                        || (_case[i, j] == _case[i + 1, j]))
                    {
                        mouvementImpossible = false;
                        return;
                    }
                }
            }

            // verticalement
            // pour chaque colonne
            for (int i = 0; i < 4; i += 1)
            {
                // pour chaque ligne
                for (int j = 0; j < 3; j += 1)
                {
                    if (_case[i, j] == 0
                        || (_case[i, j] == _case[i, j + 1]))
                    {
                        mouvementImpossible = false;
                        return;
                    }
                }
            }

            if (mouvementImpossible)
            {
                OnGameOver(new EventArgs());
            }
        }

    }
}
