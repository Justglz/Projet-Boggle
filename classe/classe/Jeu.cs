using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Boogle;
using System.Runtime.InteropServices.WindowsRuntime;

namespace classe
{
    public class Jeu
    {
        private Joueur[] joueurs;
        private Plateau plateau;
        private Dictionnaire dico;
        private int nbjoueurs;
        private Dictionary<Joueur, int> tableauscore;

        #region propriétés
        public Joueur[] Joueurs
        {
            get { return this.joueurs; }
            set { this.joueurs = value; }
        }
        public Dictionnaire Dico
        {
            get { return this.dico; }
            set { this.dico = value; }
        }
        public Plateau Plateau
        {
            get { return this.plateau; }
            set { this.plateau = value; }
        }
        public int Nbjoueurs
        {
            get { return this.nbjoueurs; }
            set { this.nbjoueurs = value; }
        }
        public Dictionary<Joueur, int> Tableauscore
        {
            get { return this.tableauscore; }
            set { this.tableauscore = value; }
        }
        #endregion

        /// <summary>
        /// Demande le nom des joueurs
        /// </summary>
        public void CreerJoueursfr()
        {

            Console.WriteLine("Entrez le nombre de joueurs : \n");
            int Nbjoueurs1 = Convert.ToInt32(Console.ReadLine());
            this.Nbjoueurs = Nbjoueurs1;
            Joueur[] joueurs1 = new Joueur[this.Nbjoueurs];
            for (int i = 0; i < this.Nbjoueurs; i++)
            {
                Console.WriteLine("\nEntrez le nom du joueur " + (i + 1) + " : \n");
                List<string> mot1 = new List<string>();
                joueurs1[i] = new Joueur(Console.ReadLine(), 0, mot1);
            }
            this.Joueurs = joueurs1;
        }

        /// <summary>
        /// Créer le plateau en demandant au préalable la taille du plateau
        /// </summary>
        public Jeu(Plateau plateau1, Dictionnaire dico1, Dictionary<Joueur, int> tableauscore1)
        {
            this.Plateau = plateau1;
            this.Dico = dico1;
            this.Tableauscore = tableauscore1;
        }
        public void CreerPlateau()
        {
            DE.Creerlistelettres();
            this.Plateau.Creertaille();
            this.Plateau.Creertab();
            this.Plateau.Creersuperieures();
        }
        public void CreerDico()
        {
            this.Dico.Demander_Langue();
            this.Dico.Readfile();
        }
        public void CreerTableauscore()
        {
            for (int i = 0; i < this.Nbjoueurs; i++)
            {
                this.tableauscore[this.Joueurs[i]] = this.Joueurs[i].Score;
            }
        }
        public List<Joueur> Gagnant(Dictionary<Joueur, int> tableauscore)
        {
            // Initialiser une liste pour les joueurs gagnants
            List<Joueur> gagnants = new List<Joueur>();

            // Trouver le score maximum
            int maxScore = tableauscore.Values.Max();

            // Parcourir le dictionnaire pour trouver les joueurs ayant le score maximum
            foreach (var entry in tableauscore)
            {
                if (entry.Value == maxScore)
                {
                    gagnants.Add(entry.Key);
                }
            }

            // Retourner la liste des gagnants
            return gagnants;
        }
        /// <summary>
        /// Vérifie si le mot tapé par le joueur existe dans la langue et s'il la disposition des dés permet de former ce mot
        /// </summary>
        /// <param name="mot">mot tapé par l'utilisateur</param>
        /// <returns>si le mot peut être comptabilisé</returns>
        public bool Verif(string mot)
        {
            if (this.Dico == null || this.Plateau == null)
            {
                Console.WriteLine("Erreur : Dico ou Plateau non initialisé.");
                return false;
            }
            bool t = true;
            if (this.Plateau.Test_Plateau(mot) == false || this.Dico.RechDicoRecursif(mot, 0, -2) == false)
            {
                t = false;
            }

            return t;
        }

        public void Lancerlejeu()
        {
            this.CreerDico();

            //jai modif ici
            this.CreerJoueursfr();
            this.CreerPlateau();
            for (int i = 0; i < this.Nbjoueurs; i++)
            {
                //initialiser liste de mots du joueur 
                Console.WriteLine("C'est au tour de " + this.Joueurs[i].Nom + "\n");
                DateTime startTime = DateTime.Now;
                TimeSpan duration = TimeSpan.FromMinutes(0.2);
                while (DateTime.Now - startTime < duration)
                {
                    this.Plateau.Afffichageplateau();
                    Console.WriteLine("Entrez un mot : \n");
                    string mot = Console.ReadLine();
                    Console.WriteLine("");
                    mot = mot.Trim().ToUpper();
                    bool v = this.Verif(mot);
                    if (v == true)
                    {
                        Console.WriteLine("Mot valide\n");
                        this.Joueurs[i].Add_Mot(mot);
                    }
                    this.Joueurs[i].Calculscore();
                    Console.WriteLine("Votre score est de : " + this.Joueurs[i].Score+"\n");
                }
                Console.WriteLine("Votre tour est terminé.\n");
                Console.WriteLine(this.Joueurs[i].ToString());
            }
            this.CreerTableauscore();
            foreach (Joueur i in this.Gagnant(this.Tableauscore))
            {
                Console.WriteLine("\n"+i.Nom.ToUpper() + " a gagné la partie avec un score de " + i.Score+"\n");
            }

        }
    }
}

        /// <summary>
        /// Lance le jeu (demander les noms des joueurs, la langue, lance le chrono, affiche le plateau, demande à l'utilisateur d'entrer des mots et vérifie s'ils sont éligiles... affiche le gagnant à la fin)
        /// </summary>

