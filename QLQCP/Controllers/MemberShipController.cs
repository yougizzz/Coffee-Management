using QLQCP.Models.Action;
using QLQCP.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLQCP.Controllers
{
    public class MemberShipController : Controller
    {

        public ActionResult Book_Table(int ID_Table)
        {
            ReservationAction.Auto_Cancel();
            string phone = (string)Session["member_id"];
            if (MemberShipAction.Find_MemberShip_By_Phone(phone).score >= 10)
            {
                ReservationAction.Create_Reservation(phone, ID_Table);
                TableAction.Delete_Table(ID_Table);
                return Redirect("~/Home/HomePage");
            }
            
           
            return View();
        }


        public ActionResult Cancel_By_Customer(int ID_Reservation)
        {
            ReservationAction.Auto_Cancel();
            using (var db = new DBConnection())
            {
                var reservation = db.DbReservation.Find(ID_Reservation);
                if(reservation.cancel == false && reservation.checkin == false)
                {
                    ReservationAction.Cancel_Reservation((string)Session["member_id"], ID_Reservation);
                   
                }
                else
                {
                    if (reservation.seats == 5)
                    {
                        MemberShipAction.Decreate_Score((string)Session["member_id"], 10);
                    }
                    if (reservation.seats == 10)
                    {
                        MemberShipAction.Decreate_Score((string)Session["member_id"], 15);
                    }
                }
                return Redirect("~/Account/MemberInformation");
            }
        }


        public ActionResult Checkin_Table(string Customer_Phone, int ID)
        {
            ReservationAction.Checkin_Reservation(Customer_Phone, ID);
            ReservationAction.Auto_Cancel();
            return Redirect("~/MemberShip/List_Reservation");
        }


        public ActionResult Checkout_Table(int ID_Table)
        {
            TableAction.Table_Restore(ID_Table);
            return Redirect("~/MemberShip/List_Reservation");
        }


        public ActionResult List_Reservation()
        {
            if (Session["id"] != null)
            {
                ViewBag.Table = ReservationAction.Get_All_Reservation();
                ReservationAction.Auto_Cancel();
                return View();
            }
            return Redirect("~/Home/HomePage");
        }


        public ActionResult List_MemberShip()
        {
            return View();
        }


        public ActionResult HistoryReservation()
        {
            if((string)Session["role"] == "Manager")
            {
                ViewBag.Reservation = ReservationAction.All_Reservation();
                return View();
            }
            return Redirect("~/Home/HomePage");
        }


    }
}