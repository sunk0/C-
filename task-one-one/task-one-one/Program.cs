using System;
using System.Collections.Generic;
using System.Linq;
namespace task_one_one
{
    class Program
    {
        static void Main(string[] args)
        {
            int lines = 0;
            again:
            try
            {
                lines = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("U need to input the number of lines");
                goto again;
            }
            List<string> packetsWithNoDep = new List<string>();
            List<string[]> packetsWithDep = new List<string[]>();
            for (int i = 0; i < lines; i++)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split(" ");

                if (input.Length == 1)
                {
                    packetsWithNoDep.Add(input);
                }
                else
                {
                    packetsWithDep.Add(tokens);
                }
            }
            List<string[]> packetsWithDepOrderedByLength = packetsWithDep.OrderBy(x => x.Length).ToList();
            List<string> shashma = new List<string>();
            int count = 0;
            while (count != lines)
            {
                for (int i = 0; i < packetsWithDepOrderedByLength.Count; i++)
                {
                    bool check = true;
                    for (int j = 1; j < packetsWithDepOrderedByLength[i].Count(); j++)
                    {
                        bool depFound = false;
                        for (int k = 0; k < packetsWithNoDep.Count; k++)
                        {
                            var current = packetsWithDepOrderedByLength[i][j];
                            depFound = current.Equals(packetsWithNoDep[k]);
                            if (depFound)
                            {
                                break; 

                            }
                        }
                        check = check & depFound;
                    }
                    if (check)
                    {
                        packetsWithNoDep.Add(packetsWithDepOrderedByLength[i][0]);
                    }
                }
                count++;
            }
            List<string> test = new List<string>();
            test.AddRange(packetsWithNoDep);
            List<string> distinctElements = new List<string>();
            distinctElements.AddRange(packetsWithNoDep.Distinct());
            foreach (var element in distinctElements)
            {
                Console.Write(element);
                Console.Write(" ");
            }

            Console.ReadLine();
        }
    }
}