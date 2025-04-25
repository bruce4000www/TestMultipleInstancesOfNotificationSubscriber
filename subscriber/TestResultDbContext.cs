using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NotificationSubscriber
{
    public class TestResultDbContext : DbContext
    {
        public DbSet<LogResult> LogResults { get; set; }

        public TestResultDbContext(DbContextOptions<TestResultDbContext> options) : base(options)
        {
            // Ensure the database is created
            Database.EnsureCreated();
        }
    }

    public class LogResult
    {
        [Key] // Marks this property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configures auto-increment behavior
        public int Id { get; set; }
        [Required]
        public required string MachineName { get; set; }
        [Required]
        public required string TagCode { get; set; }
        [Required]
        public DateTimeOffset CreationTime { get; set; }
    }
}
