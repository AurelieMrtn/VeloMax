using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Projet_BDD__Aurélie_Elisa
{
    class Fournisseur
    {
        Connexion conec;
        public int siret { get; set; }
        public string fournisseur_nom { get; set; }
        public string fournisseur_contact { get; set; }
        public string libelle { get; set; }

        public Fournisseur(int siret, string fournisseur_nom, string fournisseur_contact, string libelle)
        {
            this.conec = new Connexion();
            this.siret = siret;
            this.fournisseur_nom = fournisseur_nom;
            this.fournisseur_contact = fournisseur_contact;
            this.libelle = libelle;
            conec.Co.Close();
        }
        public Fournisseur()
        {
            this.conec = new Connexion();
            conec.Co.Close();
        }

        //Affichage
        public string toString()
        {
            string res = "siret : " + siret + " | nom_fournisseur: " + fournisseur_nom + ", contact_fournisseur: " + fournisseur_contact + ", libelle: " + libelle;
            return res;
        }

        public List<Fournisseur> ListeFournisseur()
        {
            conec.Co.Open();
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM Fournisseur;";
            List<Fournisseur> listeFor = conec.SelectFournisseur(requete, param);
            for (int i = 0; i < listeFor.Count; i++)
            {
                Console.WriteLine(listeFor[i].toString());
            }
            conec.Co.Close();
            return listeFor;
        }

        // Création, suppression et mise à jour des fournisseurs

        #region Gestion des fournisseurs :

        public static void CreationFournisseur(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez créer un fournisseur");

            Console.WriteLine("Quel est le numéro de siret ? ");
            int siret = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Quelle est le nom du fournisseur ? ");
            string nom = Console.ReadLine();
            Console.WriteLine("Quel est le contact du fournisseur ? ");
            string contact = Console.ReadLine();
            Console.WriteLine("Quelle est l'adresse du fournisseur ? ");
            string adresse = Console.ReadLine();
            Console.WriteLine("Quel est le libelle du fournisseur ? ");
            string libelle = Console.ReadLine();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
             $" INSERT INTO fournisseur (siret,fournisseur_nom,fournisseur_contact,fournisseur_adresse,libelle) " +
             $"VALUES ({siret},\"{nom}\",\"{contact}\",\"{adresse}\",\"{libelle}\");";

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

        public static void SuppressionFournisseur(MySqlConnection connection)
        {
            Console.WriteLine("Entrez le nom du fournisseur");
            string attribut = Console.ReadLine();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $" DELETE FROM Fournisseur WHERE fournisseur_nom =\"{attribut}\";";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Suppression effectuée");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();

            command.CommandText = $" DELETE FROM fourniture WHERE fournisseur_nom =\"{attribut}\";";

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

        public static void MajFournisseur(MySqlConnection connection)
        {
            Console.WriteLine("Que voulez-vous modifier ? Ecrivez : siret, fournisseur_nom, fournisseur_contact, fournisseur_adresse ou libelle");
            string attribut = Console.ReadLine();
            int siret = -1;
            string mod = "";

            Console.WriteLine("Entrez le siret du fournisseur à modifier : ");
            int lieu = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Quelle est la modification ? ");

            if (attribut == "siret")
            {
                siret = Convert.ToInt32(Console.ReadLine());
            }
            else { mod = Console.ReadLine(); }

            MySqlCommand command = connection.CreateCommand();

            if (siret != -1)
            {
                command.CommandText = $" UPDATE Fournisseur SET siret = {siret} WHERE siret = {lieu};";
            }
            else
            {
                command.CommandText = $" UPDATE Fournisseur SET {attribut}=\"{mod}\" WHERE siret = {lieu};";
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

            if (attribut == "fournisseur_nom")
            {
                command.CommandText = $" UPDATE Fourniture SET {attribut}=\"{mod}\";";
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Maj effectuée (Dans la table fourniture)");
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(" Erreur de connexion : " + e.ToString());
                    Console.ReadLine();
                    return;
                }
            }
            command.Dispose();
        }
        #endregion
    }
}
