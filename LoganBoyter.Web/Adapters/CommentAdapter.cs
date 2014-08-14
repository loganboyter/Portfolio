using LoganBoyter.Data;
using LoganBoyter.Data.Models;
using LoganBoyter.Web.Adapters.Interfaces;
using LoganBoyter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoganBoyter.Web.Adapters
{
    public class CommentAdapter : IComment
    {
        public List<CommentVM> GetComments()
        {
            ApplicationDbContext Db = new ApplicationDbContext();
            List<CommentVM> Comments = Db.Comments.Select(c => new CommentVM()
            {
                Author = Db.Users.Where(u=> u.Id == c.UserId).FirstOrDefault().UserName,
                Body = c.Body,
                TimeStamp = c.Timestamp,
                Title = c.Title
            }).ToList();
            return Comments;
        }

        public Comment CreateComment(Comment comment)
        {
            ApplicationDbContext Db = new ApplicationDbContext();
            comment.Timestamp = DateTime.Now;
            Db.Comments.Add(comment);
            Db.SaveChanges();
            return comment;
        }

        public void DeleteComment(Comment comment)
        {
            ApplicationDbContext Db = new ApplicationDbContext();
            Db.Comments.Remove(Db.Comments.Where(c => c.Id == comment.Id).FirstOrDefault());
            Db.SaveChanges();
        }
    }
}