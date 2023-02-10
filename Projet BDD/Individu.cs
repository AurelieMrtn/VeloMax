using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD__Aurélie_Elisa
{
    class Individu
    {
        Connexion conec;
        public string individu_nom { get; set; }
        public string individu_prenom { get; set; }
        public string individu_adresse { get; set; }
        public string individu_courriel { get; set; }
        public string individu_tel { get; set; }
        public string individu_date { get; set; }
        public int programme_num { get; set; }

        //Constructeurs
        public Individu(string individu_nom, string individu_prenom, string individu_adresse, string individu_courriel, string individu_tel, string individu_date, int programme_num)
        {
            this.conec = new Connexion();
            this.individu_nom = individu_nom;
            this.individu_prenom = individu_prenom;
            this.individu_adresse = individu_adresse;
            this.individu_courriel = individu_courriel;
            this.individu_tel = individu_tel;
            this.individu_date = individu_date;
            this.programme_num = programme_num;
            conec.Co.Close();
        }

        public Individu()
        {
            this.conec = new Connexion();
            conec.Co.Close();
        }

        // Affichage
        public string toString()
        {
            string res = individu_nom + " " + individu_prenom + ", adresse: " + individu_adresse + ", courriel: " + individu_courriel + ", telephone: " + individu_tel +
                ", date de début: " + individu_date + ", numero de programme: " + programme_num;
            return res;
        }
        public List<Individu> ListeIndividus()
        {
            conec.Co.Open();
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM client_individu;";
            List<Individu> listeInd = conec.SelectIndividu(requete, param);
            for (int i = 0; i < listeInd.Count; i++)
            {
                Console.WriteLine(listeInd[i].toString());
            }
            conec.Co.Close();
            return listeInd;
        }

        public static int CompteClientI(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT count(*)"
                                + " FROM Client_Individu";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            Console.WriteLine("Client_individu");
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

        // Création, suppression et mise à jour des individus

        #region Gestion Client Individu :
        public static void CreationNouveauClient_i(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez créer un nouveau client : individu");
            Console.WriteLine("Quel est le nom de l'individu ? ");
            string individu_nom = Console.ReadLine();
            Console.WriteLine("Quel est le prenom de l'individu ? ");
            string individu_prenom = Console.ReadLine();
            Console.WriteLine("Quel est l'adresse de l'individu ? ");
            string individu_adresse = Console.ReadLine();
            Console.WriteLine("Quel est le courriel de l'individu ? ");
            string individu_courriel = Console.ReadLine();
            Console.WriteLine("Quel est le telephone de l'individu ? ");
            string individu_tel = Console.ReadLine();
            Console.WriteLine("Quelle est la date d'adhesion au programme ? Si pas de programme: écrire null");
            string individu_date = Console.ReadLine();
            Console.WriteLine("Quel est le numero du programme de l'individu ? ");
            int programme_num = Convert.ToInt32(Console.ReadLine());

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
             $" INSERT INTO Client_individu(individu_nom,individu_prenom,individu_adresse,individu_courriel,individu_tel,individu_date,programme_num) " +
             $"VALUES (\"{individu_nom}\",\"{individu_prenom}\",\"{individu_adresse}\",\"{individu_courriel}\",\"{individu_tel}\",\"{individu_date}\",{programme_num});";

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

        public static void SuppresionCLient_i(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez supprimer un client individu");
            Console.WriteLine("Quel est la nom du client ?");
            string mod = Console.ReadLine();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $" DELETE FROM Client_individu WHERE individu_nom =\"{mod}\";";

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

            command.CommandText = $" DELETE FROM Commande WHERE individu_nom =\"{mod}\";";

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

        public static void MajClient_i(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez modifier un individu");
            Console.WriteLine("Que voulez-vous modifier ? Ecrivez : individu_nom, individu_prenom, individu_adresse, individu_courriel, individu_tel, individu_date ou programme_num");
            string attribut = Console.ReadLine();
            int programme = -1;
            string mod = "";

            Console.WriteLine("Entrez le nom de l'individu à modifier : ");
            string lieu = Console.ReadLine();

            Console.WriteLine("Quelle est la modification ? ");

            if (attribut == "programme_num")
            {
                programme = Convert.ToInt32(Console.ReadLine());
            }
            else { mod = Console.ReadLine(); }

            MySqlCommand command = connection.CreateCommand();

            if (programme != -1)
            {
                command.CommandText = $" UPDATE Client_individu SET \"{attribut}\"={programme} WHERE individ_nom= \"{lieu}\";";
            }
            else
            {
                command.CommandText = $" UPDATE Client_individu SET \"{attribut}\"=\"{mod}\" WHERE individu_nom = \"{lieu}\";";
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