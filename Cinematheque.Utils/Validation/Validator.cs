using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cinematheque.Utils
{
    public static class Validator
    {
        public static bool IsNameValid(string name, string sname)
        {
            var re = "^[a-z ,.'-]+$";

            return !string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(sname) &&
                Regex.Match(name, re, RegexOptions.IgnoreCase).Success &&
                Regex.Match(sname, re, RegexOptions.IgnoreCase).Success;
        }

        public static bool IsAlphabetic(string s)
        {
            return !string.IsNullOrWhiteSpace(s) && Regex.Match(s, "[a-zA-Z-]+(( )?[a-zA-Z-]+)*").Success;
        }

        public static bool IsDatePast(DateTime dt)
        {
            return dt <= DateTime.Now;
        }

        public static void RequireNotNull(object o)
        {
            if (o == null)
            {
                throw new Exception("Object is null");
            }
        }

        public static void CheckFileExist(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("File does not exist");
            }
        }
    }
}
