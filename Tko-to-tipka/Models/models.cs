using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace TkoToTipka.Models
{
    public class database
    {

        public const string dbSourcePath = "C:/Users/Flige/Documents/Visual Studio 2013/Projects/Tko-to-tipka/Tko-to-tipka/";


        public static void CreateDatabase(string naziv)
        {
            SQLiteConnection.CreateFile(dbSourcePath + naziv + ".sqlite");
        }


        private static SQLiteConnection connectToDatabase(string database)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + dbSourcePath + database + ".sqlite;Version=3;");
            m_dbConnection.Open();
            return m_dbConnection;
        }


        private static void closeDatabase(SQLiteConnection m_dbConnection)
        {
            m_dbConnection.Dispose();
            m_dbConnection.Close();
        }


        public static void CreateTable(string database, string tableName) 
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);

            string sql = "CREATE TABLE " + tableName + " (username VARCHAR(20), redniBrojZapisa INT, A REAL, B REAL, C REAL, D REAL, E REAL, F REAL, G REAL, H REAL, I REAL, J REAL, K REAL, L REAL, M REAL, N REAL, O REAL, P REAL, Q REAL, R REAL, S REAL, T REAL, U REAL, V REAL, W REAL, X REAL, Y REAL, Z REAL, timeBetweenChar REAL, TimeTyping REAL)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            closeDatabase(m_dbConnection);
        }


        public static void insertToDatabase(string database, string tableName, string username, int redniBrojZapisa, string a, string b, string c, string d, string e, string f, string g, string h, string i, string j, string k, string l, string m, string n, string o, string p, string q, string r, string s, string t, string u, string v, string w, string x, string y, string z, string timeBetweenChar, string timeTyping)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);

            string sql = "insert into "+tableName+" values ('"+username+"',"+redniBrojZapisa+"," + a + "," + b + "," + c + "," + d + ", " + e + ", " + f + ", " + g + ", " + h + ", " + i + ", " + j + ", " + k + ", " + l + ", " + m + ", " + n + ", " + o + ", " + p + ", " + q + ", " + r + ", " + s + ", " + t + ", " + u + ", " + v + ", " + w + ", " + x + ", " + y + ", " + z + ", "+timeBetweenChar+","+timeTyping+")";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            closeDatabase(m_dbConnection);
        }


        public static float selectAVGfromTable(string database, string tableName, string username, string atribut)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);

            string sql = "select AVG("+atribut+") as prosjekAtributa from " + tableName + " where username='" + username + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            string time = "prosjekAtributa:" + reader["prosjekAtributa"];
            time = time.Substring(16, time.Length - 16);
            float rez = float.Parse(time);
            closeDatabase(m_dbConnection);
            return rez;
        }

       
        //postoji li osoba ciji je username xxx u bazi?
        public static bool provjera(string database, string tableName, string username)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);

            string sql = "select distinct username from "+tableName+" where username='"+username+"'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            string postoji = "username"+reader["username"];
            closeDatabase(m_dbConnection);
            return (postoji == "username") ? false : true;
        }


        public static int brojZapisaOsobe(string database, string tableName, string username)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);

            string sql = "select COUNT(username) as brojUnesenihRedaka from " + tableName + " where username='" + username + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            string pomBroj = "brojUnesenihRedaka:" + reader["brojUnesenihRedaka"];
            pomBroj = pomBroj.Substring(19, pomBroj.Length - 19);
            int broj = int.Parse(pomBroj);
            closeDatabase(m_dbConnection);
            return broj;
        }


        public static void smanjiRedniBrojZapisa(string database, string tableName, string username)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);
            SQLiteCommand command; 
            string sql;

            for (int i = 0; i < 2; i++)
            {
                sql = "update " + tableName + " set redniBrojZapisa=redniBrojZapisa-1 where redniBrojZapisa=" + i.ToString() + " and username='" + username + "'";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }
            closeDatabase(m_dbConnection);
        }


        public static void izbrisiRedak(string database, string tableName, string username, int redniBroj)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);

            string sql = "delete from " + tableName + " where username='" + username + "' and redniBrojZapisa="+redniBroj.ToString()+"";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            closeDatabase(m_dbConnection);
        }
    }

}