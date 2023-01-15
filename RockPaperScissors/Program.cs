using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace RockPaperScissors
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\valer\source\repos\AdventOfCode2022\RockPaperScissors\input.txt");

            List<string> resultsPlayer1 = new List<string>();
            List<string> resultsPlayer2 = new List<string>();
            List<string> results = new List<string>();
            int counter = 0;
            int score = 0;
            int finalScore = 0;
            const int R = 1;
            const int P = 2;
            const int S = 3;

            char player1;
            char player2;

            foreach (string line in lines)
            {
                foreach (var item in line.Split(" "))
                {
                    results.Add(item);
                }

            }

            for (int i = 0; i < results.Count(); i = i + 2)
            {
                resultsPlayer1.Add(results[i]);
            }

            for (int i = 1; i < results.Count(); i = i + 2)
            {
                resultsPlayer2.Add(results[i]);
            }

            int arrayLength = resultsPlayer1.Count();

            while (counter < arrayLength)
            {
                if (resultsPlayer1[counter] == "A")
                {
                    switch (resultsPlayer2[counter])
                    {
                        case "X":
                            score = S + 0;
                            break;
                        case "Y":
                            score = R + 3;
                            break;
                        case "Z":
                            score = P + 6;
                            break;

                    }
                }
                if (resultsPlayer1[counter] == "B")
                {
                    switch (resultsPlayer2[counter])
                    {
                        case "X":
                            score = R + 0;
                            break;
                        case "Y":
                            score = P + 3;
                            break;
                        case "Z":
                            score = S + 6;
                            break;

                    }
                }
                if (resultsPlayer1[counter] == "C")
                {
                    switch (resultsPlayer2[counter])
                    {
                        case "X":
                            score = P + 0;
                            break;
                        case "Y":
                            score = S + 3;
                            break;
                        case "Z":
                            score = R + 6;
                            break;

                    }
                }

                finalScore += score;
                counter++;
                Console.WriteLine(counter);
                if (counter > results.Count())
                {
                    break;
                }
            }



            //Console.WriteLine(counter);


            Console.WriteLine(finalScore);
        }

    }
}
