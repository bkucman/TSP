using System;

namespace RectilinearSteinerArborescence
{
    public class Point : IComparable
    {
        private int x, y;
        private int num;
        public Point(int x, int y,int num)
        {
            this.x = x;
            this.y = y;
            this.num = num;

        }
        public Point(Point a)
        {
            this.x = a.x;
            this.y = a.y;
            this.num = a.num;

        }
        public void setPoint(Point a)
        {
            this.x = a.x;
            this.y = a.y;
            this.num= a.num;
        }
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public int GetNum()
        {
            return num;
        }
        public override string ToString()
        {
            return "Point(" + x + "," + y + "): (" + num+ ")";
        }
        
        public double GetDistance(Point point)
        {
            return Math.Sqrt(Math.Pow(point.GetX() - this.x, 2) + Math.Pow(point.GetY() - this.y, 2));
        }
        // 1 - po y, else po x
        public int CompareTo(object obj, int v)
        {
            if (obj is Point)
            {
                if (v == 1)
                {
                    if (this.y == (obj as Point).GetY())
                        return this.x.CompareTo((obj as Point).GetX());
                    return this.y.CompareTo((obj as Point).GetY());
                }
                else
                {
                    if (this.x == (obj as Point).GetX())
                        return this.y.CompareTo((obj as Point).GetY());
                    return this.x.CompareTo((obj as Point).GetX());
                }
            }
            throw new ArgumentException("Object is not a Point");
        }

        public int CompareTo(object obj)
        {
            if (obj is Point)
            {
                if (this.x == (obj as Point).GetX())
                    return this.y.CompareTo((obj as Point).GetY());
                return this.x.CompareTo((obj as Point).GetX());
            }
            throw new ArgumentException("Object is not a Point");
        }
    }
}
