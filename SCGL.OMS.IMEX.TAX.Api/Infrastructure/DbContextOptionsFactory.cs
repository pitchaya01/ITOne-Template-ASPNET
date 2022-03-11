using Lazarus.Common;
using Lazarus.Common.Nexus.Database;
using Lazarus.Common.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.OMS.IMEX.TAX.Api.Infrastructure
{
    public class DbContextOptionsFactory
    {
        public static DbContextOptions<DbDataContext> GetDbContext()
        {
            var conStr= AppConfigUtilities.GetAppConfig<string>("DbDataContext");
            if (string.IsNullOrEmpty(conStr))
            {
                conStr = AppConfigUtilities.GetAppConfig<string>("DbDataContext");
            }
            var builder = new DbContextOptionsBuilder<DbDataContext>();
            DbContextConfigurer.ConfigureDbContext(
                builder,
                conStr);

            return builder.Options;
        }
        public static DbContextOptions<DbReadDataContext> GetDbReadContext()
        {
            var conStr = AppConfigUtilities.GetAppConfig<string>("DbReadDataContext");
            if (string.IsNullOrEmpty(conStr))
            {
                conStr = AppConfigUtilities.GetAppConfig<string>("DbReadDataContext");
            }

            var builder = new DbContextOptionsBuilder<DbReadDataContext>();
            DbContextConfigurer.ConfigureDbReadContext(
                builder,
                conStr);

            return builder.Options;
        }
 
 

    }
    public class DbContextConfigurer
    {
        public static void ConfigureDbContext(DbContextOptionsBuilder<DbDataContext> builder,
            string connectionString)
        {
            builder.UseSqlServer(connectionString);

        }
        public static void ConfigureDbReadContext(DbContextOptionsBuilder<DbReadDataContext> builder,
     string connectionString)
        {
            builder.UseSqlServer(connectionString);

        }
 
 
    }
}
