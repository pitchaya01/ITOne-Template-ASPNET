using Lazarus.Common.Nexus.Database;
using Lazarus.Common.Utilities;
using Microsoft.EntityFrameworkCore;
using SCGL.EDOC.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCGL.EDOC.Function.Infrastructure
{
    public class DbContextOptionsFactory
    {
        public static DbContextOptions<DbDataContext> GetDbContext()
        {
            var conStr = AppConfigUtilities.GetAppConfig<string>("DbDataContext");


            var builder = new DbContextOptionsBuilder<DbDataContext>();
            DbContextConfigurer.ConfigureDbContext(
                builder,
                conStr);

            return builder.Options;
        }
        public static DbContextOptions<DbReadDataContext> GetDbReadContext()
        {
            var conStr = AppConfigUtilities.GetAppConfig<string>("DbReadDataContext");

            var builder = new DbContextOptionsBuilder<DbReadDataContext>();
            DbContextConfigurer.ConfigureDbReadContext(
                builder,
                conStr);

            return builder.Options;
        }


        public static string GetNexusConnString => AppConfigUtilities.GetAppConfig<string>("NexusDatabase");

        public static DbContextOptions<NexusDataContext> GetNexus()
        {
            var conStr = AppConfigUtilities.GetAppConfig<string>("NexusDatabase");

            var builder = new DbContextOptionsBuilder<NexusDataContext>();
            DbContextConfigurer.ConfigureNexusContext(
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
            builder.UseSqlServer(connectionString).UseLazyLoadingProxies();

        }
        public static void ConfigureDbReadContext(DbContextOptionsBuilder<DbReadDataContext> builder,
     string connectionString)
        {
            builder.UseSqlServer(connectionString).UseLazyLoadingProxies(); ;

        }

        public static void ConfigureNexusContext(DbContextOptionsBuilder<NexusDataContext> builder,
     string connectionString)
        {
            builder.UseSqlServer(connectionString).UseLazyLoadingProxies(); ;
        }
    }
}
