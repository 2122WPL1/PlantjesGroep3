using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Models.Extensions
{
    internal static class StringExtensions
    {
        //written by Warre
        public static string FirstToUpper(this string input) 
            =>input.Length > 1 ? char.ToUpper(input[0]) + input[1..].ToLower() : input.ToUpper();

        public static string RemoveQuotes(this string name)
        {
            if (name == null)
                return null;
            name = name.Trim();
            if (name.StartsWith('\''))
                name = name[1..];
            if (name.EndsWith('\''))
                name = name[..^1];
            return name;
        }
    }
}
