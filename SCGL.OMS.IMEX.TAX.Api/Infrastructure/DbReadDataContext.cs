using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGL.EDOC.Api.Infrastructure
{
    public class DbReadDataContext : DbDataContext
    {
        public DbReadDataContext(DbContextOptions<DbReadDataContext> options)
  : base(options)
        { }
    }

}
