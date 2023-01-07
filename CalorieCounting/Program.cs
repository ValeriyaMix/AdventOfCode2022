using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace CalorieCounting
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\valer\source\repos\AdventOfCode2022\CalorieCounting\input.txt");

            List<int> listOfSums = new List<int>();
            List<int> listOfTopThreeSums = new List<int>();
            int sum = 0;

            foreach (string line in lines)
            {   
                if (line != "")
                {
                    //Console.WriteLine(line);
                    sum += Convert.ToInt32(line);
                    
                }
                else
                {
                    listOfSums.Add(sum);
                    sum = 0;
                }
            }
            listOfSums.Add(sum);


            for (int i =0; i < 3; i++)
            {
                int maxOfSum1 = listOfSums.Max();
                int indexMax1 = listOfSums.FindIndex(a => a == maxOfSum1);
                listOfTopThreeSums.Add(maxOfSum1);
                listOfSums.RemoveAt(indexMax1);

                Console.WriteLine(maxOfSum1);
                Console.WriteLine(indexMax1);
            }

            int maxOfThreeSums = listOfTopThreeSums.Sum();
            Console.WriteLine(maxOfThreeSums);
        }
        
    }
}

