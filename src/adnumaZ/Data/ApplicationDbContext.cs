using adnumaZ.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace adnumaZ.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> UserAccounts { get; set; }
        public DbSet<Torrent> Torrents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(t => t.UploadedTorrents)
                .WithOne(c => c.Uploader);

            base.OnModelCreating(builder);
        }
    }
}
