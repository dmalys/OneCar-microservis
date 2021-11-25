using LinqToDB;
using CarImageService.DataAccessLayer.Entities;

namespace CarImageService.DataAccessLayer
{
    public class OneCarDbConnection : LinqToDB.Data.DataConnection
    {
        public OneCarDbConnection(string connectionString) : base("SqlServer", connectionString) { }

        public ITable<CarImageEntity> CarImage => GetTable<CarImageEntity>();
        //public ITable<CarEntity> Car => GetTable<CarEntity>();
        //public ITable<BrandEntity> Brand => GetTable<BrandEntity>();
        //public ITable<CarModelEntity> CarModel => GetTable<CarModelEntity>();
        //public ITable<UserEntity> User => GetTable<UserEntity>();
        //public ITable<CouponEntity> Coupon => GetTable<CouponEntity>();
        //public ITable<TicketEntity> Ticket => GetTable<TicketEntity>();
    }
}
