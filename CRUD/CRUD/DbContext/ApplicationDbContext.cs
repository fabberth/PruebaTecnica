namespace CRUD.DbContext
{
    using CRUD.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Clase administradora de la base de datos
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales de modelo

            // Mapear la entidad Autor a la tabla Autores
            modelBuilder.Entity<Autor>().ToTable("Autores");
        }
    }
}
