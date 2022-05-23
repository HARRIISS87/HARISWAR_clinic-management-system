using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ClinicManagementSystem.Models
{
    public class Doctor
    {
        [Required(ErrorMessage ="Please enter First name")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Not allowed(Space,Numbers,Special Characters)")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter Last name")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Not allowed(Space,Numbers,Special Characters)")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Choose Gender")]
        public string Sex { get; set; }
        [Required(ErrorMessage ="Choose Specializations")]
        public string Specializations { get; set; }
        [Required(ErrorMessage ="Choose VisitingHours")]
        public string VisitingHours { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
    }
}
