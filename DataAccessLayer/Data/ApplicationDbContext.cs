using DataAccessLayer.DataBaseTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<TblState> tblState { get; set; }
        public DbSet<TblCity> tblCity { get; set; }
        public DbSet<TblUserRegistration> tblUserRegistration { get; set; }
    }
}
