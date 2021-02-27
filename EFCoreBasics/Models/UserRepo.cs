using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBasics.Models
{
    public class UserRepo : DbContext
    {
        public UserRepo(DbContextOptions<UserRepo> options) : base(options) 
        {
        
        }

        public DbSet<UserInfoModel> UserTable { get; set; }
    }
}
