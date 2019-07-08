using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLQCP.Models.Action;
using QLQCP.Models.DB;
using QLQCP.Models;

namespace QLQCP.Controllers
{
    public class AccountController : Controller
    {
        

        public ActionResult AccountManager()
        {
            if(Session["role"] != null && (string)Session["role"] == "Manager")
            {
                var tmp = AccountAction.Get_Account();
                ViewBag.Account = tmp;
                ViewBag.Member = MemberAction.Get_Account_Member();
                List<string> lst_account = new List<string>();
                for(int i = 0; i < tmp.Count; i++)
                {
                    if(MemberAction.Find_By_Id_User(tmp[i].username) == null)
                    {
                        lst_account.Add(tmp[i].username);
                    }
                }
                ViewBag.Username = lst_account;
                using (var db = new DBConnection())
                {
                    int cnt = db.DbMemberShip.Count();
                    ViewBag.Page = cnt / 10 + (cnt % 10 == 0 ? 0 : 1 ) ;
                    db.Dispose();
                }
                    return View();
            }
            else
            {
                return Redirect("~/Home/Index");
            }
            
        } 
       

        public ActionResult AccountRestore()
        {
            if (Session["role"] != null && (string)Session["role"] == "Manager")
            {
                ViewBag.Member = MemberAction.Get_Member_Delete();
                return View();
            }
            else
                return Redirect("~/Home/Index");
            
        }


       

        [HttpPost]
        public ActionResult Register(string Username, string Password, string Role)
        {
            AccountAction.Create_Acccount(Username, Password, Role);
            return Redirect("~/Account/AccountManager");
        }


        public ActionResult ResetPassword(string Username)
        {
            AccountAction.Change_Password(Username, "123456");
            return Redirect("~/Account/AccountManager");
        }


        public ActionResult PromoteStaff(string Username)
        {
            if(Username != null)
            {
                AccountAction.Update_Role(Username);
                return Redirect("~/Account/AccountManager");
            }
            
            return Redirect("~/Account/AccountManager");
        }



        [HttpPost]
        public ActionResult CreateMember(string FullName, string Date_Of_Birth, string ID_User, string Address, string Phone,
            string Email, string ID_Card, string Gender)
        {
            MemberAction.Create_Member(FullName, Date_Of_Birth, ID_User, Address, Phone, Email, ID_Card, Gender);
            return Redirect("~/Account/AccountManager");
        }


        [HttpPost]
        public ActionResult UpdateMember(int ID, string FullName, string Date_Of_Birth, string Address, string Phone,
            string Email, string ID_Card, string Gender)
        {
            MemberAction.Update_Member(ID, FullName, Date_Of_Birth, Address, Phone, Email, ID_Card, Gender);
            return Redirect("~/Account/AccountManager");
        }

        [HttpPost]
        public ActionResult ChangePassword(string Username, string Password)
        {
            AccountAction.Change_Password(Username, Password);
            return Redirect("~/Home/HomePage");
        }

    
        public ActionResult DeleteMember(string ID)
        {
            AccountAction.Delete_Account(ID);
            MemberAction.Delete_Member(ID);
            return Redirect("~/Account/AccountManager");
        }


        public ActionResult UndeleteMember(string ID)
        {
            MemberAction.Restore_Member(ID);
            AccountAction.Restore_Account(ID);
            return Redirect("~/Account/AccountRestore");
        }



        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            var result = AccountAction.Check_Login(Username, Password);
            if (result != null)
            {
                Session["id"] = result.username;
                var name = MemberAction.Find_By_Id_User(result.username);
                if (name != null)
                {
                    Session["name"] = name.fullname;
                }
                
                Session["role"] = result.role;
                if (result.role == "Manager")
                {
                    return Redirect("~/Manager/ProductManager");
                }
                else
                    return Redirect("~/Home/ShowProduct");
                
            }
            else
            {
                return Redirect("~/Home/Index");
            }
        }


        public ActionResult Logout()
        {
            if (Session["id"] != null)
            {
                Session.Clear();
                return Redirect("~/Home/HomePage");
            }
            else
                return Redirect("~/Home/HomePage");
        }



        public ActionResult CreateMemberShip(string Phone, string FullName, string Gender, string Address)
        {
            MemberShipAction.Create_MemberShip(Phone,FullName, Gender, Address);
            return Redirect("~/Home/HomePage");
        }


        [HttpPost]
        public ActionResult MemberLogin(string Phone)
        {
            var member = MemberShipAction.Find_MemberShip_By_Phone(Phone);
            if(member != null) 
            {
                Session["member_id"] = member.phone_number;
                Session["name"] = member.fullname;
                return Redirect("~/Home/HomePage");
            }
            return Redirect("~/Home/HomePage");
        }

        
        public ActionResult MemberLogout()
        {
            if (Session["member_id"] != null)
            {
                Session.Clear();
                return Redirect("~/Home/HomePage");
            }
            else
                return Redirect("~/Home/HomePage");
        }


        public ActionResult MemberInformation()
        {
            if (Session["name"] != null)
            {
                var id = (string)Session["member_id"];
                ViewBag.Customer = MemberShipAction.Find_MemberShip_By_Phone(id);
                ViewBag.Reservation = ReservationAction.Get_Reservation_By_Phone(id);
                ViewBag.History = ReservationAction.Get_History_Reservation(id);
                return View();
            }
            else
                return Redirect("~/Home/HomePage");
            
        }


        public JsonResult GetListMemberShip(int page)
        {
            return Json(MemberShipAction.Get_All_MemberShip(10, page), JsonRequestBehavior.AllowGet);
        }


    }
}