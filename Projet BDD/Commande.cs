using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Projet_BDD__Aurélie_Elisa
{
    class Commande
    {
        Connexion conec;
        public int commande_num { get; }
        public string commande_dateC { get; set; }
        public string commande_date_livraison { get; set; }
        public int quantite { get; set; }
        public string boutique_nom { get; set; }
        public string individu_nom { get; set; }
        public int modele_num { get; set; }
        public string piece_num { get; set; }

        public Commande(int commande_num, string commande_dateC, string commande_date_livraison, int quantite, string boutique_nom, string individu_nom, int modele_num, string piece_num)
        {
            this.conec = new Connexion();
            this.commande_num = commande_num;
            this.commande_dateC = commande_dateC;
            this.commande_date_livraison = commande_date_livraison;
            this.quantite = quantite;
            this.boutique_nom = boutique_nom;
            this.individu_nom = individu_nom;
            this.modele_num = modele_num;
            this.piece_num = piece_num;
            conec.Co.Close();
        }

        public Commande()
        {
            this.conec = new Connexion();
            conec.Co.Close();
        }

        //Affichage
        public string toString()
        {
            string res = commande_num + " | date_commande: " + commande_dateC + ", date de livraison: " + commande_date_livraison + ", quantite: " + quantite + ", nom de la boutique: " +
                boutique_nom + ", nom de la personne: " + individu_nom + ", numero du modele: " + modele_num + ", numero de la pice: " + piece_num;
            return res;
        }

        public List<Commande> ListeCommandes()
        {
            conec.Co.Open();
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM Commande;";
            List<Commande> listeCom = conec.SelectCommande(requete, param);
            for (int i = 0; i < listeCom.Count; i++)
            {
                Console.WriteLine(listeCom[i].toString());
            }
            conec.Co.Close();
            return listeCom;
        }

        // Création, suppression et mise à jour des commandes

        #region Gestion des commandes :

        public static void CreationCommande(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez créer une commande");

            Console.WriteLine("Quelle est la date de commande ? ");
            string c = Console.ReadLine();
            Console.WriteLine("Quelle est la date de livraison ? ");
            string l = Console.ReadLine();
            Console.WriteLine("Quel est la quantitée commandée ? ");
            int q = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("'boutique' ou 'individu' ? ");
            string answer = Console.ReadLine();

            if (answer == "boutique")
            {
                Console.WriteLine("Quelle est le nom de la boutique ? ");
            }
            if (answer == "individu") { Console.WriteLine("Quel est le nom de l'individu ? "); }
            string i = Console.ReadLine();
            string b = answer + "_nom";

            Console.WriteLine("'modele' ou 'piece' ? ");
            string answer2 = Console.ReadLine();

            if (answer2 == "modele")
            {
                Console.WriteLine("Quel est le numero du modele ? ");
            }
            if (answer2 == "piece")
            {
                Console.WriteLine("Quel est le numero de la piece ? ");
            }

            int p = Convert.ToInt32(Console.ReadLine());
            string m = answer2 + "_num";

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
             $" INSERT INTO commande (commande_num,commande_dateC,commande_date_livraison,quantite,{b},{m}) " +
             $"VALUES (\"{c}\",\"{l}\",{q},\"{i}\",{p});";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Creation effectuée");
            }

            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }
        public static void SuppressionCommande(MySqlConnection connection)
        {
            Console.WriteLine("Entrez le numero de la commande");
            int attribut = Convert.ToInt32(Console.ReadLine());

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $" DELETE FROM commande WHERE commande_num ={attribut};";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Suppression effectuée");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" Erreur de connexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }
        public static void MajCommande(MySqlConnection connection)
        {
            Console.WriteLine("Que voulez-vous modifier ? Ecrivez : commande_num,commande_dateC,commande_date_livraison,quantite,boutique_nom,individu_nom,modele_num ou piece_num");
            string attribut = Console.ReadLine();

            int siret = -1;
            string mod = "";

            Console.WriteLine("Entrez le numero de commande à modifier : ");
            int lieu = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Quelle est la modification ? ");

            if (attribut == "modele_num" || attribut == "quantite" || attribut == "commande_num")
            {
                siret = Convert.ToInt32(Console.ReadLine());
            }
            else { mod = Console.ReadLine(); }

            MySqlCommand command = connection.CreateCommand();

            if (siret != -1)
            {
                command.CommandText = $" UPDATE Commande SET {attribut}={siret} WHERE commande_num = {lieu};";
            }
            else
            {
                command.CommandText = $" UPDATE Commande SET {attribut}=\"{mod}\" WHERE commande_num = {lieu};";
            }

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Maj effectuée");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" Erreur de connexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }
        #endregion

        // Gestion des stocks
        # region Gestion des stocks
        public static void Stock_piece(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT piece_num,count(piece_numProduitF)"
                                + " FROM Fourniture"
                                + " GROUP BY piece_num";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            Console.WriteLine("piece_num , quantite");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + "         ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
            reader.Close();
        }

        public static void Stock_Velo(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT modele_nom,count(modele_num)"
                                + " FROM modele"
                                + " GROUP BY modele_nom";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            Console.WriteLine("modele_nom, quantite");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + "         ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
            reader.Close();
        }

        public static void Stock_fournisseur(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT fournisseur_nom,count(piece_num)"
                                + " FROM Fournisseur"
                                + " NATURAL JOIN Fourniture"
                                + " GROUP BY fournisseur_nom";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            Console.WriteLine("nom,             quantite");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + "         ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
            reader.Close();
        }

        public static void Stock_categorie(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT ligne_produit,count(modele_num)"
                                + " FROM Modele"
                                + " GROUP BY ligne_produit";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            Console.WriteLine("ligne_produit , quantite");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + "         ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
            reader.Close();
        }

        public static void Stock_prix(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT modele_nom,modele_prix"
                                + " FROM Modele"
                                + " ORDER BY modele_prix,modele_num DESC ";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            Console.WriteLine("nom,          piece_num");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + "         ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
            reader.Close();
        }
        #endregion
    }
}