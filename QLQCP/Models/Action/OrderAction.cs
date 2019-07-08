using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLQCP.Models.DB;

namespace QLQCP.Models.Action
{
    public class OrderAction
    {
        public static void Create_Order(DateTime Date_Create, string Username, double Total)
        {
            using (var db = new DBConnection())
            {
                db.DbOrder.Add(new Order { date_create = Date_Create, id_user = Username, total = Total });
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static int Find()
        {
            using (var db = new DBConnection())
            {
                var order = db.DbOrder.Max(item => item.id);
                db.Dispose();
                return order;
            }
        }


    }
}