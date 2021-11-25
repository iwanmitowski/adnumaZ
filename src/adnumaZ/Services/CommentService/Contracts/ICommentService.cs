using adnumaZ.Models;
using System.Threading.Tasks;

namespace adnumaZ.Services.CommentService.Contracts
{
    public interface ICommentService
    {
        public Task<Comment> GetCommentByIdAsync(string commentId);

        public Task SetCommentAsDeletedAsync(Comment comment);

        public void AddCommentToParentChildComments(Comment comment);

        public void AddCommentToUser(Comment comment, User user);
        public void AddCommentToTorrent(Comment comment);
    }
}
