using QLQCP.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QLQCP.Models.Action
{
    public class MemberShipAction
    {
        public static void Create_MemberShip(string Phone, string FullName, string Gender, string Address)
        {
            using (var db = new DBConnection())
            {
                db.DbMemberShip.Add(new MemberShip { phone_number = Phone, fullname = FullName, gender = Gender, address = Address, score = 15});
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Update_MemberShip(string Phone, string FullName, string Gender, string Address)
        {
            using (var db = new DBConnection())
            {
                var customer = db.DbMemberShip.Where(item => item.phone_number == Phone).FirstOrDefault();
                customer.fullname = FullName;
                customer.gender = Gender;
                customer.address = Address;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Delete_MemberShip(string Phone)
        {
            using (var db = new DBConnection())
            {
                var customer = db.DbMemberShip.Where(item => item.phone_number == Phone).FirstOrDefault();
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void MemberShip_Restore(string Phone)
        {
            using (var db = new DBConnection())
            {
                var customer = db.DbMemberShip.Where(item => item.phone_number == Phone && item.flag == true).FirstOrDefault();
                customer.flag = false;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static MemberShip Find_MemberShip_By_Phone(string Phone)
        {
            using (var db = new DBConnection())
            {
                var customer = db.DbMemberShip.Where(item => item.phone_number == Phone).FirstOrDefault();
                db.Dispose();
                return customer;
                
            }
        }


        public static void Increate_Score(string Phone, int Score)
        {
            using (var db = new DBConnection())
            {
                var customer = db.DbMemberShip.Where(item => item.flag == false && item.phone_number == Phone).FirstOrDefault();
                customer.score = customer.score + Score;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Decreate_Score(string Phone, int Score)
        {
            using (var db = new DBConnection())
            {
                var customer = db.DbMemberShip.Where(item => item.flag == false && item.phone_number == Phone).FirstOrDefault();
                customer.score = customer.score - Score;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static List<MemberShip> Get_All_MemberShip(int page_size, int page_number)
        {
            using (var db = new DBConnection())
            {
                var lst_customer = db.DbMemberShip.Where(item => item.flag == false).OrderBy(item => item.fullname).Skip((page_number - 1) * page_size).Take(page_size).
                    OrderBy(item => item.fullname).ToList();
                db.Dispose();
                return lst_customer;
            }
        }


        public static List<MemberShip> Get_MemberShip_Delete(int page_size, int page_number)
        {
            using (var db = new DBConnection())
            {
                var lst_customer = db.DbMemberShip.Where(item => item.flag == true).Skip((page_number - 1) * page_size).Take(page_size).
                    OrderBy(item => item.fullname).ToList();
                db.Dispose();
                return lst_customer;
            }
        }






    }
}