using Microsoft.EntityFrameworkCore;
using ParsiDNS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsiDNS.DataLayer.Context
{
    public class ParsiDnsContext : DbContext
    {
        public ParsiDnsContext(DbContextOptions<ParsiDnsContext> options) : base(options) { }


        #region Set Tables
        public DbSet<DNS> DNS { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<DnsSoftwares> DnsSoftware { get; set; }
        #endregion
    }
}
