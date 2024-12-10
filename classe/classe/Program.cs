using classe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Boogle
{
    public class Program
    {
        static void Main(string[] args) {
            
            Plateau plateau1 = new Plateau();
            Dictionnaire dico1 = new Dictionnaire();
            Dictionary<Joueur, int> tableauscore1= new Dictionary<Joueur, int>();
            Jeu jeu1 = new Jeu(plateau1, dico1,tableauscore1);
            jeu1.Lancerlejeu();
        }
        

    }
}



