using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WFM_WebAPI.Models
{
    public class Skill
    {
        [Key]
        public int skillid { get; set; }
        public string name { get; set; }


        public List<Skillmap> Skillmaps { get; set; }
    }

    public class Skills_Employees
    {
        public int skillid { get; set; }
        public string name { get; set; }
        public List<string> Employees { get; set; }

    }
   
}
