﻿using adnumaZ.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace adnumaZ.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> UserAccounts { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Torrent> Torrents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserDownloadedTorrent> UserDownloadedTorrents { get; set; }
        public DbSet<UserFavouritedTorrent> UserFavouritedTorrents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserFavouritedTorrent>()
                .HasKey(uft => new { uft.TorrentId, uft.UserId });

            builder.Entity<UserFavouritedTorrent>()
                .Property(uft => uft.FavouritedAt);

            builder.Entity<UserDownloadedTorrent>()
                .Property(udt => udt.DownloadedAt);

            builder.Entity<User>()
                .HasMany(t => t.UploadedTorrents)
                .WithOne(c => c.Uploader);

            builder.Entity<User>()
                .Property(u => u.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow);

            base.OnModelCreating(builder);
        }
    }
}
