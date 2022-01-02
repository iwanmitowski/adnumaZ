using adnumaZ.Common.Constants;
using adnumaZ.Models;
using adnumaZ.ViewModels;

using AutoMapper;

using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace adnumaZ.Services.Mapping
{
    public class MappingProfile : Profile
    {
        private const int StringMaxLength = 20;

        public MappingProfile()
        {
            //Add mappings here

            this.CreateMap<UploadTorrentViewModel, Torrent>()
                .ForMember(x => x.CreatedOn, y => y.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.Size, y => y.MapFrom(s => s.File.Length / 1024 / 1024.0))
                .ForMember(x => x.ImageUrl, y => y.MapFrom(s => s.ImageUrl != null ? s.ImageUrl : Constants.DefaultTorrentImage))
                .AfterMap<SetModelMappingAction>(); //Mapping the uploader

            this.CreateMap<Torrent, TorrentViewModel>()
                .ForMember(x => x.DescriptionShort, y => y.MapFrom(s => GetShortParameter(s.Description)))
                .ForMember(x => x.TitleShort, y => y.MapFrom(s => GetShortParameter(s.Title)))
                .ForMember(x => x.Size, y => y.MapFrom(s => s.Size.ToString("F4")))
                .ForMember(x => x.Uploader, y => y.MapFrom(s => s.Uploader))
                .ForMember(x => x.Hash, y => y.MapFrom(s => s.Hash))
                .ForMember(x => x.UserDownloadedTorrents, y => y.MapFrom(s => s.UserDownloadedTorrents))
                .AfterMap<SetModelMappingAction>();

            this.CreateMap<User, UserViewModel>()
                .ForMember(x => x.DownloadedTorrentGBs, y => y.MapFrom(s => s.UserDownloadedTorrents.Sum(t => t.Torrent.Size)))
                .ForMember(x => x.UploadedTorrentGBs, y => y.MapFrom(s => s.UploadedTorrents.Sum(t => t.Size)))
                .ForMember(x => x.DownloadedTorrentsCount, y => y.MapFrom(s => s.UserDownloadedTorrents.Count(udt => udt.UserId == s.Id)))
                .ForMember(x => x.UploadedTorrentsCount, y => y.MapFrom(s => s.UploadedTorrents.Count()))
                .ForMember(x => x.FavouriteTorrentsCount, y => y.MapFrom(s => s.UserFavouritedTorrents.Count()))
                .AfterMap<SetIsAdminMappingAction>();

            this.CreateMap<CommentInputModel, Comment>()
                .ForMember(x => x.Id, y => y.MapFrom(s => Guid.NewGuid().ToString()))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(s => DateTime.UtcNow))
                .AfterMap<SetModelMappingAction>(); //Mapping the commenter

            this.CreateMap<Comment, CommentViewModel>()
                .AfterMap<SetModelMappingAction>();

            this.CreateMap<Torrent, EditTorrentViewModel>();

            this.CreateMap<EditTorrentViewModel, Torrent>()
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(s => DateTime.UtcNow))
                .ForMember(x => x.IsApproved, y => y.MapFrom(s => false));
        }

        private string GetShortParameter(string i) =>
            i == null ? string.Empty :
            i.Length > 0 && i.Length < StringMaxLength ? i :
            i.Substring(0, Math.Min(i.Length, StringMaxLength)) + "...";
    }
}
