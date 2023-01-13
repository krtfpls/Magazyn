using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Documents;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){
            
        }

        public DbSet<Product> Products {get;set;}
        public DbSet<Category> Categories {get;set;}
        public DbSet<Document> Documents {get;set;}
        public DbSet<Customer> Customers {get;set;}
        public DbSet<DocumentLine> DocumentLines {get;set;}
        public DbSet<DocumentType> DocumentTypes {get; set;}
        public DbSet<User> Users {get;set;}
    }
}