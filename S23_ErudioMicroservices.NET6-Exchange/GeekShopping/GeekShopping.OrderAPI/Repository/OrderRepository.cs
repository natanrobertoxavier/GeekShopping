using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.Model.Context;
using GeekShopping.OrderAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<MySQLContext> _context;
        
        public OrderRepository(DbContextOptions<MySQLContext> context)
        {
            _context = context;
        }
        public async Task<bool> AddOrder(OrderHeader header)
        {
            if (header == null) return false;
            await using var _db = new MySQLContext(_context);
            _db.Headers.Add(header);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool satus)
        {
            await using var _db = new MySQLContext(_context);
            var header = await _db.Headers.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
            if (header != null)
            {
                header.PaymentStatus = satus;
                await _db.SaveChangesAsync();
            }
        }
    }
}
