using System.Collections.Generic;
using System.Text;

namespace ByteDev.PostmanEcho
{
    internal static class EnumerableExtensions
    {
        public static string ToQueryString<T>(this IEnumerable<T> source)
        {
            var query = new StringBuilder();

            foreach (var name in source)
            {
                if (query.Length > 0)
                    query.Append("&");

                query.Append(name);
            }

            return query.ToString();
        }
    }
}