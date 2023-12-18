﻿using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet<User>Users { get; set; }
        public DbSet<Order> Orders {  get; set; }

        public DbSet<Product>Products { get; set; }
    }
}
