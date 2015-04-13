using ClientManagerBase.Helpers;
using NUnit.Framework;

namespace ClientManagerBaseTests
{
    /// <summary>
    /// Test for <see cref="FilterByTagHelper"/>
    /// </summary>
    [TestFixture]
    public class FilterByTagHelperTest
    {
        /// <summary>
        /// Nulls the string to where test.
        /// </summary>
        [Test]
        public void NullStringToWhereTest()
        {
            // Setup
            string input = null;
            string expect = "true";

            // Execute
            var result = FilterByTagHelper.ConvertInputStringToSQLWhereStatement(input);

            // Verify
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// Empties the string to where test.
        /// </summary>
        [Test]
        public void EmptyStringToWhereTest()
        {
            // Setup
            string input = string.Empty;
            string expect = "true";

            // Execute
            var result = FilterByTagHelper.ConvertInputStringToSQLWhereStatement(input);

            // Verify
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// Whites the space string to where test.
        /// </summary>
        [Test]
        public void WhiteSpaceStringToWhereTest()
        {
            // Setup
            string input = "          ";
            string expect = "true";

            // Execute
            var result = FilterByTagHelper.ConvertInputStringToSQLWhereStatement(input);

            // Verify
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// Filters the and in string to where test.
        /// </summary>
        [Test]
        public void FilterAndInStringToWhereTest()
        {
            // Setup
            string input = "cool & quiet";
            string expect = "Tags.Any(Value==\"cool\") && Tags.Any(Value==\"quiet\")";

            // Execute
            var result = FilterByTagHelper.ConvertInputStringToSQLWhereStatement(input);

            // Verify
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// Filters the and with spaces in string to where test.
        /// </summary>
        [Test]
        public void FilterAndWithSpacesInStringToWhereTest()
        {
            // Setup
            string input = "    cool   &   quiet    ";
            string expect = "Tags.Any(Value==\"cool\") && Tags.Any(Value==\"quiet\")";

            // Execute
            var result = FilterByTagHelper.ConvertInputStringToSQLWhereStatement(input);

            // Verify
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// Filters the or in string to where test.
        /// </summary>
        [Test]
        public void FilterOrInStringToWhereTest()
        {
            // Setup
            string input = "cool | quiet";
            string expect = "Tags.Any(Value==\"cool\") || Tags.Any(Value==\"quiet\")";

            // Execute
            var result = FilterByTagHelper.ConvertInputStringToSQLWhereStatement(input);

            // Verify
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// Filters the or with spaces in string to where test.
        /// </summary>
        [Test]
        public void FilterOrWithSpacesInStringToWhereTest()
        {
            // Setup
            string input = "    cool   |   quiet    ";
            string expect = "Tags.Any(Value==\"cool\") || Tags.Any(Value==\"quiet\")";

            // Execute
            var result = FilterByTagHelper.ConvertInputStringToSQLWhereStatement(input);

            // Verify
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// Bigs the complex verify string to where test.
        /// </summary>
        [Test]
        public void BigComplexVerifyStringToWhereTest()
        {
            // Setup
            string input = "    cool   |   quiet   & cool|quiet  ";
            string expect = "Tags.Any(Value==\"cool\") || Tags.Any(Value==\"quiet\") && Tags.Any(Value==\"cool\") || Tags.Any(Value==\"quiet\")";

            // Execute
            var result = FilterByTagHelper.ConvertInputStringToSQLWhereStatement(input);

            // Verify
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// Bigs the complex verify with fail string to where test.
        /// </summary>
        [Test]
        public void BigComplexVerifyWithFailStringToWhereTest()
        {
            // Setup
            string input = "    cool   |   quiet  &|| cool  | quiet  ";
            string expect = "Tags.Any(Value==\"cool\") || Tags.Any(Value==\"quiet\") && Tags.Any(Value==\"cool\") || Tags.Any(Value==\"quiet\")";

            // Execute
            var result = FilterByTagHelper.ConvertInputStringToSQLWhereStatement(input);

            // Verify
            Assert.AreEqual(expect, result);
        }
    }
}
