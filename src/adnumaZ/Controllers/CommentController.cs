using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CommentController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CommentInputModel input)
        {
            var comment = mapper.Map<Comment>(input);

            var comments = await dbContext.Comments.AddAsync(mapper.Map<Comment>(input));
            await dbContext.SaveChangesAsync();

            return View();
        }

        public IActionResult All()
        {
            var comments = dbContext.Comments.Include(x=>x.User);

            return View(comments);
        }
    }
}
