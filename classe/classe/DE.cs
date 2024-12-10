using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace classe
{
    public class DE
    {
        private string[] faces;
        private string visible;
        public static Dictionary<string, int[]> dico = new Dictionary<string, int[]>();
        public static List<string> lettres = new List<string>();

        #region propriétés
        public string[] Faces
        {
            get { return this.faces; }
            set { this.faces = value; }
        }
        public string Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
        #endregion

        /// <summary>
        /// Créer un dictionnaire à partir du fichier "lettres" qui va attribuer à chaque lettres (la clée) les valeurs d'occurences et le poids qui leur correspondent sous forme d'un tableau d'entier
        /// </summary>
        /// <param name="lienfichier">fichier lettre</param>
        public static void Creerdico(string lienfichier)
        {
            try
            {
                foreach (string ligne in File.ReadLines(lienfichier))
                {
                    string[] separe = ligne.Split(';');

                    // Vérifier que la ligne contient suffisamment d'éléments avant de tenter de les convertir
                    if (separe.Length >= 3)
                    {
                        string cle = separe[0].Trim().ToUpper();
                        int[] valeurs = new int[2];

                        // Tentative de conversion des valeurs
                        int valeur1 = Convert.ToInt32(separe[1].Trim());
                        int valeur2 = Convert.ToInt32(separe[2].Trim());

                        valeurs[0] = valeur1;
                        valeurs[1] = valeur2;
                        dico[cle] = valeurs; // Ajoute dans le dictionnaire
                    }
                    else
                    {
                        Console.WriteLine($"La ligne ne contient pas suffisamment de données : {ligne}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du traitement du fichier : {ex.Message}");
            }
        }


        /// <summary>
        /// Ajoute à la liste "lettre" x fois la lettre en fonction de la fréquence d'occurence qui à été attribué à la lettre (qui correspond à la deuxième valeur du tableau d'entier de chaque clé)
        /// </summary>
        public static void Creerlistelettres()
        {
            Creerdico("Lettres.txt");
            foreach (KeyValuePair<string, int[]> indice in dico) //KeyValuePair<string, int[]> correspond à la signature des éléments du dictionnaire
            {
                for (int i = 0; i < indice.Value[1]; i++)
                {
                    lettres.Add(indice.Key);
                }
            }
        }

        /// <summary>
        /// Attribue un tableau de lettre correspondant aux faces d'un dé à l'attribut de classe "faces" de l'instance courrante
        /// </summary>
        public void Tableaufaces()
        {
            string[] tableau = new string[6];
            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                int h = rand.Next(0, lettres.Count - 1);
                tableau[i] = lettres[h];
                lettres.Remove(lettres[h]); //ne supprime qu'une lettre mais pas toutes les occurences de la meme lettre dans la liste?

            }
            this.Faces = tableau;
        }

        /// <summary>
        /// attribue la lettre correspondant à la face visible à l'attribut de classe "visible" de l'instance courrante
        /// </summary>
        /// <param name="rand"></param>
        public void Lance(Random rand)
        {
            this.Visible = this.Faces[rand.Next(0, 5)];
        }

        /// <summary>
        /// Retourne une chaine de caractère qui décrit un dé (les faces du dé et la face visibles)
        /// </summary>
        /// <returns>chaine de caractère</returns>
        public override string ToString()
        {
            string S = "";
            for (int i = 0; i < this.Faces.Length; i++)
            {
                S = S + this.Faces[i];
                if (i < this.Faces.Length - 1)
                {
                    S = S + ", ";
                }
            }
            return ("Les faces du dé sont " + S + ".\nLa face visible est" + this.Visible);
        }
    }
}

