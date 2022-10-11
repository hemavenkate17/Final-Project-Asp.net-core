using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFM_WebAPI.Models
{
    public class Softlock
    {
        public int employee_id { get; set; }

        public string manager { get; set; }
        public DateTime reqdate { get; set; }

        public string status { get; set; }
        public DateTime lastupdated { get; set; }

        [Key]
        public int lockid { get; set; }
        public string requestmessage { get; set; }
        public string wfmremark { get; set; }
        public string managerstatus { get; set; }
        public string mgrstatuscomment { get; set; }
        public DateTime mgrlastupdate { get; set; }
    }

    public class SoftlockRequest
    {
        public int employee_id { get; set; }

        public string manager { get; set; }
        public DateTime reqdate { get; set; }

        [Key]
        public int lockid { get; set; }
        public string status { get; set; }
        public string requestmessage { get; set; }
    }
}
