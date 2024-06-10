using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace LttLesson8.Models
{
    public class LttBookStore : DbContext
    {
        public LttBookStore():base("LttBookStoreConnectionStrings")
            {
                
            }
        //Tao cac bang
        public DbSet<LttCategory> LttCategories { get; set; }
        public DbSet<LttBook> LttBooks { get; set; }
    }
}