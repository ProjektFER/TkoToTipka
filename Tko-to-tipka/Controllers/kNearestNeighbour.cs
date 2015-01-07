using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tko_to_tipka.Models;

namespace Tko_to_tipka.Controllers
{
    public class KNearestNeighbour
    {

        public static String Recognize(List<User> dataset, double[] query) 
        {
            int k = 5;
            List<Result> neighbors = getNeighbors(dataset, query, k);
            return getRecognizedUser(neighbors);     
        }

        public static List<TestModel> Test(List<User> dataset) 
        {
            int k = 5;
            List<User> trainingSet = new List<User>();
            List<User> testSet = new List<User>();
            List<Result> neighbors = new List<Result>();     
            List<String> predictions = new List<String>();
            String recognizedUser;

            List<TestModel> results = new List<TestModel>();

            loadDataBase(dataset, trainingSet, testSet);
            for (int i = 0; i < testSet.Count(); i++)
            {
                neighbors = getNeighbors(trainingSet, testSet.ElementAt(i).userStatistic, k);
                recognizedUser = getRecognizedUser(neighbors);
                results.Add(new TestModel(testSet.ElementAt(i).username, recognizedUser));
                predictions.Add(recognizedUser);
            }
            //double accuracy = getAccuracy(testSet, predictions);
            //return accuracy;
            return results;
        }
        
        private static void loadDataBase(List<User> userList, List<User> trainingSet, List<User> testSet)
        {
            Random rnd = new Random();
            userList.ForEach(s =>
            {
                if (rnd.NextDouble() <= 0.5)
                    testSet.Add(s);
                else
                    trainingSet.Add(s);

            });
        }

        private static double euclideanDistance(double[] data1, double[] data2) 
        {
            int len = data1.Length;
            double dist = 0;

            for (int i = 0; i < len; i++)
            {
                dist += Math.Pow(data1[i]-data2[i], 2);
            }
            return Math.Sqrt(dist);
        }

        private static List<Result> getNeighbors(List<User> trainingSet, double[] testInstance, int k) 
        {
            List<Result> distances = new List<Result>();
            List<Result> neighbors = new List<Result>();
            int len = trainingSet.Count();
            for (int i = 0; i < len; i++)
            {
                double dist = euclideanDistance(trainingSet[i].userStatistic, testInstance);
                distances.Add(new Result(dist, trainingSet[i].username));
            }
            distances.Sort(new DistanceComparator());
            neighbors = distances.Take(k).ToList();
            return neighbors;

        }

        private static String getRecognizedUser(List<Result> neighbors) 
        {          
            var q = neighbors.GroupBy(x => x.username)
                             .Select(g => new { Value = g.Key, Count = g.Count() })
                             .OrderByDescending(x => x.Count);
            return q.ElementAt(0).Value;
        }

        private static float getAccuracy(List<User> testSet, List<String> predictions) {

            int correct = 0;
            for (int i = 0; i < testSet.Count() ; i++)
            {
                if (testSet.ElementAt(i).username.Equals(predictions.ElementAt(i))) {
                    correct++;                    
                }
            }
            return correct/(float)testSet.Count()*100;
        }

    }

    //class User
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