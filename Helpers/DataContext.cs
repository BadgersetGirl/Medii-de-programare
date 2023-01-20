namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to mysql with connection string from app settings
        var connectionString = Configuration.GetConnectionString("WebApiDatabase");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // ... your existing code ...

    // modelBuilder.Entity<Category>().HasData(
    //     new Category { CategoryId = 1, Name = "News" },
    //     new Category { CategoryId = 2, Name = "Sports" },
    //     new Category { CategoryId = 3, Name = "Entertainment" }
    // );

    // modelBuilder.Entity<Article>().HasData(
    //     new Article { ArticleId = 1, Title = "Article 1", Content = "Content of article 1", CategoryId = 1, Status = "approved" },
    //     new Article { ArticleId = 2, Title = "Article 2", Content = "Content of article 2", CategoryId = 2, Status = "approved" },
    //     new Article { ArticleId = 3, Title = "Article 3", Content = "Content of article 3", CategoryId = 3, Status = "approved" }
    // );

    // modelBuilder.Entity<ArticleAccess>().HasData(
    //     new ArticleAccess { UserId = 1, ArticleId = 1 },
    //     new ArticleAccess { UserId = 1, ArticleId = 2 },
    //     new ArticleAccess { UserId = 2, ArticleId = 1 },
    //     new ArticleAccess { UserId = 2, ArticleId = 3 }
    // );
}

    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ArticleAccess> ArticleAccesses { get; set; }
    public DbSet<User> Users { get; set; }
}