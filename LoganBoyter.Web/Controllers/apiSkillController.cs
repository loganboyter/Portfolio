using LoganBoyter.Data.Models;
using LoganBoyter.Data.Models.Adapters;
using LoganBoyter.Data.Models.Adapters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Microsoft.AspNet.Identity;

namespace LoganBoyter.Web.Controllers
{
    public class apiSkillController : ApiController
    {
        ISkill _adapter;
        public apiSkillController()
        {
            _adapter = new SkillAdapter();
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_adapter.GetSkills());
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public IHttpActionResult Post(Skill skill)
        {
            _adapter.CreateSkill(skill);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult Put(Skill skill)
        {
            _adapter.EditSkill(skill);
            return Ok();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(Skill skill)
        {
            _adapter.DeleteSkill(skill);
            return Ok();
        }
    }
}
