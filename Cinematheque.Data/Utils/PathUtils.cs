﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cinematheque.Data.Utils
{
    public static class PathUtils
    {
        public static string GetProjectDirectory()
        {
            var full = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var dir = Path.GetDirectoryName(full);

            return dir.Substring(6, dir.Length - 30);
        }
    }
}