using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFM_WebAPI.Models
{
    public class Employee
    {
        [Key]
        public int employee_id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string manager { get; set; }
        public string wfm_manager { get; set; }

        public string email { get; set; }

        public string lockstatus { get; set; }
        public int experience { get; set; }
        public int profile_id { get; set; }

        public List<Skillmap> Skillmaps { get; set; }


    }
    public class Employees_Skills
    {
        [Key]
        public int Employee_id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Manager { get; set; }
        public string Wfm_manager { get; set; }
        public string Email { get; set; }
        public string Lockstatus { get; set; }
        public int Experience { get; set; }
        [NotMapped]
        public List<string> Skills { get; set; }
    }
}
