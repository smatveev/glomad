using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
            };

        public static string AddRelAttrs(this string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            return input.Replace("<a href", "<a rel=\"noopener nofollow noreferrer\" href");
        }
    }
}
