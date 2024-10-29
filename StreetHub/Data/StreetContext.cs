using Microsoft.EntityFrameworkCore;
using StreetHub.Models;

namespace StreetHub.Data
{
    /// <summary>
    /// Represents the database context for the StreetHub application.
    /// </summary>
    public class StreetContext : DbContext
    {
        /// <summary>
        /// Gets or sets the collection of streets in the database.
        /// </summary>
        public DbSet<Street> Streets { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreetContext"/> class.
        /// </summary>
        /// <param name="options">The options for the database context.</param>
        public StreetContext(DbContextOptions<StreetContext> options) : base(options) { }

        /// <summary>
        /// Configures the model and its relationships using Fluent API.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Street>(entity =>
            {
                entity.ToTable("Streets");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100); 

                entity.Property(s => s.Capacity)
                    .IsRequired();

                entity.Property(s => s.Geometry)
                    .HasColumnType("geometry(LineString, 4326)");
            });
        }
    }
}