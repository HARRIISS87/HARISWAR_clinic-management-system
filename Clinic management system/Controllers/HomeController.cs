using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagementSystem.DAL;

namespace ClinicManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()//Login Page
        {
            return View();
        }
        [Route("Home/Validate")]
        public IActionResult Validate(Users chk) //this to be given in index validate,customer
        {
            if (ModelState.IsValid)
            {
                ClinicDAL chkobj = new ClinicDAL();
                int result = chkobj.CheckUser(chk);//CheckUser in Dal method ,used to validate Username and password
                if (result == 1)
                {
                    return RedirectToAction("Homepage");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Incorrect Username or Password');</script>";
                    return View("Index");
                   
                }

            }
            else
            {
                return View("Index");
            }
        }
        //public IActionResult ChkUser(Users chk)
        //{
        //    ClinicDAL chkobj = new ClinicDAL();
        //    int result = chkobj.CheckUser(chk);
        //    if (result == 1)
        //        return RedirectToAction("HomePage");
        //    else
            
        //    return View("Index");
        //}
        
        public IActionResult HomePage()//This method will view all the modules that we can access,literally 'Home';
        {
            ClinicDAL SDALobj = new ClinicDAL();
            List<Doctor> DoctorList = new List<Doctor>();
            DoctorList = SDALobj.ShowDoctors();//ShowDoctors method in DAL is used to Show the available doctors
            return View(DoctorList);
        }
        
        
        public IActionResult DoctorPage()//This View is used to receive values and send to AddDoctor action
        {
            return View();
        }
        public IActionResult AddDoctor(Doctor doc)//doc:parameter for AddDoctor;CDALobj:object for AddDoctor
        {
            if (ModelState.IsValid)
            {
                ClinicDAL CDALobj = new ClinicDAL();
                int result = CDALobj.NewDoctor(doc);//Calling the method(NewDoctor) by using object and passing with the values 
                if (result == 1)
                {
                    TempData["msg"] = "<script>alert('Doctor Added Successfully');</script>";
                    return RedirectToAction("DoctorPage");
                }
                else
                {
                    return View("DoctorPage");
                }
            }
            else
            {
                return View("HomePage");
            }
        }
        public IActionResult DelDoc(int id)
        {
            ClinicDAL DelDocobj = new ClinicDAL();
            int result = DelDocobj.deletedoc(id);
            if (result == 1)
            {
                //TempData["msg"] = "<script>alert('Patient Removed');</script>";
                return RedirectToAction("HomePage");
            }
            else
            {
                return View("HomePage");
            }
        }
        public IActionResult PatientPage()//This View is used to receive values of the patient page
        {
            return View();
        }
        public IActionResult AddPatient(Patient ptnts)//ptnts:parameter is ued to store the values inside parameter and using it throu
        {
            if (ModelState.IsValid)
            {
                ClinicDAL CDALobj = new ClinicDAL();
                int result = CDALobj.NewPatient(ptnts);
                if (result == 1)
                {
                    TempData["msg"] = "<script>alert('Patient Added Successfully');</script>";
                    return RedirectToAction("PatientPage");
                }
                else
                    TempData["msg"] = "<script>alert('Patient already exist');</script>";
                    return View("PatientPage");
            }
            else
            {
                return View("HomePage");
            }
        }
        public IActionResult ViewPatient()//This view will the Patients
        {
            ClinicDAL Poobj = new ClinicDAL();
            List<Patient> PatientList = new List<Patient>();
            PatientList = Poobj.GetPatients();
            return View(PatientList);
        }
        public IActionResult DelPatient(int id)//Deleting by id
        {
            try
            {
                ClinicDAL CNLobj = new ClinicDAL();
                int result = CNLobj.deleteit(id);
                if (result == 1)
                {
                    //TempData["msg"] = "<script>alert('Patient Removed');</script>";
                    return RedirectToAction("ViewPatient");
                }
                else
                {
                    return View("ViewPatient");
                }
            }
            catch
            {
                //TempData["msg"] = "<script>alert('Patient does not exist');</script>";
                return RedirectToAction("ViewPatient");
            }
        }

        public IActionResult ScheduleAppmnt(/*int Docid*/)
        {
            //ClinicDAL showdoc = new ClinicDAL();
            //List<Doctor> shoDoc = new List<Doctor>();
            //shoDoc = showdoc.PassDoctor(Docid);
            //ViewBag.DoctorName = Doc;
            //ViewBag.Specialization = Spec;
            //ViewBag.VisitingHours = Vishrs;
            return View();
        }

        public IActionResult AddSchd(Schedules sch)
        {
            try
            {
            ClinicDAL SDALobj = new ClinicDAL();
            int result = SDALobj.NewSchedule(sch);
            
            if (result == 1)
            {
                TempData["msg"] = "<script>alert('Scheduled Successfully');</script>";
                return RedirectToAction("ScheduleAppmnt");
            }
            else
            {
                return View("ScheduleAppmnt");
            }
            }
            catch
            {
                TempData["msg"] = "<script>alert('Patient does not exist');</script>";
                return RedirectToAction("ScheduleAppmnt");
            }
        }
        public IActionResult ViewAppmnt()
        {
            ClinicDAL VDALobj = new ClinicDAL();
            List<Schedules> ScheduleList = new List<Schedules>();
            ScheduleList = VDALobj.GetAllAppmnts();
            return View(ScheduleList);
        }
        public IActionResult CancelAppmnt(int id)
        {
            ClinicDAL CNLobj = new ClinicDAL();
            int result = CNLobj.CnclAppmnt(id);
            if (result == 1)
            {
                //TempData["msg"] = "<script>alert('Appointment Canceled');</script>";
                return RedirectToAction("ViewAppmnt");
            }
            else
            {
                return View("ViewAppmnt");
            }
        }
        public IActionResult DelByDat()
        {
            //ClinicDAL Dobj = new ClinicDAL();
            //List<Schedules> ScheduleList = new List<Schedules>();
            //ScheduleList = Dobj.GetAllAppmntss();
            return View();
        }
        public IActionResult DeleteDateView(Schedules dat)
        {
            ClinicDAL Dobj = new ClinicDAL();
            List<Schedules> ScheduleList = new List<Schedules>();
            ScheduleList = Dobj.DelByVis(dat);
            return View(ScheduleList);
            //ClinicDAL DelAppobj = new ClinicDAL();
            //int result = DelAppobj.DelByVis(date);
            //if (result == 1)
            //{
            //    return RedirectToAction("DelByDat");
            //}
            //else
            //{
            //    return View("ViewAppmnt");
            //}
        }
        //public IActionResult DeleteByDat()
        //{
        //    ClinicDAL DelAppobj = new ClinicDAL();
        //    int result = DelAppobj.DelByVis();
        //    if (result == 1)
        //    {
        //        return RedirectToAction("ViewAppmnt");
        //    }
        //    else
        //    {
        //        return View("DelByDat");
        //    }
        //}
        //public IActionResult Cnfacanappmnt()
        //{
        //    return View();
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
