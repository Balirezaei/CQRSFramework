using Microsoft.EntityFrameworkCore;
using System;
using Framework.Domain;

namespace CQRSFramework.QueryModel
{
    public class MainQueryModel : DbContext
    {
        public MainQueryModel()
        { }

        public MainQueryModel(DbContextOptions<MainQueryModel> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
