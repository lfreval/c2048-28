﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace c2048
{
    static class Outils
    {
        public static Dictionary<string, Color> Couleurs(int valeur)
        {
            Color couleurFonte = Color.Black;
            Color couleurFond = valeur < 6 ? Color.Black : Color.GhostWhite;

            switch (valeur)
            {
                case 2:
                    couleurFond = Color.AntiqueWhite;
                    break;
                case 4:
                    couleurFond = Color.Gold;
                    break;
                case 128:
                case 256:
                case 512:
                    couleurFond = Color.Tomato;
                    break;
                case 1024:
                    couleurFond = Color.Crimson;
                    couleurFonte = Color.GhostWhite;
                    break;
                case 2048:
                    couleurFond = Color.Firebrick;
                    couleurFonte = Color.GhostWhite;
                    break;
            }

            var couleur = new Dictionary<string, Color>
            {
                ["fonte"] = couleurFonte,
                ["fond"] = couleurFond
            };
            return couleur;
        }
    }
}
