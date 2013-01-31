using System.Text.RegularExpressions;
using Xunit;

namespace SubSonic.Tests.Unit.Linq.TestBases
{
    public abstract class SqlStringTestsBase
    {
        /// <summary>
        /// Asserts the equality of expected and actual strings ignoring extra whitespace and carriage return.
        /// </summary>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        public void AssertEqualIgnoringExtraWhitespaceAndCarriageReturn(string expected, string actual)
        {
            // Strip extra whitespace and carriage returns
            expected = ReplaceExtraWhitespaceAndCarriageReturn(expected);
            actual = ReplaceExtraWhitespaceAndCarriageReturn(actual);

            Assert.Equal(expected, actual);
        }

        private string ReplaceExtraWhitespaceAndCarriageReturn(string input)
        {
            input = Regex.Replace(input, "\\s+", " ");
            input = Regex.Replace(input, "^\\s*", "");
            input = Regex.Replace(input, "\\s*$", "");
            input = Regex.Replace(input, "\\n+", "");
            return Regex.Replace(input, "\r", "");
        }
    }
}