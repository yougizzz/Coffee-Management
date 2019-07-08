using QLQCP.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QLQCP.Models.Action
{
    public class TableAction
    {

        public static void Create_Table(int Seats)
        {
            using (var db = new DBConnection())
            {
                db.DbTable.Add(new Table { seats = Seats});
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Update_Table(int ID, int Seats)
        {
            using (var db = new DBConnection())
            {
                var table = db.DbTable.Find(ID);
                table.seats = Seats;
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static void Delete_Table(int ID)
        {
            using (var db = new DBConnection())
            {
                var table = db.DbTable.Find(ID);
                table.flag = true;
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        public static Table Find_Table_By_ID(int ID)
        {
            using (var db = new DBConnection())
            {
                var table = db.DbTable.Find(ID);
                db.Dispose();
                return table;
            }
        }


        public static List<Table> Get_All_Table()
        {
            using (var db = new DBConnection())
            {
                var lst_table = db.DbTable.Where(item => item.flag == false).ToList();
                db.Dispose();
                return lst_table;
            }
        }

        public static List<Table> Get_Table_By_Customer()
        {
            using (var db = new DBConnection())
            {
                var lst_table = db.DbTable.Where(item => item.flag == false && item.seats >= 5).OrderBy(item => item.seats).ToList();
                db.Dispose();
                return lst_table;
            }
        }

        public static List<Table> Get_Table_Delete()
        {
            using (var db = new DBConnection())
            {
                var lst_table = db.DbTable.Where(item => item.flag == true).ToList();
                db.Dispose();
                return lst_table;
            }
        }


        public static void Table_Restore(int ID)
        {
            using (var db = new DBConnection())
            {
                var table = db.DbTable.Where(item => item.id == ID && item.flag == true).FirstOrDefault();
                table.flag = false;
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();

            }
        }



    }
}