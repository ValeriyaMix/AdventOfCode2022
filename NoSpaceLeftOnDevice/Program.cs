using System;
using System.Collections;
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

            string currentFolderName = "";

            string folderToAdd = "";
            string parentFolderName = "";


            var dictOfPositions = new Dictionary<int, string>();
            var dictOfSums = new Dictionary<string, int>();
            var dictOfFolders = new Dictionary<string, List<string>>();

            List<string> listOfParents = new List<string>();
            int positionOfParentFolder = 0;

            currentFolderName = line[0].Split(" ")[2];
            parentFolderName = line[0].Split(" ")[2];

            int sumOfFiles = 0;


            var lineWithNumbers = new Regex(@"[1-9]", RegexOptions.Compiled);


            for (int i = 1; i < line.Length; i++)
            {
                if (line[i].Substring(0, 4) == "$ cd")
                {
                    if (line[i] == "$ cd ..")
                    {
                        
                        if (currentFolderName != "/")
                        {
                            positionOfParentFolder = listOfParents.FindIndex(x => x == currentFolderName) - 1;
                            if (dictOfFolders[listOfParents[positionOfParentFolder]].Contains(currentFolderName))
                            {
                                parentFolderName = listOfParents[positionOfParentFolder];
                                currentFolderName = parentFolderName;
                            }
                            else
                            {
                                while (!dictOfFolders[listOfParents[positionOfParentFolder]].Contains(currentFolderName))
                                {
                                    positionOfParentFolder = positionOfParentFolder - 1;
                                    parentFolderName = listOfParents[positionOfParentFolder];
                                }
                                currentFolderName = parentFolderName;
                            }
                            
                            
                        }
                        
                        //Console.WriteLine("Step back Parent folder {0}", parentFolderName);
                        //Console.WriteLine("Step back Current folder {0}", currentFolderName);
                        //Console.WriteLine(positionOfParentFolder);   
                    }
                    else
                    {
                        parentFolderName = currentFolderName;
                        currentFolderName = line[i].Split(" ")[2];

                        //Console.WriteLine("Step forward Parent folder {0}", parentFolderName);
                        //Console.WriteLine("Step forward Current folder {0}", currentFolderName);
                        

                        //if (dictOfFolders[parentFolderName].Contains(currentFolderName))
                        //{
                        //    Console.WriteLine($"Folder {currentFolderName} is in the folder {parentFolderName}");
                        //}
                        //else
                        //{
                        //    Console.WriteLine($"Folder {currentFolderName} is not in the folder {parentFolderName}");
                        //}
                        //Console.WriteLine("\n");

                    }

                }
                else if (line[i].Substring(0, 3) == "dir")
                {
                    folderToAdd = line[i].Split(" ")[1];
                    if (currentFolderName == "/")
                    {
                        dictOfFolders[currentFolderName].Add(folderToAdd);
                    }
                    else
                    {
                        //dictOfFolders[parentFolderName].Add(folderToAdd);
                        dictOfFolders[currentFolderName].Add(folderToAdd);
                    }

                }
                else if (line[i].Any(char.IsDigit))
                {
                    string dig = line[i].Split()[0];
                    if (currentFolderName == "/")
                    {
                        dictOfFolders[currentFolderName].Add(dig);
                    }
                    else
                    {
                        //dictOfFolders[parentFolderName].Add(dig);
                        dictOfFolders[currentFolderName].Add(dig);
                    }
                }
                else
                {
                    if (!dictOfFolders.ContainsKey(currentFolderName))
                    {
                        dictOfFolders.Add(currentFolderName, new List<string>());
                    }

                    if (!listOfParents.Contains(currentFolderName))
                    {
                        listOfParents.Add(currentFolderName);
                    }

                }

            }



            // Iterating through list of folders

            for (int i = dictOfFolders.Count() - 1; i > 0; i--)
            {
                sumOfFiles = 0;
                var item = dictOfFolders.ElementAt(i);
                var itemKey = item.Key;
                isItInDict(dictOfSums, itemKey);
                var itemValue = item.Value;
                foreach(var fi in item.Value)
                {
                    if (lineWithNumbers.IsMatch(fi))
                    {
                        sumOfFiles = sumOfFiles + Convert.ToInt32(fi);
                    }
                    else
                    {
                        //Console.WriteLine($"{fi} is a folder");
                        if (dictOfSums.ContainsKey(fi))
                        {
                            sumOfFiles = sumOfFiles + dictOfSums[fi];
                        }
                    }
                    
                }
                //Console.WriteLine($"Sum of the folder: {itemKey} files is {sumOfFiles}");
                dictOfSums[itemKey] = sumOfFiles;

            }

            sumOfFiles = 0;
            foreach (var val in dictOfSums)
            {
                
                if (val.Value <= 100000)
                {
                    sumOfFiles = sumOfFiles + val.Value;
                }
                Console.WriteLine($"Sum of the folder {val.Key} = {val.Value}");

            }
            Console.WriteLine('\n');
            Console.WriteLine(sumOfFiles);


        }
        public static int findSumOfNestedFilesAndFolders(Dictionary<string, List<string>> dictOfFolders, 
                                                         string valueToCheck, Regex regExExpr, int sumOfFiles)
        {
            foreach (var fileOrFolder in dictOfFolders[valueToCheck])
            {
                if (regExExpr.IsMatch(fileOrFolder))
                {
                    sumOfFiles = sumOfFiles + Convert.ToInt32(fileOrFolder);
                }
                else
                {
                    findSumOfNestedFilesAndFolders(dictOfFolders, fileOrFolder, regExExpr, sumOfFiles);
                }
            }
            return sumOfFiles;
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

        public static void isItInDict(Dictionary<string, int> structure, string folderName)
        {
            if (!structure.ContainsKey(folderName))
            {
                structure.Add(folderName, new int());
            }
        }


    }
}