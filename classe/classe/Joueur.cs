using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace classe
{
    public class Joueur
    {
        private string nom;
        private int score;
        private List<string> mots;
        public Joueur(string nom1,int score1, List<string>mots1) // Constructeur
        {
            this.nom = nom1;
            this.score = 0;
            this.mots = mots1;
        }
        #region propriétés
        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }
        public List<string> Mots
        {
            get { return this.mots; }
            set { this.mots = value; }
        }
        #endregion

        /// <summary>
        /// Verifie si le mot tapé appartient déja aux mots trouvé par le jouer pendant la partie
        /// </summary>
        /// <param name="mot">mot à vérifier</param>
        /// <returns></returns>
        public bool Contain(string mot)
        {
            bool verif = this.Mots.Contains(mot);
            return verif;
        }

        /// <summary>
        /// ajoute le mot dans la liste des mots déjà trouvé par le joeur au cours de la partie
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot(string mot)
        {
            if (Contain(mot) == false)
            {
                this.Mots.Add(mot);
            }
        }

        /// <summary>
        /// Retourne une chaine de caractère qui décrit le joueur (nom, score, mots trouvés)
        /// </summary>
        /// <returns>chaine de caractère</returns>
        public override string ToString()
        {
            string liste = "";
            foreach (string mot in Mots)
            {
                liste = liste + mot+ ", ";
            }
            if (liste != "")
            {
                liste = liste.Substring(0, liste.Length - 2);
            }
            return ("Nom : " + this.Nom + "\nScore : " + this.Score + "\nMots trouvés : " + liste+".");
        }

        /// <summary>
        /// Calcule le score obtenue par le joueur en fonction de la longueur du mot et des lettres qui les contiennent
        /// </summary>
        public void Calculscore()
        {
            if (this.Mots.Count != 0)
            {
                foreach (string indice in Mots)
                {
                    foreach (char caractere in indice)
                    {
                        this.Score = this.Score + DE.dico[Convert.ToString(caractere)][0];
                    }
                }
            }
            else
            {
                this.Score = 0;
            }
        }
    }
}