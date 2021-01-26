namespace _02.LegionSystem.Models
{
    using System.Collections.Generic;

    public class HealthComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            // "inverted" comparison
            // direct comparison of integers should return x - y
            return y - x;
        }
    }
}