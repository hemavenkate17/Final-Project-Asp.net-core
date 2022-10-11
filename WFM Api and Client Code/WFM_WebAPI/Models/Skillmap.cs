namespace WFM_WebAPI.Models
{
    public class Skillmap
    {
        public int employee_id { get; set; }
        public Employee Employees { get; set; }

        public int skillid { get; set; }
        public Skill Skills { get; set; }
    }
}
