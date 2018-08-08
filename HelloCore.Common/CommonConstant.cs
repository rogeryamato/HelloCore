using Autofac;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using log4net.Repository;

namespace HelloCore.Common
{
    public class CommonConstant
    {
        public readonly static string REPOSITORY = "repository";
        public static List<string> NeedSaveChangesMethodNames
        {
            get { return new List<string> { "UpdateAll", "Insert", "Update", "Delete", "Save", "DeleteByKey","Add" }; }
        }
        
        public static IConfiguration Configuration { get; set; }
        public static ILoggerRepository LoggerRepository { get; set; }

        public readonly static string CookieName = "Cookies";
    }
}
