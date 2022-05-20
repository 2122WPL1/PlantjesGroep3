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
            =>input != null ? (input.Length > 1 ? char.ToUpper(input[0]) + input[1..] : input.ToUpper()) : null;

        public static string FirstToUpperRestToLower(this string input)
            => input != null ? (input.Length > 1 ? char.ToUpper(input[0]) + input[1..].ToLower() : input.ToUpper()) : null;

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

        public static string SpaceOnUpper(this string input)
        {
            List<int> indexs = new List<int>();
            foreach (var (value, i) in input.Select((value, i) => (value, i)))
            {
                if (char.IsUpper(value))
                    indexs.Add(i);
            }
            foreach (var (index, i) in indexs.Select((value, i) => (value, i)))
            {
                input = input[..(index + i)] + " " + input[(index + i)..];
            }
            return input.Trim();
        }
    }
}
