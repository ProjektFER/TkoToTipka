using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tko_to_tipka.Controllers
{
    public class KNearestNeighbour
    {


        private static String findMajorityClass(String[] array)
        {
            HashSet<String> hash = new HashSet<String>(array);
            String[] uniqueValues = hash.ToArray();
            int[] counts = new int[uniqueValues.Length];

            int numberOfElements = array.Distinct().Count();

            for (int i = 0; i < uniqueValues.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j].Equals(uniqueValues[i]))
                    {
                        counts[i]++;
                    }

                }
            }

            int max = counts[0];
            for (int counter = 1; counter < counts.Length; counter++)
            {
                if (counts[counter] > max)
                {
                    max = counts[counter];
                }
            }


            int freq = 0;
            for (int counter = 0; counter < counts.Length; counter++)
            {
                if (counts[counter] == max)
                {
                    freq++;
                }
            }


            int index = -1;

            if (freq == 1)
            {
                for (int counter = 0; counter < counts.Length; counter++)
                {
                    if (counts[counter] == max)
                    {
                        index = counter;
                        break;
                    }
                }
                return uniqueValues[index];
            }
            else
            {
                int[] ix = new int[freq];
                int ixi = 0;
                for (int counter = 0; counter < counts.Length; counter++)
                {
                    if (counts[counter] == max)
                    {
                        ix[ixi] = counter;
                        ixi++;
                    }
                }

                Random generator = new Random();
                int rIndex = generator.Next(ix.Length);
                int nIndex = ix[rIndex];
                return uniqueValues[nIndex];
            }

        }

        public static String Initialize(List<User> userList, double[] query) 
        {
            //number of neighbours
            int k = 5;
            //list to save city data
            //List<User> userList = new List<User>();
            //list to save distance result
            List<Result> resultList = new List<Result>();

            //cityList.Add(new User(instances[0], "Arijana"));
            //cityList.Add(new User(instances[1], "Tomo"));
            //cityList.Add(new User(instances[2], "Jelena"));
            //cityList.Add(new User(instances[3], "Arijana"));
            //cityList.Add(new User(instances[4], "Jelena"));
            //cityList.Add(new User(instances[5], "Tomo"));
            //cityList.Add(new User(instances[6], "Arijana"));
            //cityList.Add(new User(instances[7], "Jelena"));
            //cityList.Add(new User(instances[8], "Tomo"));

            //double[] query = { 0.65, 0.78, 0.21, 0.29, 0.58 };
            //double[] query = { 0.53, 0.17, 0.63, 0.29, 0.72 };

            //calculate distance for each data in data set
            foreach (User user in userList)
            {
                double dist = 0.0;
                for (int i = 0; i < user.userStatistic.Length; i++)
                {
                    dist += Math.Pow(user.userStatistic[i] - query[i], 2);
                }
                double distance = Math.Sqrt(dist);
                resultList.Add(new Result(distance, user.username));
            }

            //sort distances
            resultList.Sort(new DistanceComparator());
            String[] ss = new String[k];

            //find first k neighbours
            for (int x = 0; x < k; x++)
            {
                ss[x] = resultList.ElementAt(x).username;
            }


            String majClass = findMajorityClass(ss);
            return majClass;
        
        }


        //public class User
        //{
        //    public User(double[] userStatistic, String username)
        //    {
        //        this.userStatistic = userStatistic;
        //        this.username = username;
        //    }

        //    public double[] userStatistic { get; set; }
        //    public String username { get; set; }
        //}

        class Result
        {
            public Result(double distance, String username)
            {
                this.distance = distance;
                this.username = username;
            }

            public double distance { get; set; }
            public String username { get; set; }
        }

        class DistanceComparator : Comparer<Result>
        {
            public override int Compare(Result x, Result y)
            {
                return (x.distance < y.distance) ? -1 : (x.distance == y.distance) ? 0 : 1;
            }
        }



    }
}