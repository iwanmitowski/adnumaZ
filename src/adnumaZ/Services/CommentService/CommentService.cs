using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.Services.CommentService.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace adnumaZ.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext dbContext;

        public CommentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddCommentToParentChildComments(Comment comment)
        {
            if (comment.Parent != null)
            {
                comment.Parent.ChildComments.Add(comment);
            }
        }

        public void AddCommentToTorrent(Comment comment)
        {
            comment.Torrent.Comments.Add(comment);
        }

        public void AddCommentToUser(Comment comment, User user)
        {
            user.Comments.Add(comment);
        }

        public async Task<Comment> GetCommentByIdAsync(string commentId)
        {
            return await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
        }

        public async Task SetCommentAsDeletedAsync(Comment comment)
        {
            comment.IsDeleted = true;
            comment.DeletedOn = DateTime.UtcNow;
            comment.ModifiedOn = comment.DeletedOn;

            await dbContext.SaveChangesAsync();
        }
    }
}
