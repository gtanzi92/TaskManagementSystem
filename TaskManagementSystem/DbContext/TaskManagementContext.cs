using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Principal;

namespace TaskManagementSystem.Context
{
    //This is the context that you will need to wire in with your models in order to create code-first migrations and database sync.
    public class TaskManagementContext : DbContext
    {
        // I didn't want to use Dependency Injection for DBContext. But i want to controll the database call in each method. 
        public TaskManagementContext() : base() { }

        public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options)
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("TaskManagementDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
