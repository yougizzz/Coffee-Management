using System.Collections.Generic;
using System.Linq;
using QLQCP.Models.DB;
using System.Data.Entity;
using System;

namespace QLQCP.Models.Action
{
    public class AccountAction 
    {
        /* create */
        public static void  Create_Acccount(string Username, string Password,string Role)
        {
            using (var db = new DBConnection())
            {
                db.DbAccount.Add(new Account { username = Username, password = Password, date_create = DateTime.Now.Date.ToString() ,role = Role ,flag = false});
                db.SaveChanges();
                db.Dispose();
            }
        }
        

        /*delete */
        public static void Delete_Account(string ID)
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Where(item => item.username == ID).FirstOrDefault();
                account.flag = true;
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static Account Check_Login(string Username, string Password)
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Where(item => item.username == Username && item.password == Password && item.flag == false).FirstOrDefault();
                db.Dispose();
                return account;
            }
        }



        public static Account Find(string Username)
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Find(Username);
                db.Dispose();
                return account;
            }
        }


        public static void Restore_Account(string ID)
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Where(item => item.username == ID && item.flag == true).FirstOrDefault();
                account.flag = false;
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Update_Role(string Username)
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Find(Username);
                account.role = "Manager";
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Change_Password(string Username, string Password)
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Find(Username);
                account.password = Password;
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static List<Account> Get_Account(int Page_Size, int Page_Number)
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Where(item => item.flag == false).
                    OrderBy(item => item.username).
                    Skip((Page_Number - 1) * Page_Size).
                    Take(Page_Size).ToList();
                db.Dispose();
                return account;
            }
        }



        public static List<Account> Get_Account()
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Where(item => item.flag == false).ToList();
                db.Dispose();
                return account;
            }
        }



        public static List<Account> Get_Account_Delete(int Page_Size, int Page_Number)
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Where(item => item.flag == true).
                    OrderBy(item => item.username).
                    Skip((Page_Number - 1) * Page_Size).
                    Take(Page_Size).ToList();
                db.Dispose();
                return account;
            }
        }



        public static List<Account> Get_Account_Delete()
        {
            using (var db = new DBConnection())
            {
                var account = db.DbAccount.Where(item => item.flag == true).ToList();
                db.Dispose();
                return account;
            }
        }





    }
}