using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoganBoyter.Web.Models
{
    public class CommentVM
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}