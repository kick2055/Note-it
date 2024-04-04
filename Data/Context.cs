using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace projekt.Data
{
    public class Context : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null;
        public DbSet<Note> Notes { get; set; } = null;
        public DbSet<Expense> Expenses { get; set; } = null;
        public DbSet<CategoryMoney> CategoriesMoney { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=notatki;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
