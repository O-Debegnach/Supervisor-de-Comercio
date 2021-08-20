using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_Layer.General
{
    public static class StringHelpers
    {
        public static string Center(this string s, int lenght)
        {
            var i = lenght % 2 == 0 ? 0 : 1;
            string r = s.PadLeft((lenght + i) / 2 + s.Length / 2);
            return r.PadRight(lenght);
        }

        public static string Center(this string s, int lenght, char paddingChar)
        {
            var i = lenght % 2 == 0 ? 0 : 1;
            string r = s.PadLeft((lenght + i) / 2 + s.Length / 2, paddingChar);
            return r.PadRight(lenght, paddingChar);
        }
    }
}
