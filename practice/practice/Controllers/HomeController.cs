using practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using practice.Controllers;
using System.Data;
using System.IO;

namespace practice.Controllers
{
    public class HomeController : Controller
    {
        Database1Entities1 db = new Database1Entities1();
       public ActionResult employee()
        {
            ViewBag.employee = db.tbl_employee.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult employee(tbl_employee data,HttpPostedFileBase[] files,FormCollection frm)
        {
            try
            {
                string field = Request.Form["HOBBY"];


                if (data.ID > 0)
                {
                    var emp = db.tbl_employee.Where(i => i.ID == data.ID).SingleOrDefault();
                    if(data.NAME !=null)
                    {
                        emp.NAME = data.NAME;
                    }
                    if (data.EMAIL != null)
                    {
                        emp.EMAIL = data.EMAIL;
                    }
                    if (data.PASSWORD != null)
                    {
                        emp.PASSWORD = data.PASSWORD;
                    }
                    if (data.NUMBER != null)
                    {
                        emp.NUMBER = data.NUMBER;
                    }
                    if (data.EMAIL != null)
                    {
                        emp.EMAIL = data.EMAIL;
                    }
                    if (data.GENDER != null)
                    {
                        emp.GENDER = data.GENDER;
                    }
                    if (data.BIRTHDATE != null)
                    {
                        emp.BIRTHDATE = data.BIRTHDATE;
                    }

                    if (data.IMAGE != null)
                    {
                        if (Request.Files.Count > 0)
                        {
                            var Inputfile = Request.Files[0];
                            if (Inputfile != null && Inputfile.ContentLength > 0)
                            {
                                var filename = Path.GetFileName(Inputfile.FileName);
                                var path = Path.Combine(Server.MapPath("~/img/"), filename);

                                Inputfile.SaveAs(path);
                                data.IMAGE = filename;
                            }
                        }
                        emp.IMAGE = data.IMAGE;
                    }

                    if (data.CITY != null)
                    {
                        emp.CITY = data.CITY;
                    }
                    if (data.HOBBY != null)
                    {
                        //for (var i = 0; i < field.Length; i++)
                        //{
                        //    var item = field[i];

                        //}
                        emp.HOBBY = field;
                        
                    }
                    db.SaveChanges();
                }
                else
                {               
                    //for(var i=0; i<field.Length; i++)
                    //{
                    //    var item = field[i];
                    //    data.HOBBY = item;
                    //}   
                  
                    if (Request.Files.Count > 0)
                    {
                        var Inputfile = Request.Files[0];
                        if (Inputfile != null && Inputfile.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(Inputfile.FileName);
                            var path = Path.Combine(Server.MapPath("~/img/"), filename);
                          
                            Inputfile.SaveAs(path);
                            data.IMAGE = filename;
                        }
                    }
                    data.HOBBY = field;
                    db.tbl_employee.Add(data);
                    db.SaveChanges();
                }

               
            }
            catch (Exception e)
            {

                throw e;
            }
            return RedirectToAction("employee");
        }

        public JsonResult getemployee(int id)
        {
            var emp = db.tbl_employee.Where(i => i.ID == id).FirstOrDefault();
           
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult delete(int id,string emp_del)
        {
            if(emp_del !=null)
            {
                var employee = db.tbl_employee.Where(i => i.ID == id).SingleOrDefault();
                db.tbl_employee.Remove(employee);
                db.SaveChanges();
            }
            return Json('s');
        }

    }
}