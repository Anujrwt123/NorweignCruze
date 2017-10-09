using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
   
    public class UserRole
    {
        [Key]
        public int ID { get;set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
}