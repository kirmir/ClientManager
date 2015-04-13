using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientManagerBase.Helpers
{
    /// <summary>
    /// Represent's easy to use features for teg filtering.
    /// </summary>
    public static class FilterByTagHelper
    {
        /// <summary>
        /// Converts the input string to SQL where statement.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The SQL where statement.</returns>
        public static string ConvertInputStringToSQLWhereStatement(string input)
        {
            var result = new StringBuilder();
            var sb = new StringBuilder(input);

            sb.Replace("|", ",|,");
            sb.Replace("&", ",&,");
            var commands = sb.ToString().Split(',');

            bool lastWasAndOrOr = true;

            foreach (var command in commands)
            {
                if (string.IsNullOrWhiteSpace(command)) continue;

                if (command == "&")
                {
                    if (lastWasAndOrOr) continue;
                    lastWasAndOrOr = true;
                    result.Append("&& ");
                    continue;
                }

                if (command == "|")
                {
                    if (lastWasAndOrOr) continue;
                    lastWasAndOrOr = true;
                    result.Append("|| ");
                    continue;
                }

                if (!lastWasAndOrOr) continue;
                lastWasAndOrOr = false;

                result.AppendFormat("Tags.Any(Value==\"{0}\") ", command.Trim());
            }

            var res = result.ToString().Trim();

            return res == string.Empty ? "true" : res;
        }
    }
}
