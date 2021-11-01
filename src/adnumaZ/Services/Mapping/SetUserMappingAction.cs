using adnumaZ.Models;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;

namespace adnumaZ.Services.Mapping
{
    //https://docs.automapper.org/en/stable/Before-and-after-map-actions.html#asp-net-core-and-automapper-extensions-microsoft-dependencyinjection
    public class SetUserMappingAction : IMappingAction<UploadTorrentViewModel, Torrent>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public SetUserMappingAction(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.userManager = userManager;
        }

        public void Process(UploadTorrentViewModel source, Torrent destination, ResolutionContext context)
        {
            destination.Uploader = userManager.GetUserAsync(httpContextAccessor.HttpContext.User).Result;
        }
    }

}
