using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace Projet_BDD__Aurélie_Elisa
{
    class Program
    {
        public static int Verification(int min, int max)
        {
            int result;
            while (!Int32.TryParse(Console.ReadLine(), out result) || !(min <= result && result <= max))
            {
                Console.WriteLine("\nErreur. Merci de sélectionner un entier entre " + min + " et " + max);
            }
            return result;
        }

        static void Main(string[] args)
        {
            
            //Connexion
            Connexion connexion = new Connexion();
            MySqlConnection conec ;
            conec = null;
            try
            {
                string StringAurelie = "SERVER = localhost; PORT = 3306; DATABASE = velomax; UID = user; PASSWORD = User123oui**";
                string StringElisa = "SERVER = localhost; PORT = 3306; DATABASE = Velomax; UID = root; PASSWORD = LiLi1312!!;";

                // Permet de tester sur nos deux bases de données
                // Attention, bien penser à changer la ligne suivante en fonction de la personne ------------------------------------------------------------------!!!
                string connexionString = StringAurelie;

                conec = new MySqlConnection(connexionString);
                conec.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }

            //Menu

            ConsoleKeyInfo cki;
            Console.WindowHeight = 30;
            Console.WindowWidth = 150;

            do
            {
                // On autorise plusieurs utilisateurs:
                Console.WriteLine("----- Bienvenue sur le site VéloMax ----- :\n\n\n");
                Console.WriteLine("-- LOGIN\n"
                    + "-- 1) ADMIN \n"
                    + "-- 2) NORMAL USER \n");

                int option = Verification(1, 2);

                Console.WriteLine();
                Console.Clear();

                switch (option)
                {
                    #region case 1
                    case 1:
                        // Lecture ou modification
                        do
                        {
                            Console.Clear();
                            List<List<string>> nb = connexion.Affichage_Piece_Quantite();

                            if (nb.Count != 0)
                            {
                                Console.WriteLine("Attention il faut racheter des pieces!!\n\n ");
                                Console.WriteLine(nb);
                                Console.WriteLine("\n-- Veuillez appuyer sur une touche");
                                Console.ReadKey();
                            }

                            Console.Clear();
                            Console.WriteLine("-- PORTAIL ADMINISTRATEUR --\n\n");
                            Console.WriteLine("-- 0) MODULE STATISTIQUE \n"
                             + "-- 1) Gestion des vélos \n"
                             + "-- 2) Gestion des pièces de rechange \n"
                             + "-- 3) Gestion des clients particuliers \n"
                             + "-- 4) Gestion des clients entreprise \n"
                             + "-- 5) Gestion des fournisseurs \n"
                             + "-- 6) Gestion des commandes \n"
                             + "-- 7) Gestion stock \n"
                             + "-- 8) Export stock faible Xml\n"
                             + "-- 9) DEMO \n"
                             + "\n"
                             + "Entrez le numero choisi");

                            int option2 = Verification(0,9);
                            Console.WriteLine();
                            Console.Clear();
                            switch (option2)
                            {
                                #region case 0

                                case 0:
                                    //MODULE STATISTIQUE
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine(" MODULE STATISTIQUE \n\n");
                                        Console.WriteLine("-- 1) Date d'expiration des programmes pour chaque individu \n"
                                         + "-- 2) Liste des membres pour chaque programme d'adhésion \n"
                                         + "-- 3) Recherche du meilleur client\n"
                                         + "\n\n"
                                         + "Rentrez le numéro correspondant à votre choix");

                                        int option3 = Verification(1, 3);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option3)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine(" Quantite vendude pour chaque piece \n\n");
                                                Connexion.Stat_DateExp(conec);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                Console.WriteLine(" Liste des membres pour chaque programme \n\n");
                                                Connexion.StatMembre_Programme(conec);
                                                break;
                                            case 3:
                                                Console.Clear();
                                                Console.WriteLine("Recherche du meilleur client... \n\n");
                                                Console.WriteLine("...selon la quantité d'items vendus: \n");
                                                //Ajouter fonction
                                                Console.WriteLine("\n...selon le cumul en euros: \n");
                                                //Ajouter fonction
                                                break;
                                            default: break;
                                            #endregion

                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez choisir une autre analyse statistique, appuyez sur la barre ESPACE"
                                         + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail de gestion ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                #endregion

                                case 1:
                                    // VELOS
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Création d'un nouveau vélo \n\n"
                                         + "-- 2) MAJ d'un vélo \n"
                                         + "-- 3) Suppression d'un vélo \n"
                                         + "-- 4) Voir tous les vélos \n"
                                         + "\n"
                                         + "Rentrez un numero");
                                        int option4 = Verification(1, 5);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Modele.CreationModele(conec);
                                                break;
                                            case 2:
                                                Modele.MajModele(conec);
                                                break;
                                            case 3:
                                                Modele.SuppresionModele(conec);
                                                break;
                                            case 4:
                                                Modele mod = new Modele();
                                                Console.Clear();
                                                Console.WriteLine(" Liste de tous les velos \n\n");
                                                mod.ListeModele();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez choisir une autre modification sur les vélos, appuyez sur la barre ESPACE"
                                        + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail de gestion ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                case 2:
                                    // PIECES DE RECHANGE
                                    do
                                    {
                                        Console.Clear();
                                        Fourniture piece = new Fourniture();
                                        Console.WriteLine(" Gestion des pièces de rechange \n\n");
                                        Console.WriteLine("-- 1) Création d'une nouvelle pièce \n\n"
                                         + "-- 2) MAJ d'une pièce \n"
                                         + "-- 3) Suppression d'une pièce \n"
                                         + "-- 4) Voir toutes les pièces \n"
                                         + "\n"
                                         + "Rentrez le numéro voulu");
                                        int option4 = Verification(1, 4);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case

                                            case 1:
                                                Console.Clear();
                                                Fourniture.CreationPiece_Fourniture(conec);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                Fourniture.MajPiece(conec);
                                                break;
                                            case 3:
                                                Console.Clear();
                                                Fourniture.SuppresionPiece(conec);
                                                break;
                                            case 4:
                                                Console.Clear();
                                                Console.WriteLine(" LISTE DE TOUTES LES PIECES \n\n");
                                                piece.ListeFournitures();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez choisir une autre modification sur les pièces, appuyez sur la barre ESPACE"
                                        + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail de gestion ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape); break;
                                case 3:
                                    // CLIENTS INDIVIDUS
                                    do
                                    {
                                        //création d'individu
                                        Individu indiv = new Individu();
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Ajout d'un nouveau client \n"
                                         + "-- 2) MAJ d'un client \n"
                                         + "-- 3) Suppression d'un client \n"
                                         + "-- 4) Liste de tous les clients \n"
                                         + "\n"
                                         + "Veuillez s'il vous plaît sélectionner le numéro correspondant à votre choix");
                                        int option4 = Verification(1, 4);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Individu.CreationNouveauClient_i(conec);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                Individu.MajClient_i(conec);
                                                break;
                                            case 3:
                                                Console.Clear();
                                                Individu.SuppresionCLient_i(conec);
                                                break;
                                            case 4:
                                                Console.Clear();
                                                Console.WriteLine(" Liste des clients individus \n\n");
                                                indiv.ListeIndividus();
                                                break;
                                            default: break;
                                                #endregion
                                        }

                                        Console.WriteLine("\n\n-- Si vous souhaitez choisir une autre modification sur les particuliers, appuyez sur la barre ESPACE"
                                        + "\nappuyez sur ECHAP si vous souhaitez accéder à un autre portail de gestion ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape); break;
                                case 4:
                                    // CLIENTS ENTREPRISES
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Ajout d'une nouvelle entreprise \n\n"
                                         + "-- 2) MAJ d'une entreprise \n"
                                         + "-- 3) Suppression d'une entreprise \n"
                                         + "-- 4) Voir toutes les entreprises \n"
                                         + "\n"
                                         + "Entrez le numero choisi");
                                        int option4 = Verification(1, 4);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Boutique.CreationNouveauClient_b(conec);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                Boutique.MajClient_b(conec);
                                                break;
                                            case 3:
                                                Console.Clear();
                                                Boutique.SuppresionClient_b(conec);
                                                break;
                                            case 4:
                                                Boutique bout = new Boutique();
                                                Console.Clear();
                                                Console.WriteLine(" Liste des entreprises clientes \n\n");
                                                bout.ListeBoutique();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez choisir une autre modification sur les entreprises, appuyez sur la barre ESPACE"
                                        + "\nAppuyez sur ECHAP si vous souhaitez accéder à un autre portail de gestion ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape); break;
                                case 5:
                                    // FOURNISSEURS
                                    do
                                    {
                                        Fournisseur four = new Fournisseur();
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Ajout d'un nouveau fournisseur \n\n"
                                         + "-- 2) MAJ d'un fournisseur \n"
                                         + "-- 3) Suppression d'un fournisseur \n"
                                         + "-- 4) Voir tous les fournisseur \n"
                                         + "\n"
                                         + "Veuillez s'il vous plaît sélectionner le numéro correspondant à votre choix");
                                        int option4 = Verification(1, 4);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case

                                            case 1:
                                                Console.Clear();
                                                Fournisseur.CreationFournisseur(conec);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                Fournisseur.MajFournisseur(conec);
                                                break;
                                            case 3:
                                                Console.Clear();
                                                Fournisseur.SuppressionFournisseur(conec);
                                                break;
                                            case 4:
                                                Console.Clear();
                                                Console.WriteLine("Liste des fournisseurs \n\n");
                                                four.ListeFournisseur();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez choisir une autre modification sur les fournisseurs, appuyez sur la barre ESPACE"
                                        + "\nAppuyez sur ECHAP si vous souhaitez accéder à un autre portail de gestion ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape); break;
                                case 6:
                                    //COMMANDES 
                                    do
                                    {
                                        Commande commande = new Commande();
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Ajout d'une commande \n\n"
                                         + "-- 2) MAJ d'une commande \n"
                                         + "-- 3) Suppression d'une commande \n"
                                         + "-- 4) Voir toutes les commandes \n"
                                         + "\n"
                                         + "rentrez le numero voulu");
                                        int option4 = Verification(1, 4);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case

                                            case 1:
                                                Console.Clear();
                                                Commande.CreationCommande(conec);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                Commande.MajCommande(conec);
                                                break;
                                            case 3:
                                                Console.Clear();
                                                Commande.SuppressionCommande(conec);
                                                break;
                                            case 4:
                                                Console.Clear();
                                                Console.WriteLine(" Liste des commandes \n\n");
                                                commande.ListeCommandes();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez choisir une autre modification sur les commandes, appuyez sur la barre ESPACE"
                                        + "\nAppuyez sur ECHAP si vous souhaitez accéder à un autre portail de gestion ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                case 7:
                                    //GESTION STOCK
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine(" GESTION STOCK \n\n");

                                        Console.WriteLine("-- 1) Gestion du stock par fournisseur \n\n"
                                         + "-- 2) Gestion du stock par pièce \n"
                                         + "-- 3) Gestion du stock par velo \n"
                                         + "-- 4) Gestion du stock par ligne de produit \n"
                                         + "-- 5) Gestion du stock par prix \n"
                                         + "\n"
                                         + "Entrez le numero choisi");
                                        int option4 = Verification(1, 5);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par fournisseur \n\n");
                                                Commande.Stock_fournisseur(conec);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par pièce \n\n");
                                                Commande.Stock_piece(conec);
                                                break;
                                            case 3:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par fournisseur \n\n");
                                                Commande.Stock_Velo(conec);
                                                break;
                                            case 4:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par pièce \n\n");
                                                Commande.Stock_categorie(conec);
                                                break;
                                            case 5:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par fournisseur \n\n");
                                                Commande.Stock_prix(conec);
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez retourner sur le portail gestion du stock, appuyez sur la barre ESPACE"
                                        + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                case 8:
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine(" EXPORT STOCK FAIBLE XML \n\n");
                                        connexion.ExportXmlStock();

                                        Console.WriteLine("\n\n-- Si vous souhaitez retourner sur le portail gestion du stock, appuyez sur la barre ESPACE"
                                        + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                case 9:
                                    do
                                    {   //MODE DEMO
                                        Console.Clear();
                                        Console.WriteLine("-- MODE DEMO --\n\n");
                                        Console.WriteLine(" Pour faire défiler les informations, appuyez sur la ENTREE\n Si vous souhaitez quitter le mode démo, appuyez sur une autre touche\n\n");
                                        Console.WriteLine(" Nombre de clients (entreprise et individus confondus):\n");

                                        //nombre de clients entreprises+individus
                                        int nbInd = Individu.CompteClientI(conec);
                                        int nbBout = Boutique.CompteClientB(conec);
                                        int tot = nbInd + nbBout;

                                        Console.WriteLine(" Il y a actuellement " + tot + " de clients");
                                        Console.WriteLine("\n--Appuyez sur entrée");

                                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                                        {
                                            Console.Clear();
                                            Console.WriteLine(" Nom des individus avec cumul de toutes les commandes en euros:\n");
                                            Connexion.CumulI(conec);
                                            Console.WriteLine(" Nom des boutiques avec cumul de toutes les commandes en euros:\n");
                                            Connexion.CumulB(conec);
                                            Console.WriteLine("\n--Appuyez sur entrée");

                                            if (Console.ReadKey().Key == ConsoleKey.Enter)
                                            {
                                                Console.Clear();
                                                Console.WriteLine(" Liste des produits ayant une quantité en stock <=2 :\n");
                                                connexion.Affichage(connexion.Affichage_Piece_Quantite(), new List<string> { "piece_description", "piece_numProduitF", "piece_prix", "quantite" });
                                                Console.WriteLine("\n--Appuyez sur entrée");
                                                if (Console.ReadKey().Key == ConsoleKey.Enter)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine(" Nombre de pièces fournis par fournisseur :\n");
                                                    Commande.Stock_fournisseur(conec);
                                                    Console.WriteLine(" Nombre de Velo fournis par fournisseur :\n");
                                                    Commande.Stock_Velo(conec);
                                                    Console.WriteLine("\n--Appuyez sur entrée");
                                                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(" Export en XML/JSON d'un table :\n");
                                                        connexion.ExportXml();
                                                        connexion.ExportJson();
                                                    }
                                                }
                                            }
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez vous déconnecter, appuyez sur ESPACE"
                                                           + "\nOu appuyez sur ECHAP si vous souhaitez retourner au menu principal ");

                                    } while (Console.ReadKey().Key != ConsoleKey.Escape) ;
                                    break;
                                default: break;
                                    #endregion

                            } while (Console.ReadKey().Key != ConsoleKey.Escape) ;
                            cki = Console.ReadKey();

                        } while (cki.Key != ConsoleKey.Escape);
                        break;

                    case 2:
                        //ESPACE UTILISATEUR: accès que en lecture
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("-- PORTAIL UTILISATEUR --\n\n");
                            Console.WriteLine("-- 1) Accès à l'espace vélos \n"
                             + "-- 2) Accès à l'espace Pièces de rechange \n"
                             + "-- 3) Accès à l'espace Clients : particuliers \n"
                             + "-- 4) Accès à l'espace Clients : entreprise \n"
                             + "-- 5) Accès à l'espace Fournisseurs \n"
                             + "-- 6) Accès à l'espace des Commandes \n"
                             + "-- 7) Accès aux stocks \n"
                             + "\n"
                             + "Entrez le numero selectionné");
                            int option5 = Verification(1, 6);
                            Console.WriteLine();
                            Console.Clear();
                            switch (option5)
                            {
                                #region case
                                case 1:
                                    //VELOS 
                                    do
                                    {
                                        Modele velo = new Modele();
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Voir la liste des vélos présents dans la base de données \n\n"
                                         + "\n"
                                         + "Entrez le numero");
                                        int option4 = Verification(1, 1);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine(" Liste des velos\n\n");
                                                velo.ListeModele();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez retourner sur le portail vélos, appuyez sur la barre ESPACE"
                                           + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                case 2:
                                    // PIECES DE RECHANGE
                                    do
                                    {
                                        Console.Clear();
                                        Fourniture piece = new Fourniture();
                                        Console.WriteLine("-- 1) Voir la liste de toutes les pièces de rechange \n\n"
                                         + "\n"
                                         + "Entrez le numero choisi");
                                        int option4 = Verification(1, 1);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine(" Liste des pieces \n\n");
                                                piece.ListeFournitures();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez retourner sur le portail pièce de rechange, appuyez sur la barre ESPACE"
                                         + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);

                                    break;
                                case 3:
                                    // CLIENTS INDIVIDUS 
                                    do
                                    {
                                        //on crée un nouvel individu
                                        Individu indiv = new Individu();
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Voir la liste de tous les clients particuliers \n"
                                         + "\n"
                                         + "Entrez le numero choisi");
                                        int option4 = Verification(1, 1);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine(" Liste des clients \n\n");
                                                indiv.ListeIndividus();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez retourner sur le portail pièce de rechange, appuyez sur la barre ESPACE"
                                         + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail de gestion ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                case 4:
                                    // CLIENTS ENTREPRISE
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Voir la liste de toutes les entreprises \n\n"
                                         + "\n"
                                         + "Entrez le numero choisi");
                                        int option4 = Verification(1, 1);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case

                                            case 1:
                                                Boutique bout = new Boutique();
                                                Console.Clear();
                                                Console.WriteLine(" Liste des boutiques \n\n");
                                                bout.ListeBoutique();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez retourner sur le portail clients entreprise, appuyez sur la barre ESPACE"
                                        + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                case 5:
                                    // FOURNISSEURS
                                    do
                                    {
                                        Fournisseur fourn = new Fournisseur();
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Voir la liste de tous les fournisseurs \n\n"
                                         + "\n"
                                         + "Entrez le numero choisi");
                                        int option4 = Verification(1, 1);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine(" Liste des fournisseurs \n\n");
                                                fourn.ListeFournisseur();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez retourner sur le portail des fournisseurs, appuyez sur la barre ESPACE"
                                        + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                case 6:
                                    // COMMANDES
                                    do
                                    {
                                        Commande commande = new Commande();
                                        Console.Clear();
                                        Console.WriteLine("-- 1) Voir la liste toutes les commandes \n\n"
                                         + "\n"
                                         + "Entrez le numero choisi");
                                        int option4 = Verification(1, 1);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case                                                     
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine(" Liste des commandes \n\n");
                                                commande.ListeCommandes();
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez retourner sur l'espace commandes, appuyez sur la barre ESPACE"
                                        + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail de lecture ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                case 7:
                                    //GESTION STOCK
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine(" GESTION STOCK \n\n");

                                        Console.WriteLine("-- 1) Gestion du stock par fournisseur \n\n"
                                         + "-- 2) Gestion du stock par pièce \n"
                                         + "-- 3) Gestion du stock par velo \n"
                                         + "-- 4) Gestion du stock par ligne de produit \n"
                                         + "-- 5) Gestion du stock par prix \n"
                                         + "\n"
                                         + "Entrez le numero choisi");
                                        int option4 = Verification(1, 5);
                                        Console.WriteLine();
                                        Console.Clear();
                                        switch (option4)
                                        {
                                            #region case
                                            case 1:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par fournisseur \n\n");
                                                Commande.Stock_fournisseur(conec);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par pièce \n\n");
                                                Commande.Stock_piece(conec);
                                                break;
                                            case 3:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par fournisseur \n\n");
                                                Commande.Stock_Velo(conec);
                                                break;
                                            case 4:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par pièce \n\n");
                                                Commande.Stock_categorie(conec);
                                                break;
                                            case 5:
                                                Console.Clear();
                                                Console.WriteLine(" Gestion du stock par fournisseur \n\n");
                                                Commande.Stock_prix(conec);
                                                break;
                                            default: break;
                                                #endregion
                                        }
                                        Console.WriteLine("\n\n-- Si vous souhaitez retourner sur le portail gestion du stock, appuyez sur la barre ESPACE"
                                        + "\nOu appuyez sur ECHAP si vous souhaitez accéder à un autre portail ");
                                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                                    break;
                                default: break;
                                    #endregion
                            }
                            Console.WriteLine("\n\n-- Si vous souhaitez accéder à un autre portail de lecture, appuyez sur la barre ESPACE"
                                         + "\nOu appuyez sur ECHAP si vous souhaitez vous déconnecter");
                        } while (Console.ReadKey().Key != ConsoleKey.Escape);
                        break;
                    default: break;
                }
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
        }
    }
}