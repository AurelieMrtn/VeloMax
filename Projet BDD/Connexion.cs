using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;

namespace Projet_BDD__Aurélie_Elisa
{
    class Connexion
    {
        public MySqlConnection connexion;

        public Connexion()
        {
            connexion = null;
            try
            {
                string StringAurelie = "SERVER = localhost; PORT = 3306; DATABASE = velomax; UID = user; PASSWORD = User123oui**";
                string StringElisa = "SERVER = localhost; PORT = 3306; DATABASE = Velomax; UID = root; PASSWORD = LiLi1312!!;";

                // Permet de tester sur nos deux bases de données
                // Attention, bien penser à changer la ligne suivante en fonction de la personne ------------------------------------------------------------------!!!
                string connexionString = StringAurelie;

                connexion = new MySqlConnection(connexionString);
                connexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }
        }

        public MySqlConnection Co
        {
            get { return connexion; }
        }

        // Méthodes Générales
        #region Méthodes de lecture des données
        public List<Boutique> SelectBoutique(string requete, MySqlParameter[] param)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = requete;
            for (int i = 0; i < param.Length; i++)
            {
                command.Parameters.Add(param[i]);
            }

            MySqlDataReader reader = command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            List<Boutique> res = new List<Boutique>();
            while (reader.Read())
            {
                res.Add(new Boutique(
                    reader.GetValue(0).ToString(),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(),
                    reader.GetValue(3).ToString(),
                    reader.GetValue(4).ToString(),
                    reader.GetValue(5).ToString(),
                    reader.GetInt32(6)
                    ));
            }
            reader.Close();
            return res;
        }

        #region Statistiques
        public static void StatMembre_Programme(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT programme_num,individu_nom,individu_prenom"
                                + " FROM Client_individu"
                                + " GROUP BY programme_num";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            Console.WriteLine("numprog,   nom ,        prenom");
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
            command.Dispose();
        }

        public static void Stat_DateExp(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT individu_nom,individu_prenom,individu_date,programme_duree"
                                + " FROM Client_individu"
                                + " NATURAL JOIN Programme ";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            int[] valueInt = new int[reader.FieldCount];
            string[] valueString = new string[reader.FieldCount];
            Console.WriteLine("date_expiration");

            while (reader.Read())
            {
                valueString[0] = reader.GetValue(0).ToString();
                valueString[1] = reader.GetValue(1).ToString();
                valueInt[0] = reader.GetInt32(2);
                valueInt[1] = reader.GetInt32(3);
                Console.Write(valueString[0] + valueString[1] + " 01-01-" + (valueInt[0] + valueInt[1]));
                Console.WriteLine();
            }
            Console.ReadLine();
            reader.Close();
            command.Dispose();
        }
        #endregion

        //EXPORT
        #region Export
        public void ExportJson()
        {
            string exportJson = "Bicyclette.json";

            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM Bicyclette;";
            List<Modele> liste_Bicylette = SelectModele(requete, param);

            StreamWriter fileWriter = new StreamWriter(exportJson);
            JsonTextWriter jsonWriter = new JsonTextWriter(fileWriter);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, liste_Bicylette);

            jsonWriter.Close();
            fileWriter.Close();
        }
        public void ExportXml()
        {
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM Bicyclette;";
            List<Modele> liste_Bicylette = SelectModele(requete, param);

            XmlSerializer xs = new XmlSerializer(typeof(List<Modele>));
            StreamWriter wr = new StreamWriter("Bicyclette.xml");
            xs.Serialize(wr, liste_Bicylette);
            wr.Close();
        }
        #endregion
        public List<Individu> SelectIndividu(string requete, MySqlParameter[] param)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = requete;
            for (int i = 0; i < param.Length; i++)
            {
                command.Parameters.Add(param[i]);
            }

            MySqlDataReader reader = command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            List<Individu> res = new List<Individu>();
            while (reader.Read())
            {
                res.Add(new Individu(
                    reader.GetValue(0).ToString(),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(),
                    reader.GetValue(3).ToString(),
                    reader.GetValue(4).ToString(),
                    reader.GetValue(5).ToString(),
                    reader.GetInt32(6)
                    ));
            }
            reader.Close();
            return res;
        }

        public List<Commande> SelectCommande(string requete, MySqlParameter[] param)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = requete;
            for (int i = 0; i < param.Length; i++)
            {
                command.Parameters.Add(param[i]);
            }

            MySqlDataReader reader = command.ExecuteReader();
            List<Commande> res = new List<Commande>();
            while (reader.Read())
            {
                string boutique_nom;
                string individu_nom;
                int modele_num;
                string piece_num;

                if (reader.GetValue(4).ToString() == "") { boutique_nom = null; }
                else { boutique_nom = reader.GetValue(4).ToString(); }

                if (reader.GetValue(5).ToString() == "") { individu_nom = null; }
                else { individu_nom = reader.GetValue(5).ToString(); }

                if (reader.GetValue(6).ToString() == "") { modele_num = -1; }
                else { modele_num = reader.GetInt32(6); }

                if (reader.GetValue(7).ToString() == "") { piece_num = null; }
                else { piece_num = reader.GetValue(7).ToString(); }

                res.Add(new Commande(
                    reader.GetInt32(0),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(),
                    reader.GetInt32(3),
                    boutique_nom,
                    individu_nom,
                    modele_num,
                    piece_num
                    ));
            }
            reader.Close();
            return res;
        }

        public List<Piece> SelectPiece(string requete, MySqlParameter[] param)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = requete;
            for (int i = 0; i < param.Length; i++)
            {
                command.Parameters.Add(param[i]);
            }

            MySqlDataReader reader = command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            List<Piece> res = new List<Piece>();
            while (reader.Read())
            {
                res.Add(new Piece(
                    reader.GetValue(0).ToString(),
                    reader.GetValue(1).ToString(),
                    reader.GetInt32(2)
                    ));
            }
            reader.Close();
            return res;
        }

        public List<ProgrammeFidelite> SelectProgrammeFidelite(string requete, MySqlParameter[] param)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = requete;
            for (int i = 0; i < param.Length; i++)
            {
                command.Parameters.Add(param[i]);
            }

            MySqlDataReader reader = command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            List<ProgrammeFidelite> res = new List<ProgrammeFidelite>();
            while (reader.Read())
            {
                res.Add(new ProgrammeFidelite(
                    reader.GetInt32(0),
                    reader.GetValue(1).ToString(),
                    reader.GetDouble(2),
                    reader.GetValue(3).ToString(),
                    reader.GetValue(4).ToString()
                    ));
            }
            reader.Close();
            return res;
        }

        public List<Fourniture> SelectFourniture(string requete, MySqlParameter[] param)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = requete;
            for (int i = 0; i < param.Length; i++)
            {
                command.Parameters.Add(param[i]);
            }

            MySqlDataReader reader = command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            List<Fourniture> res = new List<Fourniture>();
            while (reader.Read())
            {
                res.Add(new Fourniture(
                    reader.GetValue(0).ToString(),
                    reader.GetValue(1).ToString(),
                    reader.GetDouble(2),
                    reader.GetValue(3).ToString(),
                    reader.GetValue(4).ToString(),
                    reader.GetValue(5).ToString(),
                    reader.GetValue(6).ToString()
                    ));
            }
            reader.Close();
            return res;
        }

        public List<Fournisseur> SelectFournisseur(string requete, MySqlParameter[] param)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = requete;
            for (int i = 0; i < param.Length; i++)
            {
                command.Parameters.Add(param[i]);
            }

            MySqlDataReader reader = command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            List<Fournisseur> res = new List<Fournisseur>();
            while (reader.Read())
            {
                res.Add(new Fournisseur(
                    reader.GetInt32(0),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(),
                    reader.GetInt32(3).ToString()
                    ));
            }
            reader.Close();
            return res;
        }

        public List<Modele> SelectModele(string requete, MySqlParameter[] param)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = requete;
            for (int i = 0; i < param.Length; i++)
            {
                command.Parameters.Add(param[i]);
            }

            MySqlDataReader reader = command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            List<Modele> res = new List<Modele>();
            while (reader.Read())
            {
                res.Add(new Modele(
                    reader.GetInt32(0),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(),
                    reader.GetDouble(3),
                    reader.GetValue(4).ToString(),
                    reader.GetValue(5).ToString(),
                    reader.GetValue(6).ToString(),
                    reader.GetInt32(7)
                    ));
            }
            reader.Close();
            return res;
        }

        public List<Assemblage> SelectAssemblage(string requete, MySqlParameter[] param)
        {
            MySqlCommand command = connexion.CreateCommand();
            command.CommandText = requete;
            for (int i = 0; i < param.Length; i++)
            {
                command.Parameters.Add(param[i]);
            }

            MySqlDataReader reader = command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            List<Assemblage> res = new List<Assemblage>();
            while (reader.Read())
            {
                res.Add(new Assemblage(
                    reader.GetInt32(0),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(),
                    reader.GetValue(3).ToString(),
                    reader.GetValue(4).ToString(),
                    reader.GetValue(5).ToString(),
                    reader.GetValue(6).ToString(),
                    reader.GetValue(7).ToString(),
                    reader.GetValue(8).ToString(),
                    reader.GetValue(9).ToString(),
                    reader.GetValue(10).ToString(),
                    reader.GetValue(11).ToString(),
                    reader.GetValue(12).ToString()
                    ));
            }
            reader.Close();
            return res;
        }
        #endregion

        public void Affichage(List<List<string>> tableau, List<string> nom_colonne)
        {
            for (int i = 0; i < tableau.Count; i++)
            {
                if (i == 0)
                {
                    for (int k = 0; k < nom_colonne.Count; k++)
                    {
                        while (nom_colonne[k].Length < 20)
                        {
                            nom_colonne[k] += " ";
                        }
                        Console.Write(nom_colonne[k]);
                        if (k < nom_colonne.Count - 1) { Console.Write(" | "); }
                        else
                        {
                            Console.WriteLine("\n");
                        }
                    }
                }

                for (int j = 0; j < tableau[0].Count; j++)
                {
                    while (tableau[i][j].Length < 20)
                    {
                        tableau[i][j] += " ";
                    }

                    Console.Write(tableau[i][j]);
                    if (j < tableau[0].Count - 1) { Console.Write(" | "); }
                    else
                    {
                        Console.WriteLine("");
                    }
                }
            }
        }
        public List<List<string>> Affichage_Piece_Quantite()
        {
            string requete1 = "SELECT piece_description, piece_num, piece_prix, piece_quantiteDispo FROM fourniture NATURAL JOIN piece WHERE quantite <= 2;";

            MySqlCommand Affichage_Piece_Quantite = connexion.CreateCommand();
            Affichage_Piece_Quantite.CommandText = requete1;
            MySqlDataReader reader = Affichage_Piece_Quantite.ExecuteReader();
            List<List<string>> resultat = new List<List<string>>();

            while (reader.Read())
            {
                resultat.Add(new List<string> { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), Convert.ToString(Math.Round(reader.GetDouble(2), 2)), reader.GetValue(3).ToString() });
            }
            reader.Close();
            Affichage_Piece_Quantite.Dispose();

            return resultat;
        }

        public void ExportXmlStock()
        {
            MySqlParameter[] param = new MySqlParameter[0];
            string requete1 = "SELECT piece_description, piece_numProduitF, piece_prix, quantite ";
            requete1 += "FROM Fourniture NATURAL JOIN piece ";
            requete1 += "WHERE quantite <= 2; ";

            MySqlCommand Affichage_Piece_Quantite = connexion.CreateCommand();
            Affichage_Piece_Quantite.CommandText = requete1;
            MySqlDataReader reader = Affichage_Piece_Quantite.ExecuteReader();
            List<List<string>> resultat = new List<List<string>>();


            XmlDocument docXml = new XmlDocument();

            XmlElement racine = docXml.CreateElement("Array");
            docXml.AppendChild(racine);

            XmlDeclaration xmldecl = docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.InsertBefore(xmldecl, racine);

            while (reader.Read())
            {
                XmlElement piece = docXml.CreateElement("Piece");
                piece.InnerText = "Piece";
                racine.AppendChild(piece);

                XmlElement description = docXml.CreateElement("piece_description");
                description.InnerText = reader.GetValue(0).ToString();
                piece.AppendChild(description);

                XmlElement no_PieceF = docXml.CreateElement("piece_numProduitF");
                no_PieceF.InnerText = reader.GetValue(1).ToString();
                piece.AppendChild(no_PieceF);

                XmlElement prix_piece = docXml.CreateElement("piece_prix");
                prix_piece.InnerText = Convert.ToString(Math.Round(reader.GetDouble(2), 2));
                piece.AppendChild(prix_piece);

                XmlElement quantite = docXml.CreateElement("quantite");
                quantite.InnerText = reader.GetValue(3).ToString();
                piece.AppendChild(quantite);
            }
            reader.Close();
            Affichage_Piece_Quantite.Dispose();
            docXml.Save("Stock_faible.xml");
            Console.WriteLine("Le fichier a été exporté");
        }

        public static void CumulI(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT Client_Individu.individu_nom,sum(quantite*modele_prix)+sum(quantite*piece_prix)"
                                + " FROM Client_Individu"
                                + " JOIN Commande"
                                + " JOIN Fourniture"
                                + " JOIN  Modele"
                                + " GROUP BY Client_Individu.individu_nom";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            Console.WriteLine(" Client_nom, cumul_prix");
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
            command.Dispose();
        }

        public static void CumulB(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = " SELECT Client_Boutique.boutique_nom,sum(quantite*modele_prix)+sum(quantite*piece_prix)"
                                + " FROM Client_Boutique"
                                + " JOIN Commande"
                                + " JOIN Fourniture"
                                + " JOIN  Modele"
                                + " GROUP BY Client_Boutique.boutique_nom";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            Console.WriteLine(" Client_boutique,    cumul_prix");
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
            command.Dispose();
        }
    }
}