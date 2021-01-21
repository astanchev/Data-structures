namespace ConsoleApp
{
    using System;
    using System.Linq;
    using Tree;

    class Program
    {
        private static TreeFactory treeFactory;

        static void Main(string[] args)
        {
            treeFactory = new TreeFactory();
            int n = int.Parse(Console.ReadLine());
            string[] input = new string[n];

            for (int i = 0; i < n; i++)
            {
                input[i] = Console.ReadLine();
            }

            var tree = treeFactory.CreateTreeFromStrings(input);

            Console.WriteLine();
            Console.WriteLine(string.Join(" ", tree.GetLongestPath()));
        }
    }
}
