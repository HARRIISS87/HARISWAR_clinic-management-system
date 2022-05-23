using ClinicManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace ClinicManagementSystem.DAL
{
    public class ClinicDAL
    {
        public string cnn = "";

        public ClinicDAL()
        {
            var builder = new ConfigurationBuilder().SetBasePath
               (Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cnn = builder.GetSection("ConnectionStrings:Conn").Value;
        }

        public int CheckUser(Users chk)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("ChkUsr", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", chk.UserName);
            cmd.Parameters.AddWithValue("@Password", chk.Password);
            con.Open();
            IDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                return (1);

            con.Close();
            return (0);
        }

        public int NewDoctor(Doctor doc)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("AddDoctor", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fname", doc.FirstName);
            cmd.Parameters.AddWithValue("@lname", doc.LastName);
            cmd.Parameters.AddWithValue("@sex", doc.Sex);
            cmd.Parameters.AddWithValue("@spec", doc.Specializations);
            cmd.Parameters.AddWithValue("@visithrs", doc.VisitingHours);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int NewPatient(Patient ptnts)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("AddPatient", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@fname", ptnts.FirstName);
            cmd.Parameters.AddWithValue("@lname", ptnts.LastName);
            cmd.Parameters.AddWithValue("@sex", ptnts.Sex);
            //cmd.Parameters.AddWithValue("@age", ptnts.Age);
            cmd.Parameters.AddWithValue("@dob", ptnts.DateofBirth);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public List<Patient> GetPatients()
        {
            List<Patient> listpatients = new List<Patient>();
            using (SqlConnection con = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("GetPatients", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listpatients.Add(new Patient()
                        {
                            PatientID = int.Parse(reader["PatientID"].ToString()),
                            //FirstName = reader["FirstName"].ToString(),
                            //LastName = reader["LastName"].ToString(),
                            PatientName = reader["PatientName"].ToString(),
                            Sex = reader["Sex"].ToString(),
                            Age = int.Parse(reader["Age"].ToString()),
                            DateofBirth = reader["DateofBirth"].ToString(),


                        }); ;
                    }

                }
            }
            return listpatients;
        }
        public int deleteit(int id)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("Delpat", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int NewSchedule(Schedules sch)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("Addsch", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", sch.PatientID);
            cmd.Parameters.AddWithValue("@pname", sch.PatientName);
            cmd.Parameters.AddWithValue("@spec", sch.Specializations);
            cmd.Parameters.AddWithValue("@DName", sch.DoctorName);
            cmd.Parameters.AddWithValue("@VD", sch.VisitDate);
            cmd.Parameters.AddWithValue("@appmntime", sch.AppointmentTime);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public List<Schedules> GetAllAppmnts()
        {
            List<Schedules> listSchedules = new List<Schedules>();
            using (SqlConnection con = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("GetAppmnt", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listSchedules.Add(new Schedules()
                        {
                            AppointmentID = int.Parse(reader["AppointmentID"].ToString()),
                            PatientID = int.Parse(reader["PatientID"].ToString()),
                            PatientName = reader["PatientName"].ToString(),
                            Specializations = reader["Specializations"].ToString(),
                            DoctorName = reader["DoctorName"].ToString(),
                            VisitDate = reader["VisitDate"].ToString(),
                            AppointmentTime = reader["AppointmentTime"].ToString()
                        }); ; 
                    }

                }
            }
            return listSchedules;
        }
        public List<Doctor> ShowDoctors()
        {
            List<Doctor> docSchedules = new List<Doctor>();
            using (SqlConnection con = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("ShowDoc", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        docSchedules.Add(new Doctor()
                        {
                            DoctorID =int.Parse(reader["DoctorID"].ToString()),
                            //FirstName = reader["FirstName"].ToString(),
                            DoctorName = reader["DoctorName"].ToString(),
                            Specializations = reader["Specializations"].ToString(),
                            VisitingHours = reader["VisitingHours"].ToString()
                        });
                    }

                }
            }
            return docSchedules;
        }
        //public List<Doctor> PassDoctor(int Docid)
        //{
        //    List<Doctor> showdoctorss = new List<Doctor>();
        //    using (SqlConnection con = new SqlConnection(cnn))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("PassDoc", con))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@id", Docid);
        //            if (con.State == ConnectionState.Closed)
        //                con.Open();
        //            IDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                showdoctorss.Add(new Doctor()
        //                {
        //                    //DoctorID = int.Parse(reader["DoctorID"].ToString()),
        //                    FirstName = reader["FirstName"].ToString(),
        //                    //DoctorName = reader["DoctorName"].ToString(),
        //                    Specializations = reader["Specializations"].ToString(),
        //                    VisitingHours = reader["VisitingHours"].ToString()
        //                });
        //            }

        //        }
        //    }
        //    return showdoctorss;
        //}
        public int deletedoc(int id)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("DelDoctor", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int CnclAppmnt(int id)
        {
            SqlConnection con = new SqlConnection(cnn);
            SqlCommand cmd = new SqlCommand("Delsch", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public List<Schedules> DelByVis(Schedules dat)
        {
            List<Schedules> datBySchedules = new List<Schedules>();
            using (SqlConnection con = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("ShowByDate", con))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.Parameters.AddWithValue("@vd", dat.VisitDate);
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        datBySchedules.Add(new Schedules()
                        {

                            //FirstName = reader["FirstName"].ToString(),
                            PatientID = int.Parse(reader["PatientID"].ToString()),
                            DoctorName = reader["DoctorName"].ToString(),
                            Specializations = reader["Specializations"].ToString(),
                            VisitDate = reader["VisitDate"].ToString()
                        }); 
                    }

                }
            }
            return datBySchedules;
        }
    }
}
