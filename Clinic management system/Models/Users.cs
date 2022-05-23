using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class Users
    {
        [Required(ErrorMessage ="Please Enter Your UserName")]

        public string UserName { get; set; }
        [Required(ErrorMessage ="Enter your Password")]
        public string Password { get; set; }

    }
}
