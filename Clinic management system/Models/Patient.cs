using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        [Required(ErrorMessage = "Please enter First name")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Not allowed(Space,Numbers,Special Characters)")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter First name")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Not allowed(Space,Numbers,Special Characters)")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Please Choose your Gender")]
        public string Sex { get; set; }
        [Required(ErrorMessage ="Enter your age")]      
        public int Age { get; set; }
        [Required(ErrorMessage = "Choose your Date Of Birth")]
        public string DateofBirth { get; set; }
        public string PatientName { get; set; }
    }
}
