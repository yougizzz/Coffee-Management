using QLQCP.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLQCP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult HomePage()
        {
            ViewBag.Drink = ProductAction.Get_Product();
            return View();
        }


        public ActionResult ShowProduct()
        {
            if(Session["id"] != null)
            {
                ViewBag.Product = ProductAction.Get_Product();
                return View();
            }
            else
            {
                return Redirect("~/Home/Index");
            }
            
        }


        public ActionResult ShowTable()
        {
            ViewBag.Table = TableAction.Get_All_Table();
            return View();
        }


        public JsonResult Validation_Password(string Username)
        {
            return Json(AccountAction.Find(Username), JsonRequestBehavior.AllowGet);
        }


        public JsonResult Validation_Member(string Username)
        {
            return Json(MemberAction.Find_By_Id_User(Username), JsonRequestBehavior.AllowGet);
        }


        public JsonResult Validation_Product(string Productname)
        {
            return Json(ProductAction.Find_By_Name(Productname), JsonRequestBehavior.AllowGet);
        }

       

    }
}