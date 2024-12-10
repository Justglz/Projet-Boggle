using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Nuage_de_mots
{
    class GenerateurDeNuageDeMots
    {
        private Dictionary<string, int> mots;

        public GenerateurDeNuageDeMots(Dictionary<string, int> mots)
        {
            this.mots = mots;
        }

        public void GenererNuageDeMots(string cheminFichier)
        {
            int largeur = 800;
            int hauteur = 600;

            // Créer une image vide avec un fond dégradé
            using (Bitmap image = new Bitmap(largeur, hauteur))
            {
                using (Graphics graphique = Graphics.FromImage(image))
                {
                    // Créer un fond dégradé
                    using (Brush pinceauDégradé = new LinearGradientBrush(new Point(0, 0), new Point(largeur, hauteur), Color.LightBlue, Color.White))
                    {
                        graphique.FillRectangle(pinceauDégradé, 0, 0, largeur, hauteur);
                    }

                    Random aleatoire = new Random();

                    // Définir la police et la couleur des mots
                    foreach (var mot in mots)
                    {
                        string texte = mot.Key;
                        int poids = mot.Value;

                        // Taille de police basée sur le poids
                        int taillePolice = Math.Max(20, poids * 2);
                        using (Font police = new Font("Arial", taillePolice, FontStyle.Bold))
                        {
                            // Définir la couleur aléatoire pour chaque mot
                            Color couleurMot = Color.FromArgb(aleatoire.Next(256), aleatoire.Next(256), aleatoire.Next(256));
                            using (Brush pinceau = new SolidBrush(couleurMot))
                            {
                                // Mesurer la taille du mot et générer une position aléatoire
                                SizeF tailleTexte = graphique.MeasureString(texte, police);
                                int x = aleatoire.Next(0, largeur - (int)tailleTexte.Width);
                                int y = aleatoire.Next(0, hauteur - (int)tailleTexte.Height);

                                // Dessiner le mot sur l'image
                                graphique.DrawString(texte, police, pinceau, new PointF(x, y));
                            }
                        }
                    }
                }

                // Sauvegarder l'image
                image.Save(cheminFichier, ImageFormat.Png);
            }
        }
    }
}