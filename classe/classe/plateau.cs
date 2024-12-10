using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace classe
{
    public class Plateau
    {
        private DE[] tab;
        private string[,] superieures;
        private int taille;

        #region propriétés
        public string[,] Superieures
        {
            get { return this.superieures; }
            set { this.superieures = value; }
        }
        public DE[] Tab
        {
            get { return this.tab; }
            set { this.tab = value; }
        }
        public int Taille
        {
            get { return taille; }
            set { taille = value; }
        }
        #endregion

        /// <summary>
        /// Demande à l'utilisateur d'insérer la taille du plateau à laquelle il souhaite jouer
        /// </summary>
        public void Creertaille()
        {
            Console.WriteLine("\nDe quelle taille sera la plateau ? \n ");
            this.Taille = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");
        }

        /// <summary>
        /// Attribut à l'attribut de classe tab de l'instance courrante un tableau de dé qui va composé le plateau
        /// </summary>
        public void Creertab()
        {
            DE[] tab1 = new DE[this.Taille * this.Taille];
            Random rand = new Random();
            for (int i = 0; i < this.Taille * this.Taille; i++)
            {
                DE de = new DE();
                de.Tableaufaces();
                de.Lance(rand);
                tab1[i] = de;
            }
            this.Tab = tab1;
        }

        /// <summary>
        /// Attribut à l'attribut de classe supérieures de l'instance courrante un tableau des faces supérieures qui va composé le plateau
        /// </summary>
        public void Creersuperieures()
        {
            string[,] superieures2 = new string[this.Taille, this.Taille];
            this.Superieures = superieures2;
            int compte = 0;
            for (int i = 0; i < this.Taille; i++)
            {
                for (int j = 0; j < this.Taille; j++)
                {
                   this.Superieures[i, j] = this.Tab[compte].Visible;
                   compte++;
                }
            }
        }

        /// <summary>
        /// Affiche le plateau avec toutes les lettres des phases visibles des dés
        /// </summary>
        public void Afffichageplateau()
        {
            string S = "Le plateau :\n\n";
            for (int i = 0; i < this.Superieures.GetLength(0); i++)
            {
                for (int j = 0; j < this.Superieures.GetLength(1); j++)
                {
                    S += this.Superieures[i, j] + " "; //comprend pas
                }
                S += "\n";
            }
            Console.WriteLine(S);
        }

        /// <summary>
        /// Retourne une chaine de caractère décrivant le plateau (taille et faces visibles)
        /// </summary>
        /// <returns> chaine de caractère</returns>
        public override string ToString()
        {
            string S = "Le plateau est de taille " + this.Taille + "*" + this.Taille + " et les faces visibles sont ";
            for (int i = 0; i < this.Superieures.GetLength(0); i++)
            {
                for (int j = 0; j < this.Superieures.GetLength(1); j++)
                {
                    S += this.Superieures[i, j];
                    if ((i < this.Superieures.GetLength(0) - 1) && (j < this.Superieures.GetLength(1) - 1))
                    {
                        S += ", ";
                    }
                }
            }
            return S;
        }

        public bool Test_Plateau(string mot)
        {
            if (mot.Length > this.Taille * this.Taille) // Le mot est plus grand que le plateau
            {
                return false;
            }

            int lignes = this.Superieures.GetLength(0);
            int colonnes = this.Superieures.GetLength(1);

            for (int ligne = 0; ligne < lignes; ligne++)
            {
                for (int col = 0; col < colonnes; col++)
                {
                    if (this.Superieures[ligne, col] == mot[0].ToString()) // Vérifier la première lettre du mot
                    {
                        bool[,] casesVisitees = new bool[lignes, colonnes];
                        if (RechercheAdjacente(mot, ligne, col, casesVisitees, 0,this.Superieures)==true) // Appel à la recherche récursive
                        {
                            return true; // Si le mot est trouvé, retourner true
                        }
                    }
                }
            }
            Console.WriteLine("Le mot ne se trouve pas sur le plateau.");
            return false; // Si le mot n'est pas trouvé, retourner false
        }

        public static bool RechercheAdjacente(string mot, int ligne, int col, bool[,] casesVisitees, int indice, string[,]sup)
        {
            // Si le mot est complètement trouvé (indice atteint la longueur du mot), on retourne true
            if (indice == mot.Length)
            {
                return true;
            }

            // Si la position est hors limites ou si la case a déjà été visitée, ou si la lettre ne correspond pas
            if (ligne < 0 || ligne >= sup.GetLength(0) || col < 0 || col >= sup.GetLength(1) || casesVisitees[ligne, col] || Convert.ToChar(sup[ligne, col]) != mot[indice])
            {
                return false;
            }

            // Marque la case actuelle comme visitée
            casesVisitees[ligne, col] = true;

            // Définition des déplacements pour explorer les 8 directions adjacentes
            int[] deplacementsLignes = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] deplacementsColonnes = { -1, 0, 1, -1, 1, -1, 0, 1 };

            // On tente de rechercher dans les 8 directions adjacentes
            for (int dir = 0; dir < 8; dir++)
            {
                // Appel récursif pour explorer chaque direction adjacente
                if (RechercheAdjacente(mot, ligne + deplacementsLignes[dir], col + deplacementsColonnes[dir], casesVisitees, indice + 1, sup))
                {
                    return true; // Si le mot est trouvé dans cette direction, retourner true
                }
            }

            // Réinitialise la case comme non visitée pour les autres recherches
            casesVisitees[ligne, col] = false;

            return false; // Si aucune direction n'a permis de trouver le mot, retourner false
        }

    }
}
