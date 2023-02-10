using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Projet_BDD__Aurélie_Elisa
{
    class Assemblage
    {
        Connexion conec;
        public int assemblage_num { get; }
        public string cadre { get; set; }
        public string guidon { get; set; }
        public string freins { get; set; }
        public string selle { get; set; }
        public string derailleur_avant { get; set; }
        public string derailleur_arriere { get; set; }
        public string roue_avant { get; set; }
        public string roue_arriere { get; set; }
        public string reflecteur { get; set; }
        public string pedalier { get; set; }
        public string ordinateur { get; set; }
        public string panier { get; set; }

        public Assemblage(int assemblage_num, string cadre, string guidon, string freins, string selle, string derailleur_avant, string derailleur_arriere, string roue_avant, string roue_arriere, string reflecteur, string pedalier, string ordinateur, string panier )
        {
            this.conec = new Connexion();
            this.assemblage_num = assemblage_num;
            this.cadre = cadre;
            this.guidon = guidon;
            this.freins = freins;
            this.selle = selle;
            this.derailleur_avant = derailleur_avant;
            this.derailleur_arriere = derailleur_arriere;
            this.roue_avant = roue_avant;
            this.roue_arriere = roue_arriere;
            this.reflecteur = reflecteur;
            this.pedalier = pedalier;
            this.ordinateur = ordinateur;
            this.panier = panier;
            conec.Co.Close();
        }

        //Affichage (On veut pouvoir utiliser cet affichage pour faire des requetes qui lient commande.modele_num et piece_num)
        public string[] toTab()
        {
            string[] res = new string[] { cadre, guidon, freins, selle, derailleur_avant, derailleur_arriere, roue_avant, roue_arriere, reflecteur, pedalier, ordinateur, panier };
            return res;
        }

        public List<Assemblage> ListeAssemblage() //Attention retourne une liste de tableaux
        {
            conec.Co.Open();
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM Assemblage;";
            List<Assemblage> listeAs = conec.SelectAssemblage(requete, param);
            for (int i = 0; i < listeAs.Count; i++)
            {
                Console.WriteLine(listeAs[i].toTab());
            }
            conec.Co.Close();
            return listeAs;
        }

    }
}
