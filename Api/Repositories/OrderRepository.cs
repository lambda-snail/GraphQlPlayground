using Microsoft.EntityFrameworkCore;

using Api.Models;

namespace Api.Repositories;
public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(NorthwindContext context) : base(context)
    {

    }

    public Task<List<Order>> GetOrdersWithDetails()
    {
        return _dbSet
            .Include(o => o.OrderDetails)
            .ThenInclude(detail => detail.Product)
            .ToListAsync();
    }
}

public interface IOrderRepository : IOrderRepositoryBase<Order>
{
    Task<List<Order>> GetOrdersWithDetails();
}