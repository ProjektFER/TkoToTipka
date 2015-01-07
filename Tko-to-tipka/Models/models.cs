using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace TkoToTipka.Models
{
    public class database
    {

        //public const string dbSourcePath = "D:/FER/Projekt/GitHub/TkoToTipka/Tko-to-tipka/App_Data/";
        public const string dbSourcePath = "D:/FER/Projekt/GitHub/TkoToTipka/Tko-to-tipka/App_Data/";

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

            string sql = "CREATE TABLE " + tableName + " (username VARCHAR(20), redniBrojZapisa INT, txt TEXT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            closeDatabase(m_dbConnection);
        }


        public static void insertToDatabase(string database, string tableName, string username, int redniBrojZapisa, string txt)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);

            string sql = "insert into "+tableName+" values ('" + username + "'," + redniBrojZapisa.ToString() + ",'" + txt + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            closeDatabase(m_dbConnection);
        }


        public static string selectTEXTfromTable(string database, string tableName, string username, int redniBrojZapisa)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);
            string txt = "";

            string sql = "select txt from " + tableName + " where username = '" + username + "' and redniBrojZapisa = " + redniBrojZapisa.ToString() + "";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            txt += reader["txt"];

            closeDatabase(m_dbConnection);

            return txt;
        }

        public static string selectAllUsernames(string database, string tableName)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);
            string str = "";

            string sql = "select distinct username from " + tableName;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
                str += reader["username"] + " ";
            str = str.Substring(0, str.Length - 1);

            closeDatabase(m_dbConnection);

            return str;
        }
       
        //postoji li osoba ciji je username xxx u bazi?
        public static bool provjera(string database, string tableName, string username)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);
            string postoji = "";

            string sql = "select distinct username from "+tableName+" where username='"+username+"'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            reader.Read();
            postoji += reader["username"];

            closeDatabase(m_dbConnection);

            return (postoji == "") ? false : true;
        }


        public static int brojZapisaOsobe(string database, string tableName, string username)
        {
            SQLiteConnection m_dbConnection = connectToDatabase(database);
            string pomBroj = "";

            string sql = "select COUNT(username) as brojUnesenihRedaka from " + tableName + " where username='" + username + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            reader.Read();
            pomBroj += reader["brojUnesenihRedaka"];
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