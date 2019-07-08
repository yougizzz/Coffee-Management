using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using QLQCP.Models;
using QLQCP.Models.Action;

namespace QLQCP.Controllers
{
    public class ManagerController : Controller
    {
        public ActionResult ProductManager()
        {
            if ( Session["role"] != null && (string)Session["role"] == "Manager")
            {

                ViewBag.Product = ProductAction.Get_Product();
                return View();
            }
            else
                return Redirect("~/Home/Index");
           
        }


        public ActionResult ProductRestore()
        {
            if (Session["role"] != null && (string)Session["role"] == "Manager")
            {
                ViewBag.Product = ProductAction.Get_Delete_Product();
                ViewBag.Table = TableAction.Get_Table_Delete();
                return View();
            }
            else
                return Redirect("~/Home/Index");
            
        }


        public ActionResult CreateProduct(string Name, double Price,  HttpPostedFileBase File)
        {
            string path = "";
            if (File.ContentLength > 0)
            {
                string File_Name = Path.GetFileName(File.FileName);
                path = Path.Combine(Server.MapPath("~/UploadFiles"), File_Name);
                File.SaveAs(path);
                ProductAction.Create_Product(Name, Price, File_Name);
                return Redirect("~/Manager/ProductManager");
            }
            else
               return Redirect("~/Manager/ProductManager");

        }


        public ActionResult UpdateProduct(int Id,string Name, double Price )
        {
            ProductAction.Update_Product(Id, Name, Price);
            return Redirect("~/Manager/ProductManager");
        }


        public ActionResult Update_Image_Product(int Id, HttpPostedFileBase File)
        {
            string path = "";
            if (File.ContentLength > 0)
            {
                string File_Name = Path.GetFileName(File.FileName);
                path = Path.Combine(Server.MapPath("~/UploadFiles"), File_Name);
                File.SaveAs(path);
                ProductAction.Update_Image(Id, File_Name);
                return Redirect("~/Manager/ProductManager");
            }
            else
                return Redirect("~/Manager/ProductManager");
        }


        public ActionResult Delete_Product(int Id)
        {
            ProductAction.Delete_Product(Id);
            return Redirect("~/Manager/ProductManager");
        }


        public ActionResult Undelete_Product(int Id)
        {
            ProductAction.Restore_Product(Id);
            return Redirect("~/Manager/ProductRestore");
        }


        public ActionResult ClearAll()
        {
            if (Session["order"] != null)
            {
                Dictionary<int, Item> order = (Dictionary<int, Item>)Session["order"];
                order.Clear();
                Session["order"] = null;
                return Redirect("~/Home/ShowProduct");
            }
            return Redirect("~/Home/ShowProduct");
        }


        public ActionResult TableManager()
        {
            if(Session["role"] != null && (string)Session["role"] == "Manager")
            {
                ViewBag.Table = TableAction.Get_All_Table();
                return View();
            }
            return Redirect("~/Home/HomePage");
            
        }


        public ActionResult Create_Table(int Seats)
        {
            TableAction.Create_Table(Seats);
            return Redirect("~/Manager/TableManager");
        }


        public ActionResult DeleteTable(int ID)
        {
            TableAction.Delete_Table(ID);
            return Redirect("~/Manager/TableManager");
        }


        public ActionResult RestoreTable(int ID)
        {
            TableAction.Table_Restore(ID);
            return Redirect("~/Manager/TableManager");
        }


        public JsonResult Get_Product(int Id)
        {
            return Json(ProductAction.Find_By_Id(Id), JsonRequestBehavior.AllowGet);
        }


        public JsonResult CreateOrder(int Id_Product)
        {
            if (Session["order"] == null)
            {
                Dictionary<int, Item> order = new Dictionary<int, Item>();
                order.Add(Id_Product, new Item { product = ProductAction.Find_By_Id(Id_Product), quantity = 1 });
                Session["order"] = order;
            }
            else
            {
                Dictionary<int, Item> order = (Dictionary<int, Item>)Session["order"];
                if (order.ContainsKey(Id_Product))
                {
                    var tmp = order[Id_Product];
                    tmp.quantity = tmp.quantity + 1;
                    order[Id_Product] = tmp;
                    
                }
                else
                {
                    order.Add(Id_Product, new Item { product = ProductAction.Find_By_Id(Id_Product), quantity = 1 });
                }
            }
            return Json("Good", JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetAllOrder()
        {
            List<Item> lst_order = new List<Item>();
            Dictionary<int, Item> order = (Dictionary<int, Item>)Session["order"];
            foreach(var item in order)
            {
                lst_order.Add(item.Value);
            }
            return Json(lst_order, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Delete_Specific_Item(int Id_Product)
        {
            if (Session["order"] != null)
            {
                Dictionary<int, Item> order = (Dictionary<int, Item>)Session["order"];
                order.Remove(Id_Product);
                Session["order"] = order;
                List<Item> lst_order = new List<Item>();
                foreach (var item in order)
                {
                    lst_order.Add(item.Value);
                }
                return Json(lst_order, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
            
        }


        public JsonResult CreateBill(string Customer_Phone)
        {
            Dictionary<int, Item> order = (Dictionary<int, Item>)Session["order"];
            double sum = 0;
            foreach(var item in order)
            {
                sum += item.Value.product.price * item.Value.quantity;
            }
            if(sum != 0)
            {
                OrderAction.Create_Order(DateTime.Now.Date, (string)Session["id"], sum);
                int ID_Order = OrderAction.Find();
                foreach (var item in order)
                {
                    BillAction.Create_Bill(ID_Order, item.Value.product.id, item.Value.quantity, item.Value.product.price);
                }
                if ( !Customer_Phone.Equals("0") || Customer_Phone != "")
                {
                    MemberShipAction.Increate_Score(Customer_Phone, order.Count * 2);
                }
                Session["order"] = null;
                order.Clear();
                ID_Order = 0;
                sum = 0;
                return Json("Good", JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["order"] = null;
                order.Clear();
                sum = 0;
                return Json("Bad", JsonRequestBehavior.AllowGet);
            }
            
        }


        public JsonResult Get_Member(string Id)
        {
            return Json(MemberAction.Find_By_Id_User(Id), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetTable(int ID)
        {
            return Json(TableAction.Find_Table_By_ID(ID), JsonRequestBehavior.AllowGet);
        }
    
        
        public JsonResult GetAllTable()
        {
            return Json(TableAction.Get_Table_By_Customer(), JsonRequestBehavior.AllowGet);
        }

    }
}