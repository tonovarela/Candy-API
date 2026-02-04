using System;
using CandyApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandyApi.Data;

public class ApplicationDBContext:DbContext
{

 public DbSet<CatUsuario> CatUsuarios { get; set; } = null!;
public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }


    //  protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);

    //     modelBuilder.Entity<CatUsuario>(entity =>
    //     {
    //         entity.HasKey(e => e.Id);
    //         entity.ToTable("cat_Usuarios", schema: "dbo"); // si la tabla está en otra BD, EF no cambia la BD; usa la del connection string
    //         entity.Property(e => e.Id).HasColumnName("Id");
    //         entity.Property(e => e.Login).HasColumnName("Login").HasMaxLength(200);
    //         entity.Property(e => e.Password).HasColumnName("Password").HasMaxLength(200);
            
    //     });
    // }

}
