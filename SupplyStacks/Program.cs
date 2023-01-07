using System;
using System.Diagnostics.Metrics;
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
            var listOfCrates = new Dictionary<int, List<char>>();
            int numOfPos = 0;

            List<string> moveList = new List<string>();
            List<string> fromList = new List<string>();
            List<string> toList = new List<string>();


            var rx = new Regex(@"\[[a-zA-Z]]", RegexOptions.Compiled);
            var rxx = new Regex(@"\s[^1-9]", RegexOptions.Compiled);

            int totalNumOfLists = (lines[0].Count() - 3) / 4;
           


            foreach (string line in lines)
            {
                Console.WriteLine(line);
                             
                for (int i = 0; i < line.Length; i = i + 4)
                {
                    
                    if (rx.IsMatch(line.Substring(i, 3)) || rxx.IsMatch(line.Substring(i, 3)))
                    {
                        ListOfCrates.Add(numOfPos, line.Substring(i+1, 1));
                        Console.WriteLine(line.Substring(i+1, 1));
                        numOfPos++;
                        counter++;
                        //Console.WriteLine(numOfPos);
                        
                 
                    }
                    else
                    {
                        if (line.Contains("move"))
                        {
                            string[] lineSplit = line.Split(" ");
                            Console.WriteLine("move: {0} from: {1} to: {2}", lineSplit[1], lineSplit[3], lineSplit[5]);

                            Console.WriteLine("\n");
                            moveList.Add(lineSplit[1]);
                            fromList.Add(lineSplit[3]);
                            toList.Add(lineSplit[5]);

                            break;
                        }
                        
                    }
                }

            }
            //ListOfCrates.Remove(ListOfCrates.Keys.Last());
            Console.WriteLine("Length of dictionary:{0}", ListOfCrates[0]);

        }
    }
}
