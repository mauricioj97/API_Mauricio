using LibraryAPI_Mauricio.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace LibraryAPI_Mauricio.DAL
{
    public class DataBaseContext : DbContext
    {
        // CONEXION DB POR MEDIO DE ESTE CONSTRUCTOR
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) 
        {
        


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasIndex(b => b.Name).IsUnique();
        }

        #region DbSets

        public DbSet<Book> Books { get; set; }

        #endregion

    }
}
