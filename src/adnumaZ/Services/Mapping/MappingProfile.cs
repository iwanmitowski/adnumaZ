using adnumaZ.Models;
using adnumaZ.ViewModels;
using AutoMapper;
using System;

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
                .AfterMap<SetUserMappingAction>(); //Mapping the uploader

            this.CreateMap<Torrent, TorrentViewModel>()
                .ForMember(x => x.Description, y => y.MapFrom(s => GetShortParameter(s.Description)))
                .ForMember(x => x.TitleShort, y => y.MapFrom(s => GetShortParameter(s.Title)))
                .ForMember(x => x.Size, y => y.MapFrom(s => s.Size.ToString("F4")))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(s => s.CreatedOn.ToShortDateString()));
        }

        private string GetShortParameter(string i) => 
            i == null ? string.Empty :
            i.Length > 0 && i.Length < StringMaxLength ? i :
            i.Substring(0, Math.Min(i.Length, StringMaxLength)) + "...";
    }
}
