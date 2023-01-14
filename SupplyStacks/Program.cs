using System;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace SupplyStacks
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\valer\source\repos\AdventOfCode2022\SupplyStacks\input.txt");

            int counter = 0;

            var dictOfCrates = new Dictionary<int, List<string>>();
            int numOfPos = 1;

            List<string> moveList = new List<string>();
            List<string> fromList = new List<string>();
            List<string> toList = new List<string>();


            var rx = new Regex(@"\[[a-zA-Z]]", RegexOptions.Compiled);
            var rxx = new Regex(@"\s[^1-9]", RegexOptions.Compiled);
            var rxxx = new Regex(@"\[1-9]", RegexOptions.Compiled);

            int totalNumOfColumns = (lines[2].Count() - 3) / 4 + 1;

            foreach (string line in lines)
            {
                             
                for (int i = 0; i < line.Length; i = i + 4)
                {
                    if (rx.IsMatch(line.Substring(i, 3))) 
                    {
                        if (dictOfCrates.ContainsKey(numOfPos))
                        {
                            dictOfCrates[numOfPos].Add(line.Substring(i + 1, 1));
                        }
                        else
                        {
                            dictOfCrates.Add(numOfPos, new List<string>());
                            dictOfCrates[numOfPos].Add(line.Substring(i + 1, 1));
                        }
                        
                        if (counter == totalNumOfColumns - 1)
                        {
                            counter = 0;
                            numOfPos = 1;
                        }
                        else
                        {
                            counter++;
                            numOfPos++;
                        }
                        
                    }
                    else if (rxx.IsMatch(line.Substring(i, 3)))
                    {
                        if (counter == totalNumOfColumns - 1)
                        {
                            counter = 0;
                            numOfPos = 1;
                        }
                        else
                        {
                            counter++;
                            numOfPos++;
                        }
                        
                    }
                    
                    else
                    {
                        if (line.Contains("move"))
                        {
                            string[] lineSplit = line.Split(" ");

                            moveList.Add(lineSplit[1]);
                            fromList.Add(lineSplit[3]);
                            toList.Add(lineSplit[5]);

                            break;
                        }
                        
                    }
                }

            }

            //Reversing lists in dictionary

            for (int i = 1; i <= totalNumOfColumns; i++)
            {
                dictOfCrates[i].Reverse();
            }


            for (int i = 0; i < moveList.Count(); i++)
            {
                int numOfMovingCaters = Convert.ToInt32(moveList[i]);
                
                var theCaterList = dictOfCrates[Convert.ToInt32(fromList[i])];

                for (int j = 0; j < numOfMovingCaters; j++)
                {
                    dictOfCrates[Convert.ToInt32(toList[i])].Add(theCaterList[theCaterList.Count() - 1 - j]);

                }
                int startRemovingFrom = theCaterList.Count() - numOfMovingCaters;
                dictOfCrates[Convert.ToInt32(fromList[i])].RemoveRange(startRemovingFrom, numOfMovingCaters);

            }

            for (int i = 1; i <= totalNumOfColumns; i++)
            {
                Console.WriteLine($"column {i}");
                Console.WriteLine(dictOfCrates[i].Last());
            }


        }
    }
}
