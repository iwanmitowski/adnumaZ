using adnumaZ.Data;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace adnumaZ.ViewComponents
{
    public class RecentTorrentsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public RecentTorrentsViewComponent(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = mapper
                .Map<IEnumerable<TorrentViewModel>>(
                    dbContext.Torrents.Include(x => x.Uploader)
                             .OrderByDescending(x => x.CreatedOn)
                             .Take(5));

            return this.View(viewModel);
        }
    }
}
