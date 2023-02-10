using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Projet_BDD__Aurélie_Elisa
{
    class ProgrammeFidelite
    {
        Connexion conec;
        public int programme_num { get; set; }
        public string programme_nom { get; set; }
        public double programme_prix { get; set; }
        public string programme_duree { get; set; }
        public string programme_rabais { get; set; }

        public ProgrammeFidelite(int programme_num, string programme_nom, double programme_prix, string programme_duree, string programme_rabais)
        {
            this.conec = new Connexion();
            this.programme_num = programme_num;
            this.programme_nom = programme_nom;
            this.programme_prix = programme_prix;
            this.programme_duree = programme_duree;
            this.programme_rabais = programme_rabais;
            conec.Co.Close();
        }

        // Affichage

        public string toString()
        {
            string res = programme_num + " | nom du programm: " + programme_nom + ", prix du programme: " + programme_prix + ", duree du programme: " + programme_duree + 
                ", rabais: "+ programme_rabais;
            return res;
        }

        public List<ProgrammeFidelite> ListeProgrammesFidelite()
        {
            conec.Co.Open();
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM Programme;";
            List<ProgrammeFidelite> listePro = conec.SelectProgrammeFidelite(requete, param);
            for (int i = 0; i < listePro.Count; i++)
            {
                Console.WriteLine(listePro[i].toString());
            }
            conec.Co.Close();
            return listePro;
        }
    }
}
