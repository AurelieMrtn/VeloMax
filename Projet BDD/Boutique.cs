using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD__Aurélie_Elisa
{
    class Boutique
    {
        Connexion conec;

        //Proprietes
        public string boutique_nom { get; set; }
        public string boutique_adresse { get; set; }
        public string boutique_tel { get; set; }
        public string boutique_courriel { get; set; }
        public string boutique_contact { get; set; }
        public string boutique_remise { get; set; }
        public int volumeTot { get; set; }

        //Constructeur
        public Boutique(string boutique_nom, string boutique_adresse, string boutique_tel, string boutique_courriel, string boutique_contact, string boutique_remise, int volumeTot)
        {
            this.conec = new Connexion();
            this.boutique_nom = boutique_nom;
            this.boutique_adresse = boutique_adresse;
            this.boutique_tel = boutique_tel;
            this.boutique_courriel = boutique_courriel;
            this.boutique_contact = boutique_contact;
            this.boutique_remise = boutique_remise;
            this.volumeTot = volumeTot;
            conec.Co.Close();
        }

        public Boutique()
        {
            this.conec = new Connexion();
            conec.Co.Close();
        }

        //affichage

        public string toString()
        {

            string res = boutique_nom + ", adresse: " + boutique_adresse + ", telephone: " + boutique_tel + ", courriel: " +
                boutique_courriel + ", contact: " + boutique_contact + ", remise: " + boutique_remise + ", volume total: " + volumeTot;
            return res;
        }
        public List<Boutique> ListeBoutique()
        {
            conec.Co.Open();
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM client_boutique;";
            List<Boutique> listeB = conec.SelectBoutique(requete, param);
            for (int i = 0; i < listeB.Count; i++)
            {
                Console.WriteLine(listeB[i].toString());
            }
            conec.Co.Close();
            return listeB;
        }

        public static int CompteClientB(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT count(*)"
                                + " FROM Client_Boutique";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            Console.WriteLine(" Client_boutique");
            int[] valueString = new int[reader.FieldCount];
            while (reader.Read())
            {
                valueString[0] = reader.GetInt32(0);
            }
            Console.ReadLine();
            reader.Close();
            command.Dispose();
            return valueString[0];
        }
        // Création, suppression et mise à jour des boutiques

        #region Gestion Client boutique :
        public static void CreationNouveauClient_b(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez créer une boutique");

            Console.WriteLine("Quel est le nom de la boutique ? ");
            string boutique_nom = Console.ReadLine();
            Console.WriteLine("Quel est l'adresse de la boutique ? ");
            string boutique_adresse = Console.ReadLine();
            Console.WriteLine("Quel est le telephone de la boutique ? ");
            string boutique_tel = Console.ReadLine();
            Console.WriteLine("Quel est le courriel de la boutique ? ");
            string boutique_courriel = Console.ReadLine();
            Console.WriteLine("Qui est le contact ? ");
            string boutique_contact = Console.ReadLine();
            Console.WriteLine("Quelle est la remise ? ");
            string boutique_remis = Console.ReadLine();
            Console.WriteLine("Quel est le volume total ? ");
            int volumeTot = Convert.ToInt32(Console.ReadLine());

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
             $" INSERT INTO Client_boutique(boutique_nom,boutique_adresse,boutique_tel,boutique_courriel,boutique_contact,boutique_remise,volumeTot) " +
             $"VALUES (\"{boutique_nom}\",\"{boutique_adresse}\",\"{boutique_tel}\",\"{boutique_courriel}\",\"{boutique_contact}\",\"{boutique_remis}\",{volumeTot});";

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
            command.Dispose();
        }

        public static void SuppresionClient_b(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez supprimer une boutique");

            Console.WriteLine("Entrez le nom de la boutique");
            string attribut = Console.ReadLine();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $" DELETE FROM Client_boutique WHERE boutique_nom = \"{attribut}\";";
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

            command.CommandText = $" DELETE FROM Commande WHERE boutique_nom =\"{attribut}\";";
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

        public static void MajClient_b(MySqlConnection connection)
        {
            Console.WriteLine("Que voulez-vous modifier ? Ecrivez : boutique_nom, boutique_adresse, boutique_tel, boutique_courriel, boutique_contact, boutique_remise ou volumeTot");
            string attribut = Console.ReadLine();
            int volumeTot = -1;
            string mod = "";

            Console.WriteLine("Entrez le nom de la boutique à modifier : ");
            string lieu = Console.ReadLine();
            Console.WriteLine("Quelle est la modification ? ");

            if (attribut == "volumeTot")
            {
                volumeTot = Convert.ToInt32(Console.ReadLine());
            }
            else { mod = Console.ReadLine(); }

            MySqlCommand command = connection.CreateCommand();
            if (volumeTot != -1)
            {
                command.CommandText = $" UPDATE Boutique SET {attribut}={volumeTot} WHERE boutique_nom = {lieu};";
            }
            else
            {
                command.CommandText = $" UPDATE Boutique SET {attribut}=\"{mod}\" WHERE boutique_nom = {lieu};";
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
    }
}
