using RectilinearSteinerArborescence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(1, 1, 0));
            points.Add(new Point(7, 1, 1));
            points.Add(new Point(1, 4, 2));
            points.Add(new Point(7, 4, 3));

            //int[,] weights = new int[,]
            //{
            //    {0,1,15,6},
            //    {2,0,7,3},
            //    {9,6,0,12},
            //    {10,4,8,0}
            //};
            int[,] weights = new int[,]
{
                            {0,10,15,20},
                            {5,0,9,10},
                            {6,13,0,12},
                            {8,8,9,0}
};
            string file = "..\\..\\..\\files\\";
            string fileName = "p3.csv";
            file = file + fileName;
            var points2 = DataReader.ReadFile(file);

            int n = points2.Count();

            int[,] weights1 = new int[n,n];
            //int[,] weights1 = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                //Console.WriteLine(points2.ElementAt(i));
                for(int j = 0; j<n; j++)
                {
                    // tu kombinowałem bo trzeba na wejściu obliczyć odległosći między punktami 
                    weights1[i, j] = Convert.ToInt32((points2.ElementAt(i).GetDistance(points2.ElementAt(j))*10));
                   // weights1[i, j] = points2.ElementAt(i).GetDistance(points2.ElementAt(j));
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(Convert.ToInt32( weights1[i, j]) + " " );
                }
                Console.WriteLine();
            }
            Console.WriteLine("koniec");


            // Console.WriteLine((1 << 4) - 1);
            // Console.WriteLine((weights.GetLength(0)));
            // for (int i = 0; i <= 4; i++)
            // {
            //     Console.WriteLine(1 << i);
            //     Console.WriteLine("1& " + (1 << i) + " " + (1 & (1 << i)));
            // }
            // int[,] memorizeArray = new int[weights.GetLength(0), 1 << weights.GetLength(0)];

            //// Console.WriteLine(memorizeArray[1, 1]);

            // Console.WriteLine((1 & (1 << 0)));

            TravelingSalesman ts = new TravelingSalesman(weights1, points.ElementAt(0), points2);

            ts.tspPrepeare();
            foreach(Point p in ts.LowerCostVertex)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine(ts.MinimumCost);
            
        }
    }
}
