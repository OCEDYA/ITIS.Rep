using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            if (ax == bx && ay == by)
            {
                return Math.Sqrt((x - ax) * (x - ax) + (y - ay) * (y - ay));
            }
            else if (((x - ax) * (bx - ax) + (y - ay) * (by - ay)) < 0 || ((x - bx) * (ax - bx) + (y - by) * (ay - by)) < 0)
            {
                double a = Math.Sqrt((x - ax) * (x - ax) + (y - ay) * (y - ay));
                double b = Math.Sqrt((x - bx) * (x - bx) + (y - by) * (y - by));
                return Math.Min(a, b);
            }
            else
            {
                double a = Math.Abs((bx - ax) * (y - ay) - (by - ay) * (x - ax));
                double b = Math.Sqrt((bx - ax) * (bx - ax) + (by - ay) * (by - ay));
                return a / b;
            }
        }
    }
}
