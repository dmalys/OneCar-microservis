using LinqToDB;
using CouponService.DataAccessLayer.Entities;

namespace CouponService.DataAccessLayer
{
    public class OneCarDbConnection : LinqToDB.Data.DataConnection
    {
        public OneCarDbConnection(string connectionString) : base("SqlServer", connectionString) { }

        public ITable<CouponEntity> Coupon => GetTable<CouponEntity>();
    }
}
