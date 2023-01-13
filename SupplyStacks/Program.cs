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

            char character;
            int counter = 0;
            

            var ListOfCrates = new Dictionary<int, string>();
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
                    //Console.WriteLine("line of substring is {0}", line.Substring(i, 3));

                    if (rx.IsMatch(line.Substring(i, 3))) 
                    {
                        //Console.WriteLine("i add a letter {0}", line.Substring(i, 3));

                        if (dictOfCrates.ContainsKey(numOfPos))
                        {
                            dictOfCrates[numOfPos].Add(line.Substring(i + 1, 1));
                            //Console.WriteLine(numOfPos);
                        }
                        else
                        {
                            dictOfCrates.Add(numOfPos, new List<string>());
                            dictOfCrates[numOfPos].Add(line.Substring(i + 1, 1));
                            //Console.WriteLine(numOfPos);
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
                        //Console.WriteLine("i add a letter nothing", line.Substring(i, 3));

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
                        //Console.WriteLine("populate arrays");
                        if (line.Contains("move"))
                        {
                            string[] lineSplit = line.Split(" ");
                            //Console.WriteLine("move: {0} from: {1} to: {2}", lineSplit[1], lineSplit[3], lineSplit[5]);

                            moveList.Add(lineSplit[1]);
                            fromList.Add(lineSplit[3]);
                            toList.Add(lineSplit[5]);

                            break;
                        }
                        
                    }
                }

            }
            //for (int i = 1; i <= totalNumOfColumns; i++)
            //{
            //    Console.WriteLine("column {0}", i);
            //    foreach (var item in dictOfCrates[i])
            //    {
            //        Console.WriteLine(item);
            //    }
            //    Console.WriteLine("\n");
            //}


            for (int i = 0; i < moveList.Count(); i++)
            {
                int numOfMovingCaters = Convert.ToInt32(moveList[i]);
                //Console.WriteLine("the num of moving caters: {0}", numOfMovingCaters);
                //Console.WriteLine("\n");
                var theCaterList = dictOfCrates[Convert.ToInt32(fromList[i])];


                for (int j = 0; j < numOfMovingCaters; j++)
                {
                    dictOfCrates[Convert.ToInt32(toList[i])].Add(theCaterList[0 + j]);
                    
                    //Console.WriteLine("i m adding: {0}", theCaterList[0 + j]);
                }
                dictOfCrates[Convert.ToInt32(fromList[i])].RemoveRange(0, numOfMovingCaters);

                //Console.WriteLine("letters that are left after moving:");
                //foreach (var theCater in dictOfCrates[Convert.ToInt32(fromList[i])])
                //{
                //    Console.WriteLine(theCater);
                //}

            }

            for (int i = 1; i <= totalNumOfColumns; i++)
            {
                Console.WriteLine($"column {i}");
                //Console.WriteLine(dictOfCrates[i].First());
                foreach (var item in dictOfCrates[i])
                {
                    Console.WriteLine(item);
                }
            }


        }
    }
}
