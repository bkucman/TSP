using RectilinearSteinerArborescence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    public class TravelingSalesman
    {
        private List<Point> points = new List<Point>();
        private List<Point> lowerCostVertex = new List<Point>();
        private Point startPoint;
        private int[,] memorizeArray;
        private int[,] prevArray;
        private int[,] weights;
        private int minimumCost = Int32.MaxValue;
        private int lastState;

        public List<Point> LowerCostVertex { get => lowerCostVertex; set => lowerCostVertex = value; }
        public int MinimumCost { get => minimumCost; set => minimumCost = value; }

        public TravelingSalesman(int[,] weights, Point startPoint, List<Point> points)
        {

            this.weights = weights;
            this.startPoint = startPoint;
            this.points = points;
            // 1 << size weight e.g. 4 node => ( 1<<4 )-1 = 15 (1111)
            this.lastState = (1 << weights.GetLength(0)) - 1;


        }
        // set fields in array to memorize states to minValue
        private int[,] setArray(int[,] array)
        {
            Console.WriteLine(array.GetLength(0) + " " + array.GetLength(1));
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = Int32.MinValue;
                }
            }
            Console.WriteLine();
            return array;
        }
        #region Drukowanie 
        private int[,] printArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {

                    Console.Write("{0,2}", (array[i, j] == Int32.MinValue) ? 0 : array[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            return array;
        }



        #endregion


        public void tspPrepeare()
        {
            // set first state to 1 << startPoint e.g. startPoint is 0 so 1 << 0 = 1 , startPoint = 1 : 1 << 1 = 2 (0010)

            int state = (1 << startPoint.GetNum());
            // set size e.g 4 nodes array[4,16]
            memorizeArray = new int[weights.GetLength(0), 1 << weights.GetLength(0)];
            prevArray = new int[weights.GetLength(0), 1 << weights.GetLength(0)];

            memorizeArray = setArray(memorizeArray);
            prevArray = setArray(prevArray);

            MinimumCost = TSP(startPoint, state, memorizeArray, prevArray);

            // printArray(memorizeArray);
            // printArray(prevArray);
            PreparePath(prevArray, state);
        }

        private int TSP(Point iPoint, int state, int[,] memorizeArray, int[,] prevArray)
        {
            int actualMinCost = Int32.MaxValue;
            int indekx = 0;
            int nextState;
            int nextCost;

            if (state == lastState)
            {
                //Console.WriteLine("if 1 .");
                // 
                memorizeArray[iPoint.GetNum(), state] = weights[iPoint.GetNum(), startPoint.GetNum()];
                return weights[iPoint.GetNum(), startPoint.GetNum()];
            }

            if (memorizeArray[iPoint.GetNum(), state] != Int32.MinValue)
            {
                // Console.WriteLine("if 2 .");
                return memorizeArray[iPoint.GetNum(), state];
            }


            for (int nextVisit = 0; nextVisit < weights.GetLength(0); nextVisit++)
            {
                // sprawdza czy byłem w węźle już. sprytnie ( na podstawie & (stan & aktulany wezeł z for)
                if ((state & (1 << nextVisit)) == 0)
                {
                    nextState = state | (1 << nextVisit);
                    // Console.WriteLine("++n"+" " + iPoint.GetNum()+ " "+nextVisit+" " + nextState);
                    nextCost = weights[iPoint.GetNum(), nextVisit] + TSP(points.ElementAt(nextVisit), nextState, memorizeArray, prevArray);
                    if (nextCost < actualMinCost)
                    {
                        actualMinCost = nextCost;
                        indekx = nextVisit;
                    }

                }
                else
                {
                    //Console.WriteLine("Nope");
                }

            }

            prevArray[iPoint.GetNum(), state] = indekx;

            memorizeArray[iPoint.GetNum(), state] = actualMinCost;

            // printArray(memorizeArray);
            // printArray(prevArray);

            return actualMinCost;
        }

        private void PreparePath(int[,] prevArray, int state)
        {
            int actualIndex = startPoint.GetNum();
            int nextIndex;

            do
            {
                lowerCostVertex.Add(points.ElementAt(actualIndex));
                nextIndex = prevArray[actualIndex, state];

                state = state | (1 << nextIndex);
                actualIndex = nextIndex;

            } while (nextIndex != Int32.MinValue);

            lowerCostVertex.Add(points.ElementAt(startPoint.GetNum()));
        }


    }
}
