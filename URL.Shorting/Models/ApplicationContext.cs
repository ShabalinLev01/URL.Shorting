﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using URL.Shorting.Models;

namespace URL.Shorting.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}