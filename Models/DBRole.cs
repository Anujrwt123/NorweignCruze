using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    [Table("webpages_Roles")]
    public class DBRole
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}