using LoganBoyter.Data.Models.Adapters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoganBoyter.Data;
using LoganBoyter.Data.Models;

namespace LoganBoyter.Data.Models.Adapters
{
    public class SkillAdapter : ISkill
    {
        public List<Skill> GetSkills()
        {
            ApplicationDbContext Db = new ApplicationDbContext(); // y u no resolve
            return Db.Skills.ToList();
        }

        public Skill CreateSkill(Skill skill)
        {
            ApplicationDbContext Db = new ApplicationDbContext();
            Db.Skills.Add(skill);
            Db.SaveChanges();
            return skill;
        }

        public Skill EditSkill(Skill skill)
        {
            ApplicationDbContext Db = new ApplicationDbContext();
            Skill Skill = Db.Skills.Where(s => s.Id == skill.Id).FirstOrDefault();
            Skill.Title = skill.Title;
            Skill.Description = skill.Description;
            Skill.Icon = skill.Icon;
            Db.SaveChanges();
            return Skill;
        }

        public void DeleteSkill(Skill skill)
        {
            ApplicationDbContext Db = new ApplicationDbContext();
            Db.Skills.Remove(Db.Skills.Where(s => s.Id == skill.Id).FirstOrDefault());
            Db.SaveChanges();
        }
    }
}
