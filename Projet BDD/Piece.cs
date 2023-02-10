using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD__Aurélie_Elisa
{
    class Piece
    {
        Connexion conec;
        public string piece_num { get; }
        public string piece_description { get; set; }
        public int piece_quantiteDispo { get; set; }


        public Piece(string piece_num, string piece_description, int piece_quantiteDispo)
        {
            this.conec = new Connexion();
            this.piece_num = piece_num;
            this.piece_description = piece_description;
            this.piece_quantiteDispo = piece_quantiteDispo;
            conec.Co.Close();
        }

        // Affichage

        public string toString()
        {
            string res = piece_num + " | description: " + piece_description + ", quantite disponible: " + piece_quantiteDispo;
            return res;
        }

        public List<Piece> ListePieces()
        {
            conec.Co.Open();
            MySqlParameter[] param = new MySqlParameter[0];
            string requete = "SELECT * FROM Piece;";
            List<Piece> listePie = conec.SelectPiece(requete, param);
            for (int i = 0; i < listePie.Count; i++)
            {
                Console.WriteLine(listePie[i].toString());
            }
            conec.Co.Close();
            return listePie;
        }

        // Création, suppression et mise à jour des pièces effectuée via Fourniture

    }
}
