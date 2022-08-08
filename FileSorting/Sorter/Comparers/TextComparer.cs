namespace Sorter
{
    public class TextComparer : IComparer<string>
    {
        public int Compare(string? first, string? second)
        {
            var firstSplitted = first?.Split(new char[] { '.' }, 2);
            var secondSplitted = second?.Split(new char[] { '.' }, 2);

            if (firstSplitted == null || firstSplitted.Length < 2 || secondSplitted == null || secondSplitted.Length < 2)
                return 0;

            var comparison = string.Compare(firstSplitted[1], secondSplitted[1]);

            if (comparison == 0)
            {
                var firstNumber = long.Parse(firstSplitted[0].Substring(0, firstSplitted[0].Length));
                var secondNumber = long.Parse(secondSplitted[0].Substring(0, secondSplitted[0].Length));

                return firstNumber.CompareTo(secondNumber);
            }
            else
                return comparison;
        }
    }
}
