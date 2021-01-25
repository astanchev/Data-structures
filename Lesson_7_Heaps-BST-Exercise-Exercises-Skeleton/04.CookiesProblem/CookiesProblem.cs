namespace _04.CookiesProblem
{
    using Wintellect.PowerCollections;

    public class CookiesProblem
    {
        //public int Solve(int k, int[] cookies)
        //{
        //    var bag = new OrderedBag<int>(/*compareElements*/);

        //    foreach (var cookie in cookies)
        //    {
        //        bag.Add(cookie);
        //    }

        //    int currentMinSweetness = bag.GetFirst();
        //    int steps = 0;

        //    while (currentMinSweetness < k && bag.Count > 1)
        //    {
        //        int leastSweetCookie = bag.RemoveFirst();
        //        int secondLeastSweetCookie = bag.RemoveFirst();

        //        int combined = leastSweetCookie + 2 * secondLeastSweetCookie;

        //        bag.Add(combined);

        //        currentMinSweetness = bag.GetFirst();

        //        steps++;
        //    }

        //    return currentMinSweetness < k ? -1 : steps;

        //    int compareElements(int first, int second)
        //    {
        //        return second - first;
        //    }
        //}

        public int Solve(int k, int[] cookies)
        {
            var cookieJar = new MinHeap<int>();

            foreach (var cookie in cookies)
            {
                cookieJar.Add(cookie);
            }

            var steps = 0;
            var current = cookieJar.Dequeue();

            while (current < k && cookieJar.Size > 0)
            {
                //Get second least sweet
                var second = cookieJar.Dequeue();
                //Create mixed cookie
                var newOne = current + second * 2;

                cookieJar.Add(newOne);

                steps++;

                current = cookieJar.Dequeue();
            }

            return current < k ? -1 : steps;
        }

    }
}
