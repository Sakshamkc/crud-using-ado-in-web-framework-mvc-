using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication29.Models;

namespace WebApplication29.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            List<StudentModelController> lst = new List<StudentModelController>();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; Integrated Security=true; Database =Saksham");
            SqlCommand cmd = new SqlCommand("select *from tblstudent", con);
            con.Open();
            SqlDataReader dr = null;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lst.Add(new StudentModelController()
                {
                    StudentId = (int)dr["StudentId"],
                    Fullname = (string)dr["Fullname"],
                    Email = (string)dr["Email"],
                    Phone = (string)dr["Phone"]

                });
            }
            dr.Close();
            con.Close();
            return View(lst);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentModelController stu)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; Integrated Security=true; Database =Saksham");
            SqlCommand cmd = new SqlCommand("insert into tblstudent values(@a,@b,@c)", con);
            cmd.Parameters.AddWithValue("@a", stu.Fullname);
            cmd.Parameters.AddWithValue("@b", stu.Email);
            cmd.Parameters.AddWithValue("@c", stu.Phone);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.Message = "Created";
            return View();
        }
        public IActionResult Edit(int id)
        {
            StudentModelController student = new StudentModelController();
            SqlConnection con = new SqlConnection(@"Data source= (LocalDB)\MSSQLLocalDB; Integrated Security=true; Database=Saksham");
            SqlCommand cmd = new SqlCommand("select *from tblstudent where StudentId=@a", con);
            cmd.Parameters.AddWithValue("@a", id);
            con.Open();
            SqlDataReader dr = null;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                student.StudentId = (int)dr["StudentId"];
                student.Fullname = (string)dr["Fullname"];
                student.Email = (string)dr["Email"];
                student.Phone = (string)dr["Phone"];
            }
            dr.Close();
            con.Close();
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(StudentModelController stu)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; Integrated Security=true; Database =Saksham");
            SqlCommand cmd = new SqlCommand("update tblstudent set Fullname=@a,Email=@b,Phone=@c where StudentId=@d", con);
            cmd.Parameters.AddWithValue("@a", stu.Fullname);
            cmd.Parameters.AddWithValue("@b", stu.Email);
            cmd.Parameters.AddWithValue("@c", stu.Phone);
            cmd.Parameters.AddWithValue("@d", stu.StudentId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.Message = "Updated";
            return View();
        }
        public IActionResult Delete(int id)
        {
            StudentModelController student = new StudentModelController();
            SqlConnection con = new SqlConnection(@"Data source= (LocalDB)\MSSQLLocalDB; Integrated Security=true; Database=Saksham");
            SqlCommand cmd = new SqlCommand("select *from tblstudent where StudentId=@a", con);
            cmd.Parameters.AddWithValue("@a", id);
            con.Open();
            SqlDataReader dr = null;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                student.StudentId = (int)dr["StudentId"];
                student.Fullname = (string)dr["Fullname"];
                student.Email = (string)dr["Email"];
                student.Phone = (string)dr["Phone"];
            }
            dr.Close();
            con.Close();
            return View(student);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult Delete_Post (int id)
        {
            StudentModelController student =new StudentModelController();
            SqlConnection con = new SqlConnection(@"Data source= (LocalDB)\MSSQLLocalDB; Integrated Security=true; Database=Saksham");
            SqlCommand cmd = new SqlCommand("delete from tblstudent where StudentId=@a", con);
            cmd.Parameters.AddWithValue("@a", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Index");
        }

    }
}
