using Microsoft.EntityFrameworkCore;
using NotaAPI.Models;

namespace NotaAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Nota> Notas { get; set; }
}
