﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filebrowser
{
    public class LogHelper
    {
        public static log4net.ILog GetLogger ([System.Runtime.CompilerServices.CallerFilePath] string filename="")
        {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}
