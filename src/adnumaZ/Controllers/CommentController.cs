using adnumaZ.Data;
using adnumaZ.Data.Migrations;
using adnumaZ.Models;
using adnumaZ.Services.UserService.Contracts;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public CommentController(ApplicationDbContext dbContext, IMapper mapper, IUserService userService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentInputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Content))
            {
                return RedirectToAction(nameof(TorrentController.ById),
                                      nameof(TorrentController).Replace("Controller", string.Empty),
                                      new { id = input.TorrentId });
            }

            var comment = mapper.Map<Comment>(input);

            if (comment.Parent != null)
            {
                comment.Parent.ChildComments.Add(comment);
            }

            comment.Torrent.Comments.Add(comment);
            var user = await dbContext.UserAccounts
                .Include(x=>x.Comments)
                .FirstOrDefaultAsync(
                    x=>x.Equals(userService.GetCurrentUserAsync().Result));
            user.Comments.Add(comment);

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(TorrentController.ById),
                                    nameof(TorrentController).Replace("Controller", string.Empty),
                                    new { id = input.TorrentId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(CommentDeleteModel input)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == input.CommentId);

            comment.IsDeleted = true;
            comment.DeletedOn = DateTime.UtcNow;
            comment.ModifiedOn = comment.DeletedOn;

            await dbContext.SaveChangesAsync();


            return RedirectToAction(nameof(TorrentController.ById),
                                    nameof(TorrentController).Replace("Controller", string.Empty),
                                    new { id = input.TorrentId });
        }
    }
}
