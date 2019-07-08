using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using QLQCP.Models.DB;

namespace QLQCP.Models.Action
{
    public class MemberAction
    {
        public static void Create_Member(string FullName, string Date_Of_Birth, string ID_User, string Address, string Phone, 
            string Email, string ID_Card, string Gender)
        {
            using (var db = new DBConnection())
            {
                db.DbMember.Add(new Member {
                    fullname = FullName,
                    username = ID_User,
                    id_card = ID_Card,
                    date_of_birth = Date_Of_Birth,
                    address = Address,
                    email = Email,
                    phone = Phone,
                    gender = Gender,
                    flag = false});
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Update_Member(int ID, string FullName, string Date_Of_Birth, string Address, string Phone,
            string Email, string ID_Card, string Gender)
        {
            using (var db = new DBConnection())
            {
                var member = db.DbMember.Find(ID);
                member.fullname = FullName;
                member.address = Address;
                member.email = Email;
                member.date_of_birth = Date_Of_Birth;
                member.gender = Gender;
                member.id_card = ID_Card;
                member.phone = Phone;
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Delete_Member(string ID)
        {
            using (var db = new DBConnection())
            {
                var member = db.DbMember.Where(item => item.username == ID).FirstOrDefault();
                member.flag = true;
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Restore_Member(string ID)
        {
            using (var db = new DBConnection())
            {
                var member = db.DbMember.Where(item => item.flag == true && item.username == ID).FirstOrDefault();
                member.flag = false;
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static Member Find_By_Id_User(string Id)
        {
            using (var db = new DBConnection())
            {
                var member = db.DbMember.Where(item => item.username == Id).FirstOrDefault();
                db.Dispose();
                return member;
            }
        }


        public static List<Member> Get_Member(int Page_Size, int Page_Number)
        {
            using (var db = new DBConnection())
            {
                var member = db.DbMember.Where(item => item.flag == false).
                    OrderBy(item => item.fullname).
                    Skip((Page_Number - 1) * Page_Size).
                    Take(Page_Size).ToList();
                db.Dispose();
                return member;
            }
        }


        public static List<Tuple<Account, Member>> Get_Account_Member()
        {
            using (var db = new DBConnection())
            {
                List<Tuple<Account, Member>> lst_info = new List<Tuple<Account, Member>>();
                Tuple<Account, Member> tup;
                var account = db.DbAccount.Where(item => item.flag == false).ToList();
                var member = db.DbMember.Where(item => item.flag == false).ToList();
                for(int i = 0; i < account.Count; i++)
                {
                    for(int j = 0; j < member.Count; j++)
                    {
                        if(account[i].username == member[j].username)
                        {
                            tup = new Tuple<Account, Member>(account[i], member[j]);
                            lst_info.Add(tup);
                        }
                        
                    }
                }
                return lst_info;
            }
        }

        public static List<Member> Get_Member()
        {
            using (var db = new DBConnection())
            {
                var member = db.DbMember.Where(item => item.flag == false).ToList();
                db.Dispose();
                return member;
            }
        }


        public static List<Member> Get_Member_Delete(int Page_Size, int Page_Number)
        {
            using (var db = new DBConnection())
            {
                var member = db.DbMember.Where(item => item.flag == true).
                    OrderBy(item => item.fullname).
                    Skip((Page_Number - 1) * Page_Size).
                    Take(Page_Size).ToList();
                db.Dispose();
                return member;
            }
        }



        public static List<Member> Get_Member_Delete()
        {
            using (var db = new DBConnection())
            {
                var member = db.DbMember.Where(item => item.flag == true).ToList();
                db.Dispose();
                return member;
            }
        }


    }
}