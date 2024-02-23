using DewavesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DewavesAPI.Data
{
    public class DewavesAPIDbContext : DbContext
    {
        public DewavesAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacted { get; set; }
    }
}
