using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganBoyter.Data.Models.Adapters.Interfaces
{
    public interface ISkill
    {
        List<Skill> GetSkills();
        Skill CreateSkill(Skill skill);
        Skill EditSkill(Skill skill);
        void DeleteSkill(Skill skill);
    }
}
