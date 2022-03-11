using Microsoft.EntityFrameworkCore;
using SCGL.EDOC.Api.Domain;
//using SCGL.SCM.Api.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Infrastructure
{
    public class DbDataContext : DbContext
    {
        public DbDataContext(DbContextOptions<DbDataContext> options)
: base(options)
        { }
        protected DbDataContext(DbContextOptions options)
  : base(options)
        {
        }

 

        public DbSet<DOCUMENT> DOCUMENTS { get; set; }
        public DbSet<GROUP_DOCUMENT> GROUP_DOCUMENTS { get; set; }
        public DbSet<MASTER_DOCTYPE> MASTER_DOCTYPES { get; set; }




    }
}
