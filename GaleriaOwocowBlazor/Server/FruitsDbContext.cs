using GaleriaOwocowBlazor.Shared;
using Microsoft.EntityFrameworkCore;

namespace GaleriaOwocowBlazor.Server;

public class FruitsDbContext : DbContext
{
    public DbSet<Fruit> Fruits { get; set; }

    public FruitsDbContext(DbContextOptions<FruitsDbContext> options) : base(options)
    {
    }
}