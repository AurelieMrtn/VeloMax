using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Projet_BDD__Aurélie_Elisa
{
    class Modele
    {
        Connexion conec;
        public int modele_num { get; }
        public string modele_nom { get; set; }
        public string grandeur { get; set; }
        public double modele_prix { get; set; }
        public string modele_date_intro { get; set; }
        public string modele_dateDiscon { get; set; }
        public string ligne_produit { get; set; }
        public int assemblage_num { get; set; }

        public Modele(int modele_num, string modele_nom, string grandeur, double modele_prix, string modele_date_intro, string modele_dateDiscon, string ligne_produit, int assemblage_num)
        {
            this.conec = new Connexion();
            this.modele_num = modele_num;
            this.modele_nom = modele_nom;
            this.grandeur = grandeur;
            this.modele_prix = modele_prix;
            this.modele_date_intro = modele_date_intro;
            this.modele_dateDiscon = modele_dateDiscon;
            this.ligne_produit = ligne_produit;
            this.assemblage_num = assemblage_num;
            conec.Co.Close();
        }

        public Modele()
        {
            this.conec = new Connexion();
            conec.Co.Close();
        }

        //Affichage
        public string toString()
        {
            string res = modele_num + " | " + modele_nom + ", grandeur: " + grandeur + ", prix: " +  modele_prix + ", date d'introduction: " +  
                modele_date_intro + ", date de discontinuation: " + modele_dateDiscon + ", ligne produit: " + ligne_produit + ", numero d'assemblage: " + assemblage_num;
            return res;
        }

        public List<Modele> ListeModele()
        {
            conec.Co.Open();
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM Modele;";
            List<Modele> listeMo = conec.SelectModele(requete, param);
            for (int i = 0; i < listeMo.Count; i++)
            {
                Console.WriteLine(listeMo[i].toString());
            }
            conec.Co.Close();
            return listeMo;
        }

        // Création, suppression et mise à jour des modeles

        #region Gestion des Modeles :
        public static void CreationModele(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez créer un modele");
            Console.WriteLine("Quel est le numero du modele ? ");
            int modele_num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Quel est le nom du modele ? ");
            string modele_nom = Console.ReadLine();
            Console.WriteLine("Quelle est la grandeur? ");
            string grandeur = Console.ReadLine();
            Console.WriteLine("Quel est le prix du modele ? ");
            double modele_prix = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Quelle est la date d'introduction ? ");
            string modele_date_intro = Console.ReadLine();
            Console.WriteLine("Quel est la date de discontinuation ? ");
            string modele_dateDiscon = Console.ReadLine();
            Console.WriteLine("Quelle est la ligne de produit ? ");
            string ligne_produit = Console.ReadLine();
            Console.WriteLine("Quel est le numero d'assemblage ? ");
            int assemblage_num = Convert.ToInt32(Console.ReadLine());

            MySqlCommand command = connection.CreateCommand();
            command.CommandText =
             $"INSERT INTO Modele(modele_num,modele_nom,grandeur,modele_prix,modele_date_intro,modele_dateDiscon,ligne_produit,assemblage_num) " +
             $"VALUES ({modele_num},\"{modele_num}\",\"{grandeur}\" , {modele_prix} , \"{modele_date_intro}\" , \"{modele_dateDiscon}\" , \"{ligne_produit}\" , {assemblage_num});";

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

            Console.WriteLine("Il faut maintenant renseigner l'assemblage de ce modele");
            Console.WriteLine();
            Console.WriteLine("Quel est le modele de Cadre ? ");
            string Cadre = Console.ReadLine();
            Console.WriteLine("Quel est le modele de Guidon? ");
            string Guidon = Console.ReadLine();
            Console.WriteLine("Quel est le modele de Freins ? ");
            string Freins = Console.ReadLine();
            Console.WriteLine("Quel est le modele de selle ? ");
            string Selle = Console.ReadLine();
            Console.WriteLine("Quel est le modele du Derailleur avant ? ");
            string Derailleur_avant = Console.ReadLine();
            Console.WriteLine("Quel est le modele du Derailleur arriere ? ");
            string Derailleur_arriere = Console.ReadLine();
            Console.WriteLine("Quel est le modele de Roue avant ? ");
            string Roue_avant = Console.ReadLine();
            Console.WriteLine("Quel est le modele de Roue arriere ? ");
            string Roue_arriere = Console.ReadLine();
            Console.WriteLine("Quel est le modele du Reflecteur ? ");
            string Reflecteur = Console.ReadLine();
            Console.WriteLine("Quel est le modele du Pedalier ? ");
            string Pedalier = Console.ReadLine();
            Console.WriteLine("Quel est le modele de l'ordinateur ? ");
            string Ordinateur = Console.ReadLine();
            Console.WriteLine("Quel est le modele du Panier ? ");
            string Panier = Console.ReadLine();

            command.CommandText =
             $"INSERT INTO Assemblage(assemblage_num,Cadre,Guidon,Freins,Selle,Derailleur_avant,Derailleur_arriere,Roue_avant,Roue_arriere,Reflecteur,Pedalier,Ordinateur,Panier) " +
             $"VALUES ({assemblage_num},\"{Cadre}\",\"{Guidon}\",\"{Freins}\",\"{Selle}\",\"{Derailleur_avant}\",\"{Derailleur_arriere}\",\"{Roue_avant}\",\"{Roue_arriere}\",\"{Reflecteur}\",\"{Pedalier}\",\"{Ordinateur}\",\"{Panier}\");";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Creation ffectuée");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" Erreur de connexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }

        public static void SuppresionModele(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez supprimer un modele");
            Console.WriteLine("Quel est le numero du modele ?");
            int mod = Convert.ToInt32(Console.ReadLine());

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $" DELETE FROM Modele WHERE modele_num ={mod};";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Suppression effectuée");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }

            command.CommandText = $"SELECT assemblage_num FROM modele WHERE modele_num = {mod};";
            MySqlDataReader reader = command.ExecuteReader();
            int assemblage = reader.GetInt32(0); ;

            command.CommandText = $"DELETE FROM Assemblage WHERE assemblage_num ={assemblage};";

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Suppression effectuée");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }

            command.CommandText = $" DELETE FROM Commande WHERE modele_num ={mod};";

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

        public static void MajModele(MySqlConnection connection)
        {
            Console.WriteLine("Vous allez mdifier un modele");
            Console.WriteLine("Que voulez-vous modifier ? Ecrivez :modele_num, modele_nom, grandeur, modele_prix, modele_date_intro, modele_dateDiscon, ligne_produit ou assemblage_num ");
            string attribut = Console.ReadLine();
            int siret = -1;
            double prix = -1;
            string mod = "";

            Console.WriteLine("Entrez le numero du modele à modifier : ");
            int lieu = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Quelle est la modification ? ");

            if (attribut == "modele_num" || attribut == "assemblage_num")
            {
                siret = Convert.ToInt32(Console.ReadLine());
            }
            if (attribut == "modele_prix")
            {
                prix = Convert.ToDouble(Console.ReadLine());
            }
            else { mod = Console.ReadLine(); }

            MySqlCommand command = connection.CreateCommand();

            if (siret != -1)
            {
                command.CommandText = $" UPDATE Modele SET \"{attribut}\"= {siret} WHERE modele_num = {lieu};";
            }
            if (prix != -1)
            {
                command.CommandText = $" UPDATE Modele SET \"{attribut}\"= {prix} WHERE modele_num = {lieu};";
            }
            else
            {
                command.CommandText = $" UPDATE Modele SET \"{attribut}\"=\"{mod}\" WHERE modele_num = {lieu};";
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

            if (attribut == "modele_num")
            {
                command.CommandText = $" UPDATE Commande SET modele_num = {siret} WHERE modele_num = {lieu};";
            }

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Maj effectué");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }
        #endregion
    }
}
