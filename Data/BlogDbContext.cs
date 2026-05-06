using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) 
            : base(options)
        {
        }
        
        // DbSet представляет таблицу в базе данных
        public DbSet<Post> Posts { get; set; }
        
        // Если добавите комментарии позже
        // public DbSet<Comment> Comments { get; set; }
    }
}