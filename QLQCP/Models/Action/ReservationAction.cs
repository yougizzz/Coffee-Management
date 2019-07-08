using QLQCP.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QLQCP.Models.Action
{
    public class ReservationAction
    {
        public static void Create_Reservation(string Customer_Phone, int Id_Table)
        {
            using (var db = new DBConnection())
            {
                db.DbReservation.Add(new TableReservation {
                    customer_telephone = Customer_Phone,
                    id_table = Id_Table,
                    flag = false,
                    cancel = false,
                    seats = TableAction.Find_Table_By_ID(Id_Table).seats,
                    fullname = MemberShipAction.Find_MemberShip_By_Phone(Customer_Phone).fullname,
                    time_create = DateTime.Now,
                    checkin = false
                    });
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Cancel_Reservation(string Customer_Phone, int ID)
        {
            using (var db = new DBConnection())
            {
                
                var reservation = db.DbReservation.Where(item => item.customer_telephone == Customer_Phone && item.id == ID).FirstOrDefault();
                reservation.cancel = true;
                db.Entry(reservation).State = EntityState.Modified;
                var table = db.DbTable.Find(reservation.id_table);
                table.flag = false;
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Checkin_Reservation(string Customer, int ID)
        {
            using (var db = new DBConnection())
            {
                var reservation = db.DbReservation.Where(item => item.checkin == false && item.id == ID && item.customer_telephone == Customer).FirstOrDefault();
                reservation.checkin = true;
                db.Entry(reservation).State = EntityState.Modified;
                var customer = db.DbMemberShip.Where(item => item.phone_number == Customer && item.flag == false).FirstOrDefault();
                customer.score = customer.score + TableAction.Find_Table_By_ID(ID).seats * 2;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static List<TableReservation> Get_All_Reservation()
        {
            using (var db = new DBConnection())
            {
                var lst_reservation = db.DbReservation.Where(item => item.cancel == false && item.checkin == false).ToList();
                db.Dispose();
                return lst_reservation;
            }
        }


        public static List<TableReservation> All_Reservation()
        {
            using (var db = new DBConnection())
            {
                var lst_reservation = db.DbReservation.OrderBy(item => item.time_create).ToList();
                db.Dispose();
                return lst_reservation;
            }
        }


        public static void Auto_Cancel()
        {
            using (var db = new DBConnection())
            {
                var reservation = db.DbReservation.Where(item => item.checkin == false && item.cancel == false).ToList();
                for(int i = 0; i < reservation.Count; i++)
                {
                    int time_count = DateTime.Now.TimeOfDay.Subtract(reservation[i].time_create.TimeOfDay).Hours;
                    /* AUTO DELETE RESERVATION AFTER 12 HOURS WITH TABLE 10 SEATS */
                    if(reservation[i].seats == 10 && time_count > 12)
                    {
                        reservation[i].cancel = true;
                        MemberShipAction.Decreate_Score(reservation[i].customer_telephone, 15);
                        db.Entry(reservation[i]).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    /* AUTO DELETE RESERVATION AFTER 5 HOURS WITH TABLE 5 SEATS  */
                    if (reservation[i].seats == 5 && time_count > 5)
                    {
                        reservation[i].cancel = true;
                        MemberShipAction.Decreate_Score(reservation[i].customer_telephone, 10);
                        db.Entry(reservation[i]).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                db.Dispose();
            }
        }


        public static void NoCheckin_Reservation()
        {
            //using (var db = new DBConnection())
            //{
            //    var reservation = db.DbReservation.Where(item => item)
            //}
        }


        public static List<TableReservation> Get_Reservation_By_Phone(string Phone)
        {
            using (var db = new DBConnection())
            {
                var lst_reservation = db.DbReservation.
                    Where(item => item.cancel == false && item.customer_telephone == Phone && item.checkin == false).ToList();
                db.Dispose();
                return lst_reservation;
            }
        }


        public static List<TableReservation> Get_History_Reservation(string Phone)
        {
            using (var db = new DBConnection())
            {
                var lst_reservation = db.DbReservation.
                    Where(item =>item.customer_telephone == Phone).ToList();
                db.Dispose();
                return lst_reservation;
            }
        }
    }
}