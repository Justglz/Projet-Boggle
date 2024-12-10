using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using Boogle;

namespace Boogle
{
    public class Dictionnaire
    {
        private string fichier_dico_brut;
        private string langue;
        private string[] dico_trié;


        #region propriétés

        public string Langue
        {
            get { return this.langue; }
            set { this.langue = value; }
        }
        public string Fichier_dico_brut
        {
            get { return this.fichier_dico_brut; }
            set { this.fichier_dico_brut = value; }
        }
        public string[] Dico_trié
        {
            get { return this.dico_trié; }
            set { this.dico_trié = value; }
        }
        #endregion

        /// <summary>
        /// attribue le bon fichier contenant tous les mots de la langue sélectionnée et créer une instance dictionnaire
        /// </summary>
        /// <returns>un dictionanire correspondant à la langue sélectionné par le joueur</returns>
        public void Demander_Langue() { 
        
            

            Console.WriteLine("Veuillez choisir une langue (francais/anglais) : \n");
            string choix = Console.ReadLine().Trim().ToLower();
            Console.WriteLine("");

            // Boucle pour garantir une entrée valide
            while (choix != "francais" && choix != "anglais")
            {
                Console.WriteLine("Choix invalide. Veuillez entrer 'francais' ou 'anglais' : ");
                choix = Console.ReadLine().Trim().ToLower();
            }

            // Attribuer la langue choisie
            if (this.Langue == "francais")
            {
                this.Langue = choix;
                this.Fichier_dico_brut = "MotsPossiblesFR.txt";
            }
            else
            {
                this.Langue = choix;
                this.Fichier_dico_brut = "MotsPossiblesEN.txt";
            }

        }



        /// <summary>
        /// Créer un tableau à partir du fichier brut fourni par la prof qui attribue à chaque mot (séparé d'un espacce) une case du tableau
        /// </summary>
        /// <returns>tableau de tous les mots du dictionnaire attibué à une langue</returns>
        public string[] Separation_Mot_Dico()
        {
            string[] separation = File.ReadAllText(this.Fichier_dico_brut).Split(' ');
            for (int i = 0; i < separation.Length; i++)
            {
                separation[i] = separation[i].Trim();  // Trim() pour chaque mot du tableau
            }
            return separation;
        }

        /// <summary>
        /// Fait le trie du tableau contenant tous les mots d'une langue par ordre alphabétique et attribue le tableau trié à l'attribut dico_trié de l'instance courrante
        /// </summary>
        /// <returns>retourne le tableau de mots trié</returns>
        //public string[] Trie_Dico()
        //{
        //    string[] mots = this.Separation_Mot_Dico();
        //    for (int i = 0; i < mots.Length - 1; i++)
        //    {
        //        for (int j = 0; j < mots.Length - 1 - i; j++)
        //        {
        //            if (String.Compare(mots[j], mots[j + 1]) > 0)
        //            {
        //                string memoire = mots[j];
        //                mots[j] = mots[j + 1];
        //                mots[j + 1] = memoire;
        //            }
        //        }
        //    }
        //    this.Dico_trié = mots;
        //    return mots;
        //}

        public static string[] Trie_Dico(string[] tableau)
        {
            // Si le tableau contient un seul élément ou est vide, il est déjà trié
            if (tableau.Length <= 1)
                return tableau;

            // Diviser le tableau en deux parties
            int milieu = tableau.Length / 2;
            string[] gauche = new string[milieu];
            string[] droite = new string[tableau.Length - milieu];

            Array.Copy(tableau, 0, gauche, 0, milieu);
            Array.Copy(tableau, milieu, droite, 0, tableau.Length - milieu);

            // Appliquer récursivement TriFusion sur chaque moitié
            gauche = Trie_Dico(gauche);
            droite = Trie_Dico(droite);

            // Fusionner les deux moitiés triées
            return Fusionner(gauche, droite);
        }

        public static string[] Fusionner(string[] gauche, string[] droite)
        {
            string[] resultat = new string[gauche.Length + droite.Length];
            int indexGauche = 0, indexDroite = 0, indexResultat = 0;

            // Fusionner les éléments des deux tableaux en respectant l'ordre alphabétique
            while (indexGauche < gauche.Length && indexDroite < droite.Length)
            {
                if (string.Compare(gauche[indexGauche], droite[indexDroite], StringComparison.Ordinal) <= 0)
                {
                    resultat[indexResultat] = gauche[indexGauche];
                    indexGauche++;
                }
                else
                {
                    resultat[indexResultat] = droite[indexDroite];
                    indexDroite++;
                }
                indexResultat++;
            }

            // Ajouter les éléments restants du tableau gauche
            while (indexGauche < gauche.Length)
            {
                resultat[indexResultat] = gauche[indexGauche];
                indexGauche++;
                indexResultat++;
            }

            // Ajouter les éléments restants du tableau droit
            while (indexDroite < droite.Length)
            {
                resultat[indexResultat] = droite[indexDroite];
                indexDroite++;
                indexResultat++;
            }

            return resultat;
        }

        /// <summary>
        /// Sauvegarde le tableau de tous les mots du dictionnaire dans un fichier pour pouvoir être réutilisé sans avoir besoin de repasser par la pethode Separation_Mot_Dico et Trie_Dico pour optimiser la complexité temporel
        /// </summary>
        public void Ecriture_Dico_Trié()
        {
            if (this.Langue == "anglais")
            {
                File.AppendAllLines("Dico_anglais_trie.txt", this.Dico_trié); // File.AppendAllLines pour inserer plusieurs lignes à la fois avec retour à la lgine
            }
            else
            {
                File.AppendAllLines("Dico_francais_trie.txt", this.Dico_trié);

            }

        }

        /// <summary>
        /// Permet de générer le tableau trié de mots à partir du fichier brut ou du fichier traité (dictionnaire trié) et l'attribuer à l'attribut dico_trié de l'instance courrante
        /// </summary>
        public void Readfile()
        {
            if (this.Langue == "anglais")
            {
                try    //Si le fichier avec tous les mots trié existe:
                {
                    this.Dico_trié = File.ReadAllLines("Dico_anglais_trie.txt");  // "File.ReadAllLines" revoie un tableau qui associe à chaque ligne une case du tableau

                }
                catch  //Créer le fichier pour pouvoir le réutilier plus tard et opitmiser la complexité spatiale
                {
                    this.Dico_trié = Trie_Dico(this.Separation_Mot_Dico());
                    this.Ecriture_Dico_Trié();
                }
            }
            else
            {
                try
                {
                    this.Dico_trié = File.ReadAllLines("Dico_francais_trie.txt");
                }
                catch
                {
                    this.Dico_trié = Trie_Dico(this.Separation_Mot_Dico());
                    this.Ecriture_Dico_Trié();
                }
            }
        }

        /// <summary>
        /// Rechercher un mot dans le dicitonnaire de facon recursive
        /// </summary>
        /// <param name="mot">mot à chercher</param>
        /// <param name="debut">valeur initiale du tableau à parcourir</param>
        /// <param name="fin">valeur finale du tableau à parcourir</param>
        /// <returns> retourne true si le mot appartient au dictionnaire, sinon false</returns>
        public bool RechDicoRecursif(string mot, int debut = 0, int fin = -2)
        {
            if (fin == -2)
            {
                fin = this.Dico_trié.Length - 1;// l'initialiser ici car on ne peut pas mettre dico_trie.Length dans les paramètres, prendre -2 car la valeur sera jamais atteinte
            }
            if (debut > fin)
            {
                Console.WriteLine("Le mot n'existe pas dans le dictionnaire.");
                return false;

            }
            int milieu = (debut + fin) / 2;
            if (this.Dico_trié[milieu] == mot)
            {
                return true;
                
            }
            else
            {
                if (String.Compare(this.Dico_trié[milieu], mot) < 0)
                {
                    return RechDicoRecursif(mot, milieu + 1, fin);
                }
                else
                {
                    return RechDicoRecursif(mot, debut, milieu - 1);
                }
            }
        }

        /// <summary>
        /// Compte pour chaque lettre le nombre de mots du dictionnaire commencant par celles ci
        /// </summary>
        /// <returns>renvoi un tableau de taille 26 qui contient le nombre pour de mots pour chaque lettres</returns>
        public int[] Nombre_Mot_Par_Lettre()
        {
            int[] nombres_Mot_Par_Lettre = new int[26];
            for (int i = 0; i < 26; i++)
            {
                int compteur = 0;
                for (int j = 0; j < this.Dico_trié.Length; j++)
                {
                    if (this.Dico_trié[j][0] == (char)(65 + i)) // code ascii où 'A'=65
                    {
                        compteur++;
                    }
                }
                nombres_Mot_Par_Lettre[i] = compteur;
            }
            return nombres_Mot_Par_Lettre;
        }


        /// <summary>
        /// Determine la taille du mot le plus long du dictionnaire
        /// </summary>
        /// <returns>la tailledu mot le plus long</returns>
        public int Taille_Mot_Plus_Long()
        {
            int taille = this.Dico_trié[0].Length;
            for (int i = 1; i < this.Dico_trié.Length; i++)
            {
                if (taille < this.Dico_trié[i].Length)
                {
                    taille = this.Dico_trié[i].Length;
                }
            }
            return taille;
        }



        /// <summary>
        /// Calcule le nombre de mot dans le dictionnaire qui a une certaine longueur
        /// </summary>
        /// <returns>tableau de nombre de mot correspondant aux differentes tailles par ordre croissant</returns>
        public int[] Nombre_Mot_Par_Longueur()
        {
            int[] nombre_Mot_Par_Longueur = new int[this.Taille_Mot_Plus_Long()];
            for (int i = 0; i < this.Taille_Mot_Plus_Long(); i++)
            {
                nombre_Mot_Par_Longueur[this.Dico_trié[i].Length - 1]++;
            }
            return nombre_Mot_Par_Longueur;
        }

        /// <summary>
        /// Créer une chaine de caradctère qi contient les information sur le nombre de mots par longueur et par lettre, mais aussi la langue du dictionnaire
        /// </summary>
        /// <returns>retourner la chaine de caracctère</returns>
        public override string ToString()
        {
            string chaine = "Nombre de mots par longueur :\n";
            for (int i = 0; i < this.Nombre_Mot_Par_Longueur().Length; i++)
            {
                
                chaine += "\nil y a " + this.Nombre_Mot_Par_Longueur()[i] + " mots qui ont une longuer de " + (i + 1) + "lettres";
                
            }
            chaine += "\n\nNombre de mot par lettre";
            for (int j = 0; j < this.Nombre_Mot_Par_Lettre().Length; j++)
            {
                chaine += "\nil y a " + this.Nombre_Mot_Par_Lettre()[j] + " qui commence par la lettre" + (char)(j);
            }
            chaine += "\n\nLangue: " + this.Langue;
            return chaine;

        }



    }
}

