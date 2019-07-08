using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using QLQCP.Models.DB;

namespace QLQCP.Models.Action
{
    public class ProductAction
    {
        /* CREATE PRODUCT */
        public static void Create_Product(string Name, double Price, string Img_Product)
        {
            using (var db = new DBConnection())
            {
                db.DbProduct.Add(new Product { name = Name, price = Price, date_create = DateTime.Now.Date, img_product = Img_Product});
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* FIND BY ID */
        public static Product Find_By_Id(int Id)
        {
            using (var db = new DBConnection())
            {
                var product = db.DbProduct.Find(Id);
                db.Dispose();
                return product;

            }
        }


        /* FIND BY NAME */
        public static Product Find_By_Name(string Name)
        {
            using (var db = new DBConnection())
            {
                var product = db.DbProduct.Where(item => item.name.Contains(Name)).FirstOrDefault();
                db.Dispose();
                return product;
            }
        }


        /* UPDATE PRODUCT INFORMATION */
        public static void Update_Product(int Id, string Name, double Price)
        {
            using (var db = new DBConnection())
            {
                var product = db.DbProduct.Find(Id);
                product.name = Name;
                product.price = Price;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* UPDATE IMAGE PRODUCT */
        public static void Update_Image(int Id, string Img_Product)
        {
            using (var db = new DBConnection())
            {
                var product = db.DbProduct.Find(Id);
                product.img_product = Img_Product;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();

            }
        }


        /* DELETE PRODUCT */
        public static void Delete_Product(int Id)
        {
            using (var db = new DBConnection())
            {
                var product = db.DbProduct.Find(Id);
                product.flag = true;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }

        /* RESTORE PRODUCT */
        public static void Restore_Product(int Id)
        {
            using (var db = new DBConnection())
            {
                var product = db.DbProduct.Find(Id);
                product.flag = false;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }



        /* GET LIST PRODUCT BY PAGE NUMBER */
        public static List<Product> Get_Product()
        {
            using (var db = new DBConnection())
            {
                var product = db.DbProduct.Where(item => item.flag == false).ToList();
                db.Dispose();
                return product;
            }
        }


        public static List<Product> Get_Delete_Product()
        {
            using (var db = new DBConnection())
            {
                var product = db.DbProduct.Where(item => item.flag == true).ToList();
                db.Dispose();
                return product;
            }
        }


    }
}