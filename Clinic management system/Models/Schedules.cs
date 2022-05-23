using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class Schedules
    {
        public int AppointmentID { get; set; }
        [Required(ErrorMessage ="Enter existing Patient Id")]
        public int PatientID { get; set; }
        [Required(ErrorMessage = "Please enter First name")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Not allowed(Space,Numbers,Special Characters)")]
        public string PatientName { get; set; }
        [Required(ErrorMessage = "Choose Specializations")]
        public string Specializations { get; set; }
        [Required(ErrorMessage ="Choose DoctorName")]
        public string DoctorName { get; set; }
        [Required(ErrorMessage ="Please Choose Visit Date")]
        public string VisitDate { get; set; }
        [Required(ErrorMessage ="Please Choose Appointment Time")]
        public string AppointmentTime { get; set; }
    }
}
