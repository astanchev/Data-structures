namespace _02.Data.Models
{
    using System.Collections.Generic;

    public class MyComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x - y;
        }
    }
}