using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace TuningTrouble
{
    public class Program
    {
        static void Main(string[] args)
        {
            string line = readFromTheFile("TuningTrouble");
            int markerLength = 14;

            Console.WriteLine(findUniqueSequence(markerLength, line));

        }
        public static string readFromTheFile(string folderName)
        {
            return System.IO.File.ReadAllText($@"C:\Users\valer\source\repos\AdventOfCode2022\{folderName}\input.txt");

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
