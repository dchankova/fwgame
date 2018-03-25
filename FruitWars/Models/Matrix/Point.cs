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

        public bool ValidateDistance(Point other, int distrance)
        {
            if (!Equal(other))
            {
                if (Math.Abs(Y - other.Y) >= distrance ||
                    Math.Abs(X - other.X) >= distrance)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Equal(Point other)
         => X == other.X && Y == other.Y ? true : false;
    }
}
