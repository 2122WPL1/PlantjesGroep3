using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Models.Extensions
{
    internal static class StringExtensions
    {
        public static string FirstToUpper(this string input) => input.Length > 1 ? input[..0].ToUpper() + input[1..] : input.ToUpper();
    }
}
