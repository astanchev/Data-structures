namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            var openedParentheses = new Stack<char>();

            for (int i = 0; i < parentheses.Length; i++)
            {
                var bracket = parentheses[i];

                if (bracket == '(' || bracket == '[' || bracket == '{')
                {
                    openedParentheses.Push(parentheses[i]);
                }
                else if (bracket == ')' || bracket == ']' || bracket == '}')
                {
                    if (openedParentheses.Count == 0)
                    {
                        return false;
                    }

                    var openBracket = openedParentheses.Pop();

                    if (openBracket == '(' && bracket != openBracket + 1)
                    {
                        return false;
                    }
                    else if (openBracket != '(' && bracket != openBracket + 2)
                    {
                        return false;
                    }
                }
            }

            if (openedParentheses.Count > 0)
            {
                return false;
            }

            return true;
        }
    }
}
