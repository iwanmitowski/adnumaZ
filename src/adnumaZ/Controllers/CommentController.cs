using adnumaZ.Data;
using adnumaZ.Data.Migrations;
using adnumaZ.Models;
using adnumaZ.Services.CommentService.Contracts;
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
        private readonly ICommentService commentService;

        public CommentController(ApplicationDbContext dbContext, IMapper mapper, IUserService userService, ICommentService commentService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userService = userService;
            this.commentService = commentService;
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

            try
            {
                var comment = mapper.Map<Comment>(input);

                commentService.AddCommentToParentChildComments(comment);

                commentService.AddCommentToTorrent(comment);

                var user = await dbContext.UserAccounts
                    .Include(x => x.Comments)
                    .FirstOrDefaultAsync(
                        x => x.Equals(userService.GetCurrentUserAsync().Result));

                commentService.AddCommentToUser(comment, user);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(TorrentController.ById),
                                    nameof(TorrentController).Replace("Controller", string.Empty),
                                    new { id = input.TorrentId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(CommentDeleteModel input)
        {
            var comment = await commentService.GetCommentByIdAsync(input.CommentId);

            await commentService.SetCommentAsDeletedAsync(comment);

            return RedirectToAction(nameof(TorrentController.ById),
                                    nameof(TorrentController).Replace("Controller", string.Empty),
                                    new { id = input.TorrentId });
        }
    }
}
