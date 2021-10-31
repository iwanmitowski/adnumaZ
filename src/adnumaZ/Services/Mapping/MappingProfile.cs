using adnumaZ.Models;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace adnumaZ.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Add mappings here
            this.CreateMap<UploadTorrentViewModel, Torrent>()
                .ForMember(x => x.CreatedOn, y => y.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.Size, y => y.MapFrom(s => s.File.Length / 1024 / 1024.0));
                //.ForMember(x => x.Uploader, y => y.MapFrom(s => userManager.GetUserAsync(httpContextAccessor.HttpContext.User).Result));
        }
    }
}
