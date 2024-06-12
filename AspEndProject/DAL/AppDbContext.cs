using AspEndProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<FactContent> FactContents { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<ServiceContent> ServiceContents { get; set; }

    }
}
