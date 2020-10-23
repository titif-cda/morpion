using System;

namespace morpion
{
   
    class Program
    {
        enum EtatCase
        {
            Vide,
            Rond,
            Croix
        }
        static EtatCase[,] grille;// grille de 3*3
        static Random generateur; //générateur aléatoire
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // afficher message d'acceuil
            Console.WriteLine("Bienvenue dans le jeu du morpion\n");

            //Initialiser les variables

            bool finDeJeu = false;
            grille = new EtatCase[3, 3];
            int nbVide = 9;
            generateur = new Random();

            //afficher la grille
            AfficherGrille();

            // boucle principale
            while (!finDeJeu)
            {
                // jeu de l'utilisateur
                ChoisirCaseUtilisateur();
                nbVide--;

                //jeu gagnant
                bool gagne = Jeuxgagnant(EtatCase.Croix);
                if (gagne)
                {
                    finDeJeu = true;
                    Console.WriteLine("Bravo, vous avez gagné !");
                }
                //affichage de la grille
                AfficherGrille();

                // jeu de l'ordinateur
                if (!finDeJeu && nbVide >0)
                {
                    ChoisirCaseOrdinateur();
                    nbVide--;

                //affichage de la grille
                Console.WriteLine("l'ordinateur a joué");
                AfficherGrille();

                    //jeu gagnant?
                    if (Jeuxgagnant(EtatCase.Rond))
                    {
                        finDeJeu = true;
                        Console.WriteLine("Dommage, l'ordinateur a gagné :(");
                    }  
                }
                //match nul?
                if (nbVide ==0)
                {
                    Console.WriteLine("Match nul ! :(");
                    finDeJeu = true;

                }
            }
            //fin du jeu
            Console.WriteLine("Appuyer sur une touche pour fermer...");
            Console.ReadKey();
        }

        private static bool Jeuxgagnant(EtatCase etatCase)
        {
            //Cas d'une ligne
            for (int ligne = 0; ligne < 3; ligne++)
            {
                if (grille[ligne,0] == etatCase && grille[ligne, 1] == etatCase && grille[ligne, 2] == etatCase)
                {
                    return true;
                }
            }
            //Cas d'une colone
            for (int colonne = 0; colonne < 3; colonne++)
            {
                if (grille[colonne, 0] == etatCase && grille[colonne, 1] == etatCase && grille[colonne, 2] == etatCase)
                {
                    return true;
                }
            }
            //Cas des diagonales
            if (grille[0,0]== etatCase && grille[1,1] == etatCase && grille[2,2] == etatCase)
            {
                return true;
            }
            if (grille[2,0] == etatCase && grille[1, 1] == etatCase && grille[0, 2] == etatCase)
            {
                return true;
            }
            //par defaut on a pas gagné
            return false;
        }

        private static void ChoisirCaseOrdinateur()
        {
            // on boucle tant que le choix n'est pas correct
            bool choixOk = false;
            while (!choixOk)
            {
                //Choix des coordonnées
                int ligne = generateur.Next(0, 3);
                int colonne = generateur.Next(0, 3);
                if (grille[ligne,colonne] ==  EtatCase.Vide)
                {
                    
                    grille[ligne, colonne] = EtatCase.Rond; 
                    choixOk = true;
                }
            }
        }

        
        private static void ChoisirCaseUtilisateur()
        {
            bool choixOk = false;

            while (!choixOk)
            {
                //Message
                Console.WriteLine("Donnez votre choix de case");
               
                //Récupération de la réponse
                string reponse = Console.ReadLine();
                int choix;

                //Conversion vers entier, compris entre 0 et 8
                if (int.TryParse(reponse, out choix) && choix >= 0 && choix <= 8)
                {
                    //Case vide?
                    int ligne = choix / 3;
                    int colonne = choix % 3;

                    if (grille[ligne, colonne] == EtatCase.Vide)
                    {
                        //Choix ok je valide
                        grille[ligne, colonne] = EtatCase.Croix;
                        choixOk = true;
                    }
                }
            }
        }

        /// <summary>
        /// Afficher la grille du morpion
        /// </summary>
        private static void AfficherGrille()
        {
            string dessinGrille = "\n";

            // trait du haut
            dessinGrille += "*******\n";

            // Pour chaque ligne
            for (int ligne = 0; ligne < 3; ligne++)
            {
                dessinGrille += "|";
                // pour chaque colonne
                for (int colonne = 0; colonne < 3; colonne++)
                {
                    switch (grille[ligne, colonne])
                    {
                        case EtatCase.Vide:
                            dessinGrille += ligne * 3 + colonne;
                            break;
                        case EtatCase.Rond:
                            dessinGrille += "O";
                            break;
                        case EtatCase.Croix:
                            dessinGrille += "X";
                            break;
                        default:
                            break;
                    }
                    dessinGrille += "|";
                }
                dessinGrille += "\n*******\n";
            }
            //afficher la grille
            Console.WriteLine(dessinGrille);
        }
    }
}
