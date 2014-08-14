using LoganBoyter.Data.Models;
using LoganBoyter.Web.Adapters;
using LoganBoyter.Web.Adapters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace LoganBoyter.Web.Controllers
{
    public class apiCommentController : ApiController
    {
        IComment _adapter;
        public apiCommentController()
        {
            _adapter = new CommentAdapter();
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Comment comment)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("wallbookinfo@gmail.com", "codercamps");

                MailMessage msg = new MailMessage();
                msg.To.Add("logan_boyter@hotmail.com");
                msg.From = new MailAddress("Info@LoganBoyter.com");
                msg.Subject = "New comment on LoganBoyter.com";
                msg.Body = "You have a new comment on LoganBoyter.com from user" + User.Identity.GetUserName() + ". The message is \"" + comment.Body + ".\"";

                client.Send(msg);
            }
            catch (Exception ex)
            {

            }
            comment.UserId = User.Identity.GetUserId();
            _adapter.CreateComment(comment);
            return Ok(comment);
        }
        [Authorize(Roles="Admin")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_adapter.GetComments());
        }
    }
}
