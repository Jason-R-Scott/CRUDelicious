using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Models
{
    public class DishContext : DbContext
    {
        public DishContext (DbContextOptions<DishContext> options) : base(options) {}

        public DbSet<Dish> Dishes { get; set; }
    }
}