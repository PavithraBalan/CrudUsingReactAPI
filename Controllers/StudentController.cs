using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CrudUsingReact.Models;
using System.Net;
using System.Net.Http;

namespace CrudUsingReact.Controllers
{
    [RoutePrefix("Api/Student")]
    public class StudentController : ApiController
    {
        // GET: Student
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ReactCRUDEntities DB = new ReactCRUDEntities();

        [Route("AddorUpdateStudent")]
        [HttpPost]
        public object AddorUpdateStudent(Student st)
        {
            try
            {
                if (st.Id == 0)
                {
                    StudentMaster sm = new StudentMaster();
                    sm.Name = st.Name;
                    sm.RollNo = st.RollNo;
                    sm.Address = st.Address;
                    sm.Class = st.Class;
                    DB.StudentMasters.Add(sm);
                    DB.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Added Successfully"
                    };
                }
                else
                {
                    var obj = DB.StudentMasters.Where(x => x.Id == st.Id).ToList().FirstOrDefault();
                    if (obj.Id > 0)
                    {

                        obj.Name = st.Name;
                        obj.RollNo = st.RollNo;
                        obj.Address = st.Address;
                        obj.Class = st.Class;
                        DB.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfully"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };

        }
        [Route("StudentDetails")]
        [HttpGet]
        public object StudentDetails()
        {

            var a = DB.StudentMasters.ToList();
            return a;
        }

        [Route("StudentDetailById")]
        [HttpGet]
        public object StudentDetailById(int id)
        {
            var obj = DB.StudentMasters.Where(x => x.Id == id).ToList().FirstOrDefault();
            return obj;
        }
        [Route("DeleteStudent")]
        [HttpDelete]
        public object DeleteStudent(int id)
        {
            var obj = DB.StudentMasters.Where(x => x.Id == id).ToList().FirstOrDefault();
            DB.StudentMasters.Remove(obj);
            DB.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
    }
}