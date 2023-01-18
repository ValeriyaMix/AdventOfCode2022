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
            int positionOfCurrentFolder = 1;

            string folderToAdd = "";
            int sum = 0;
            int counting = 0;


            var dictOfPositions = new Dictionary<int, string>();
            var dictOfSums = new Dictionary<string, int>();
            var dictOfFolders = new Dictionary<string, List<string>>();



            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].Contains("cd"))
                {
                    if (line[i] == "$ cd ..")
                    {
                        currentFolderName = dictOfPositions[positionOfCurrentFolder - 1];
                        positionOfCurrentFolder--;
                    }
                    else
                    {
                        currentFolderName = line[i].Trim(charsToTrim);
                        dictOfPositions[positionOfCurrentFolder] = currentFolderName;
                        positionOfCurrentFolder++;
                    }
                    Console.WriteLine(currentFolderName);
                }
                else if (line[i].Contains("dir"))
                {
                    folderToAdd = line[i].Trim(charsToTrim);
                    isItInDict(dictOfFolders, folderToAdd);
                }
                else if (line[i].Any(char.IsDigit))
                {
                    string[] dig = line[i].Split();
                    sum = sum + Convert.ToInt32(dig[0]);
                    dictOfSums[currentFolderName] = sum;
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