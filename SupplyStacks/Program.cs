using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace SupplyStacks
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] lines = readFromTheFile("SupplyStacks");

            List<string> howMuchToMoveList = new List<string>();
            List<string> fromColumnList = new List<string>();
            List<string> toColumnList = new List<string>();


            var lettersWithBrackets = new Regex(@"\[[a-zA-Z]]", RegexOptions.Compiled);
            var spaceWithNumbers = new Regex(@"\s[^1-9]", RegexOptions.Compiled);

            int totalNumOfColumns = (lines[2].Count() - 3) / 4 + 1;

            var dictOfCrates = createDictFromData(lines, lettersWithBrackets, spaceWithNumbers, totalNumOfColumns,
                                                    howMuchToMoveList, fromColumnList, toColumnList);

            reverseDict(dictOfCrates, totalNumOfColumns);

            movingCratesPart2(dictOfCrates, howMuchToMoveList, fromColumnList, toColumnList);

            printResult(dictOfCrates, totalNumOfColumns);
        }

        public static string[] readFromTheFile(string folderName)
        {
            return System.IO.File.ReadAllLines($@"C:\Users\valer\source\repos\AdventOfCode2022\{folderName}\input.txt");
            
        }

        public static Dictionary<int, List<string>> createDictFromData(string[] lines, Regex lettersWithBrackets,
                                                                        Regex spaceWithNumbers, int totalNumOfColumns,
                                                                        List<string> howMuchToMoveList,
                                                                        List<string> fromColumnList, List<string> toColumnList)
        {
            int counter = 0;
            int numOfPos = 1;
            var dictOfCrates = new Dictionary<int, List<string>>();

            foreach (string line in lines)
            {

                for (int i = 0; i < line.Length; i = i + 4)
                {
                    if (lettersWithBrackets.IsMatch(line.Substring(i, 3)))
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
                    else if (spaceWithNumbers.IsMatch(line.Substring(i, 3)))
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

                            howMuchToMoveList.Add(lineSplit[1]);
                            fromColumnList.Add(lineSplit[3]);
                            toColumnList.Add(lineSplit[5]);

                            break;
                        }

                    }
                }

            }

            return dictOfCrates;
        }

        public static Dictionary<int, List<string>> reverseDict(Dictionary<int, List<string>> dictOfCrates, int totalNumOfColumns)
        {
            //Reversing lists in dictionary

            for (int i = 1; i <= totalNumOfColumns; i++)
            {
                dictOfCrates[i].Reverse();
            }
            return dictOfCrates;
        }


        public static void movingCrates(Dictionary<int, List<string>> dictOfCrates, List<string>  howMuchToMoveList,
                                        List<string> fromColumnList, List<string> toColumnList)
        {
            for (int i = 0; i < howMuchToMoveList.Count(); i++)
            {
                int numOfMovingCaters = Convert.ToInt32(howMuchToMoveList[i]);

                var theCaterList = dictOfCrates[Convert.ToInt32(fromColumnList[i])];

                for (int j = 0; j < numOfMovingCaters; j++)
                {
                    dictOfCrates[Convert.ToInt32(toColumnList[i])].Add(theCaterList[theCaterList.Count() - 1 - j]);

                }
                int startRemovingFrom = theCaterList.Count() - numOfMovingCaters;
                dictOfCrates[Convert.ToInt32(fromColumnList[i])].RemoveRange(startRemovingFrom, numOfMovingCaters);
            }
        }

        public static void movingCratesPart2(Dictionary<int, List<string>> dictOfCrates, List<string> howMuchToMoveList,
                                        List<string> fromColumnList, List<string> toColumnList)
        {
            for (int i = 0; i < howMuchToMoveList.Count(); i++)
            {
                int numOfMovingCaters = Convert.ToInt32(howMuchToMoveList[i]);

                var theCaterList = dictOfCrates[Convert.ToInt32(fromColumnList[i])];

                for (int j = 0; j < numOfMovingCaters; j++)
                {
                    dictOfCrates[Convert.ToInt32(toColumnList[i])].Add(theCaterList[theCaterList.Count() - (numOfMovingCaters - j)]);

                }
                int startRemovingFrom = theCaterList.Count() - numOfMovingCaters;
                dictOfCrates[Convert.ToInt32(fromColumnList[i])].RemoveRange(startRemovingFrom, numOfMovingCaters);
            }
        }

        public static void printResult(Dictionary<int, List<string>> dictOfCrates, int totalNumOfColumns)
        {
            for (int i = 1; i <= totalNumOfColumns; i++)
            {
                Console.WriteLine($"column {i}");
                Console.WriteLine(dictOfCrates[i].Last());
            }
        }
    }
}
