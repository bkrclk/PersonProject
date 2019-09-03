using Microsoft.Extensions.DependencyInjection;
using PersonClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClassLibrary
{
    public class DBProjectContext : DbContext  
    {


        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Add framework services.
        //    services.AddDbContext<DBProjectContext>(options =>
        //        options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
        //}

        //public DbSet<User> user { set; get; }
        //public DbSet<Project> project { set; get; }
        //public DbSet<ProjectRoles> projectroles { set; get; }

    }




}
