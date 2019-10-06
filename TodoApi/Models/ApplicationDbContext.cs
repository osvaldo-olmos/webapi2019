using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
        
        private static string GetConnectionString()
        {
            const string server = "tcp:ooo.database.windows.net,1433";
            const string databaseName = "todo";
            const string databaseUser = "osvaldo";
            const string databasePass = "3nFAt1c0!";
            
            return $"Server={server};" +
                   $"Initial Catalog={databaseName};" +
                   $"Persist Security Info=False;" +
                   $"User ID={databaseUser};" +
                   $"Password={databasePass};" +
                   $"MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}