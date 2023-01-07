using System;
using System.Diagnostics.Metrics;

namespace RucksackReorganisation
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\valer\source\repos\AdventOfCode2022\RucksackReorganisation\input.txt");

            List<int> listOfCharWeights = new List<int>();
            List<string> linesList = new List<string>();

            char character;
            int charWeight = 0;
            int counter = 0;

            foreach (string line in lines)
            {
                linesList.Add(line);
            }

            //Dictionary<int, char> alpha = new(){ 
            //    {27,'A'},{28,'B'},{29,'C'}, {30,'D'}, {31,'E'}, {32,'F'}, {33,'G'}, {34,'H'}, {35,'I'},
            //    {36,'J'}, {37,'K'}, {38,'L'}, {39,'M'}, {40,'N'}, {41,'O'}, {42,'P'}, {43,'Q'}, {44,'R'},
            //    {45,'S'}, {46,'T'}, {47,'U'}, {48,'V'}, {49,'W'}, {50,'X'}, {51,'Y'}, {52,'Z' }
            //};
            List<char> alpha = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            List<char> alpha1 = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
                'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            int start = 0;

            while (counter <= 100)
            {
                
                
                if (start == linesList.Count)
                {
                    Console.WriteLine("time to break because it is already {0}", start);
                    break;
                }

                
                for (int i = 0; i < 26; i++)
                {

                    if (linesList[start].Contains(alpha[i]) && linesList[start + 1].Contains(alpha[i]) && linesList[start + 2].Contains(alpha[i]))
                    {
                        character = alpha[i];
                        charWeight = alpha.IndexOf(character) + 27;
                        Console.WriteLine("Character = {0} and its weight = {1}", character, charWeight);
                    }
                    else if (linesList[start].Contains(alpha1[i]) && linesList[start + 1].Contains(alpha1[i]) && linesList[start + 2].Contains(alpha1[i]))
                    {
                        character = alpha1[i];
                        charWeight = alpha1.IndexOf(character) + 1;
                        Console.WriteLine("Character = {0} and its weight = {1}", character, charWeight);
                    }     
                    
                }

                listOfCharWeights.Add(charWeight);
                
                counter++;
                start += 3;
            }

            Console.WriteLine("This is Sum: {0}", listOfCharWeights.Sum());




        }
    }
}
