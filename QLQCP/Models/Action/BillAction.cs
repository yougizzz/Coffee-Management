using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using QLQCP.Models.DB;

namespace QLQCP.Models.Action
{
    public class BillAction
    {
        /* CREATE BILL */
        public static void Create_Bill(int Id_Order, int Id_Product, int Quantity, double Price)
        {
            using (var db = new DBConnection())
            {
                var product = ProductAction.Find_By_Id(Id_Product);
                db.DbBill.Add(new Bill {
                    id_order = Id_Order,
                    date_create = DateTime.Now.Date,
                    product_name = product.name,
                    price = product.price,
                    quantity = Quantity,
                    id_product = Id_Product,
                    total = product.price * Quantity });
                db.SaveChanges();
                db.Dispose();
            }
        }

        public static Bill Find_By_Id(int Id)
        {
            using (var db = new DBConnection())
            {
                var bill = db.DbBill.Find(Id);
                db.Dispose();
                return bill;
            }
        }




    }
}