using System.Data.Entity;

namespace QLQCP.Models.DB
{
    public class DBConnection:DbContext
    {
        public DBConnection():base("Coffee_Shop")
        {

        }

        public DbSet<Account> DbAccount { get; set; }
        public DbSet<Member> DbMember { get; set; }
        public DbSet<Product> DbProduct { get; set; }
        public DbSet<Order> DbOrder { get; set; }
        public DbSet<Bill> DbBill { get; set; }
        public DbSet<MemberShip> DbMemberShip { get; set; }
        public DbSet<Table> DbTable { get; set; }
        public DbSet<TableReservation> DbReservation { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>();
            modelBuilder.Entity<Member>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<Bill>();
            modelBuilder.Entity<MemberShip>();
            modelBuilder.Entity<Table>();
            modelBuilder.Entity<TableReservation>();
            base.OnModelCreating(modelBuilder);
        }
       


    }
}