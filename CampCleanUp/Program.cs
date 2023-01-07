using System;
using System.Diagnostics.Metrics;

namespace CampCleanUp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\valer\source\repos\AdventOfCode2022\CampCleanUp\input.txt");


            List<string[]> rangesList = new List<string[]>();
            List<string[]> numberList = new List<string[]>();

            int count = 0;
            int start = 0;
            int finish = 0;
            int num1;
            int num2;
            int num3;
            int num4;

            int includes = 0;

            foreach (string line in lines)
            {
                
                rangesList.Add(line.Split(","));
                
                
            }

            foreach (var item in rangesList)
            {
                foreach (var array in item)
                {
                    numberList.Add(array.Split("-"));
                }  
            }

            
            while(start < numberList.Count())
            {
                
                num1 = Convert.ToInt32(numberList[start][finish]);
                num2 = Convert.ToInt32(numberList[start][finish+1]);
                num3 = Convert.ToInt32(numberList[start+1][finish]);
                num4 = Convert.ToInt32(numberList[start+1][finish+1]);


                if (num1 < num3 && num2 < num3)
                {

                }
                else if (num3 < num1 && num4 < num1)
                {

                }
                else
                {
                    includes++;
                }

                start = start + 2;

            }
                

            Console.WriteLine(includes);
        }

        
    }
}

