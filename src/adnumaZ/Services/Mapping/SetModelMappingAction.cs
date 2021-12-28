using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace adnumaZ.Services.Mapping
{
    //https://docs.automapper.org/en/stable/Before-and-after-map-actions.html#asp-net-core-and-automapper-extensions-microsoft-dependencyinjection
    public class SetModelMappingAction : IMappingAction<UploadTorrentViewModel, Torrent>,
                                        IMappingAction<CommentInputModel, Comment>,
                                        IMappingAction<Comment, CommentViewModel>,
                                        IMappingAction<Torrent, TorrentViewModel>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext dbContext;

        public SetModelMappingAction(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public void Process(UploadTorrentViewModel source, Torrent destination, ResolutionContext context)
        {
            destination.Uploader = GetUser();
        }

        public void Process(CommentInputModel source, Comment destination, ResolutionContext context)
        {
            destination.User = source.User;
            destination.Parent = GetParentComment(source.ParentId);
            destination.Torrent = GetTorrent(source.TorrentId);
        }

        public void Process(Comment source, CommentViewModel destination, ResolutionContext context)
        {
            destination.User = GetUser(source.UserId);
        }

        public void Process(Torrent source, TorrentViewModel destination, ResolutionContext context)
        {
            var user = GetUser();

            destination.IsFavourited = source.UserFavouritedTorrents.Any(uft => uft.User == user && uft.Torrent == source );
        }
        
        private User GetUser(string userId) => dbContext.UserAccounts.FirstOrDefault(x => x.Id == userId);
        private Torrent GetTorrent(int torrentId) => dbContext.Torrents.FirstOrDefault(x => x.Id == torrentId);
        private Comment GetParentComment(string parentId) => dbContext.Comments.FirstOrDefault(x => x.Id == parentId);
        private User GetUser() => userManager.GetUserAsync(httpContextAccessor.HttpContext.User).Result;

    }

}
