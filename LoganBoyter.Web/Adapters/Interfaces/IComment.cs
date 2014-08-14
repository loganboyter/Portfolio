using LoganBoyter.Data.Models;
using LoganBoyter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganBoyter.Web.Adapters.Interfaces
{
    public interface IComment
    {
        List<CommentVM> GetComments();
        Comment CreateComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
