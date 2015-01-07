using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TkoToTipka.Models;
using Tko_to_tipka.Controllers;

namespace Tko_to_tipka.Controllers
{

    public class User
    {

        public User(double[] userStatistic, String username)
        {
            this.userStatistic = userStatistic;
            this.username = username;
        }

        public double[] userStatistic { get; set; }
        public String username { get; set; }
    }

    public class Unos
    {
        public string user { get; set; }
        public List<string> key_down = new List<string>();
        public List<long> time_down = new List<long>();
        public List<string> key_up = new List<string>();
        public List<long> time_up = new List<long>();

    }

    public class ParseDbData
    {

        public static int DohvatiBrojZapisaOsobe(string user)
        {
            int broj = database.brojZapisaOsobe("database", "user", user);

            return broj;

        }

        public static List<string> DohvatiImenaSvihUsera()
        {
            string usernamesPom = database.selectAllUsernames("database", "user"); //dohvati sve usernames u bazi
            List<string> usernames = usernamesPom.Split(' ').ToList();
            return usernames;
        }

        //iz unosa kojeg smo izvukli iz baze nadji sve brojčane vrijednosti atributa (time_up, time_down, key_up....) i stavi ih u string
        public static string DohvatiListuAtributa(string ulaz, string atribut)
        {
            string str = "";

            for (int i = ulaz.IndexOf(atribut) + atribut.Length + 3; i < ulaz.Length && i != -1 + atribut.Length + 3; i = ulaz.IndexOf(atribut) + atribut.Length + 3)
            {
                for (int j = i; j < ulaz.Length; j++)
                {
                    if (ulaz[j - 1] == 'n')
                        str += 'n';
                    if (ulaz[j] == '"' || ulaz[j] == ',' || ulaz[j] == '}')
                        break;
                    else
                    {
                        if (ulaz[j] == ' ')
                            str += "\\_";
                        else
                            str += ulaz[j];
                    }
                }
                str += " ";
                ulaz = ulaz.Substring(i + 1, ulaz.Length - i - 1);
            }

            str = str.Substring(0, str.Length - 1);
            return str;
        }

        // u listu objekta unos stavljamo sve vrijednosti za tu listu u ulazu u tu listu
        public static Unos DodajUListu(string lista, string ulaz, Unos unos)
        {
            List<string> pom = DohvatiListuAtributa(ulaz, lista).Split(' ').ToList();

            foreach (string str in pom)
            {
                switch (lista)
                {
                    case "key_down":
                        unos.key_down.Add(str);
                        break;
                    case "time_down":
                        if (str == "null")
                            unos.time_down.Add(0);
                        else
                            unos.time_down.Add(Convert.ToInt64(str));
                        break;
                    case "key_up":
                        unos.key_up.Add(str);
                        break;
                    case "time_up":
                        if (str == "null")
                            unos.time_up.Add(0);
                        else
                            unos.time_up.Add(Convert.ToInt64(str));
                        break;
                }

            }

            return unos;
        }

        //Parsiramo podatke i stavljamo ih u odgovarajuće liste objekta
        public static Unos Parsiraj(string ulaz, string user, Unos unos)
        {
            unos.user = user;
            unos = DodajUListu("key_down", ulaz, unos);
            unos = DodajUListu("time_down", ulaz, unos);
            unos = DodajUListu("key_up", ulaz, unos);
            unos = DodajUListu("time_up", ulaz, unos);
            return unos;
        }

        public static List<Unos> DohvatiListuUnosaUsera(List<Unos> listaUnosa, string user)  // u listu unosa spremi sve podatke nekog usera iz baze za svaki unos
        {
            int brojZapisaOsobe = DohvatiBrojZapisaOsobe(user);

            for (int i = 0; i < brojZapisaOsobe; i++)
            {
                Unos unos = new Unos();
                listaUnosa.Add(Parsiraj(database.selectTEXTfromTable("database", "user", user, i), user, unos));
            }

            return listaUnosa;
        }

        // funkcija vraća ukupan broj CapsLockova tijekom jednog unosa
        public static int brojCapsLock(Unos unos)
        {
            int broj = 0;

            foreach (string key in unos.key_down)
            {
                if (key == "CapsLock")
                    broj++;
            }

            return broj;
        }

        public static int brojShift(Unos unos)
        {
            int broj = 0;

            foreach (string key in unos.key_down)
            {
                if (key == "Shift")
                    broj++;
            }

            return broj;
        }

        // funkcija racuna ukupno vrijeme tipkanja nekog unesenog teksta spremljenog u bazu
        public static double time(Unos unos)
        {
            int index = unos.time_up.Count - 1; // indeks zadnjeg time_up u listi

            long t = unos.time_up[index] - unos.time_down[0];

            return Convert.ToDouble(t);
        }

        public static List<User> DohvatiSveUsere() // dohvaća sve usere (podatke za svaki unos nekog usera i sprema ih u listu objekata user)
        {
            List<User> users = new List<User>();

            List<string> AllUsers = DohvatiImenaSvihUsera();
            List<Unos> listaUnosa = new List<Unos>();

            foreach (string username in AllUsers)
            {
                List<Unos> pom = new List<Unos>();
                pom = DohvatiListuUnosaUsera(pom, username);
                listaUnosa.AddRange(pom);
            }

            //za svaki unos iz baze izgradi objekt user u kojem spremamo sve podatke potrebne objektu user
            foreach (Unos unos in listaUnosa)
            {
                double[] userStatistic = new double[3];
                String kosrsnikIme = unos.user;
                userStatistic[0] = Convert.ToDouble(brojCapsLock(unos));
                //userStatistic[1] = Convert.ToDouble(brojShift(unos));
                userStatistic[1] = time(unos);
                User user = new User(userStatistic, kosrsnikIme);
                users.Add(user);
            }
            return users;
        }

        public static double[] parseQuery(string ulaz)
        {
            List<Unos> unos = new List<Unos>();
            Unos unos1 = new Unos();
            unos1 = Parsiraj(ulaz, "", unos1);

            double[] userStatistic = new double[3];
            userStatistic[0] = Convert.ToDouble(brojCapsLock(unos1));
            //userStatistic[1] = Convert.ToDouble(brojShift(unos1));
            userStatistic[1] = time(unos1);

            return userStatistic;
        }

    }
}