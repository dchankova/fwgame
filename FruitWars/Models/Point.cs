using System;

namespace FruitWars.Models
{
    public class Point
    {
        private int x;
        private int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public bool ValidateDistance(Point second, int distrance)
        {
            if (!Equals(second))
            {
                if (Math.Abs(Y - second.Y) >= distrance ||
                    Math.Abs(X - second.X) >= distrance)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
