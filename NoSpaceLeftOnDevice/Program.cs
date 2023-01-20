using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace NoSpaceLeftOnDevice
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] line = readFromTheFile("NoSpaceLeftOnDevice");
            
            var structure = new Dictionary<string, List<int>>();
            char[] charsToTrim = {'c', 'd', ' ', 'i', 'r', '$'};
            List<string> sequenceList = new List<string>();

            string currentFolderName = "";
            int positionOfCurrentFolder = 0;

            string folderToAdd = "";
            int sum = 0;
            int counting = 0;


            var dictOfPositions = new Dictionary<int, string>();
            var dictOfSums = new Dictionary<string, List<string>>();
            var dictOfFolders = new Dictionary<string, List<string>>();



            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].Contains("cd"))
                {
                    if (line[i] == "$ cd ..")
                    {
                        positionOfCurrentFolder = positionOfCurrentFolder - 1;
                        currentFolderName = dictOfPositions[positionOfCurrentFolder];
                        
                    }
                    else
                    {
                        currentFolderName = line[i].Split(" ")[2];
                        positionOfCurrentFolder++;
                        dictOfPositions[positionOfCurrentFolder] = currentFolderName;
                        isItInDict(dictOfFolders, currentFolderName);
                        
                    }
                    //Console.WriteLine($"current folder name{currentFolderName}");
                    //foreach (var item in dictOfPositions)
                    //{
                    //    Console.WriteLine(item);
                    //}
                }
                else if (line[i].Contains("dir"))
                {
                    folderToAdd = line[i].Split(" ")[1];
                    dictOfFolders[currentFolderName].Add(folderToAdd);
                }
                else if (line[i].Any(char.IsDigit))
                {
                    string dig = line[i].Split()[0];
                    
                    isItInDict(dictOfSums, currentFolderName);
                    dictOfSums[currentFolderName].Add(dig);
                }
                else
                {

                }

            }


            

            Console.WriteLine(sum);
        }
        public static string[] readFromTheFile(string folderName)
        {
            return System.IO.File.ReadAllLines($@"C:\Users\valer\source\repos\AdventOfCode2022\{folderName}\input.txt");

        }

        public static string isItInDict(Dictionary<string, List<string>> structure, string folderName)
        {
            if (structure.ContainsKey(folderName))
            {
                return folderName;
                
            }
            else
            {
                structure.Add(folderName, new List<string>());
                return folderName;
            }
        }



        public static int findUniqueSequence(int markerLength, string line)
        {
            int numCount = markerLength;
            for (int i = 0; i < line.Count(); i++)
            {
                string diffSequence = line.Substring(i, markerLength);

                if (diffSequence.Distinct().Count() == markerLength)
                {
                    return numCount;
                }
                else
                {
                    numCount++;
                }
            }

            return numCount;
        }
    }
}