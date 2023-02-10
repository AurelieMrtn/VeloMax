using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Projet_BDD__Aurélie_Elisa
{
    class Fourniture
    {
        Connexion conec;

        public string piece_numProduitF { get; set; }
        public string fournisseur_nom { get; set; }
        public double piece_prix { get; set; }
        public string piece_dateDiscon { get; set; }
        public string piece_date_intro { get; set; }
        public string piece_delai_appro { get; set; }
        public string piece_num { get; set; }

        public Fourniture(string piece_numProduitF, string fournisseur_nom, double piece_prix, string piece_dateDiscon, string piece_date_intro, string piece_delai_appro, string piece_num)
        {
            this.conec = new Connexion();
            this.piece_numProduitF = fournisseur_nom;
            this.fournisseur_nom = fournisseur_nom;
            this.piece_prix = piece_prix;
            this.piece_dateDiscon = piece_dateDiscon;
            this.piece_date_intro = piece_date_intro;
            this.piece_delai_appro = piece_delai_appro;
            this.piece_num = piece_num;
            conec.Co.Close();
        }

        public Fourniture()
        {
            this.conec = new Connexion();
            conec.Co.Close();
        }

        //Affichage
        public string toString()
        {
            string res = "Numero de la piece du fournisseur: " + piece_numProduitF + "| nom du fournisseur: " + fournisseur_nom + ",prix de la piece: " + piece_prix + ", date de discontinuation: " + piece_dateDiscon + 
                ", date d'introduction: " + piece_date_intro + ",delai d'approvisionnement: " + piece_delai_appro + ", numero de la piece: " + piece_num;
            return res;
        }

        public List<Fourniture> ListeFournitures()
        {
            conec.Co.Open();
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM Fourniture;";
            List<Fourniture> listePiF = conec.SelectFourniture(requete, param);
            for (int i = 0; i < listePiF.Count; i++)
            {
                Console.WriteLine(listePiF[i].toString());
            }
            conec.Co.Close();
            return listePiF;
        }

        // Création, suppression et mise à jour des fournitures

        #region Gestion des fournitures (dépendance pièce)
        public static void CreationPiece_Fourniture(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez créer une fourniture ");

            Console.WriteLine("Quel est le numero de piece du fournisseur ? ");
            string piece_numProduitF = Console.ReadLine();
            Console.WriteLine("Quel est le nom du fournisseur ? ");
            string fournisseur_nom = Console.ReadLine();
            Console.WriteLine("Quel est le prix de la piece ? ");
            double piece_prix = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Quel est la date de discontinuation ? ");
            string piece_dateDiscon = Console.ReadLine();
            Console.WriteLine("Quel est la date d'introduction ? ");
            string piece_date_intro = Console.ReadLine();
            Console.WriteLine("Quel est le delai d'approvisionnement ? ");
            string piece_delai_appro = Console.ReadLine();
            Console.WriteLine("Quel est le numero de la piece ? ");
            string piece_num = Console.ReadLine();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
             $" INSERT INTO Fourniture(piece_numProduitF,fournisseur_nom,piece_prix,piece_dateDiscon,piece_date_intro,piece_delai_appro,piece_num) " +
             $"VALUES (\"{piece_numProduitF}\",\"{fournisseur_nom}\",{piece_prix},\"{piece_dateDiscon}\",\"{piece_date_intro}\",\"{piece_delai_appro}\",\"{piece_num}\");";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Creation effectuée");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" Erreur de connexion : " + e.ToString());
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Dans la table piece : ");
            Console.WriteLine("Quel est la description de la piece ? ");
            string piece_description = Console.ReadLine();

            command.CommandText =
            $" INSERT INTO Piece(piece_num,piece_description) VALUES (\"{piece_num}\",\"{piece_description}\");";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Creation affectuée");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }

        public static void SuppresionPiece(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez supprimer une piece ");
            Console.WriteLine("Quel est le numéro de la pièce ? ");
            string mod = Console.ReadLine();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $" DELETE FROM Piece WHERE piece_num =\"{mod}\";";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Suppression effectué");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }

            command.CommandText = $" DELETE FROM Commande WHERE piece_num=\"{mod}\";";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Suppression effectué");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }

            command.CommandText = $" DELETE FROM Fourniture WHERE piece_num =\"{mod}\";";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Suppression effectué");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }

        public static void MajPiece(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez modifier une piece (fourniture)");
            Console.WriteLine("Que voulez-vous modifier ? Ecrivez : piece_numProduitF, fournisseur_nom, piece_prix, piece_dateDiscon, piece_date_intro, piece_delai_appro ou piece_num");
            string attribut = Console.ReadLine();
            double prix = -1.0;
            string mod = "";

            Console.WriteLine("Entrez le numero de la piece du fournisseur à modifier : ");
            string lieu = Console.ReadLine();
            Console.WriteLine("Quelle est la modification ? ");

            if (attribut == "prix_piece")
            {
                prix = Convert.ToDouble(Console.ReadLine());
            }
            else { mod = Console.ReadLine(); }

            MySqlCommand command = connection.CreateCommand();

            if (prix != -1.0)
            {
                command.CommandText = $" UPDATE Fourniture SET {attribut}={prix} WHERE piece_num = {lieu};";
            }
            else
            {
                command.CommandText = $" UPDATE Fourniture SET {attribut}=\"{mod}\" WHERE piece_num = {lieu};";
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

            if (attribut == "piece_num")
            {
                command.CommandText = $" UPDATE Commande SET piece_num=\"{mod}\" WHERE piece_num = {lieu};";
            }
            command.Dispose();
        }
        #endregion
    }
}