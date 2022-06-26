using Api.Models;
using Api.Repositories;
using HotChocolate;
//using HotChocolate.Subscriptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Graph;
public class Query
{
    public Task<List<Order>> AllOrders([Service] IOrderRepository orderRepository) =>
            orderRepository.GetAll();

    public Task<List<Order>> AllOrdersWithDetails([Service] IOrderRepository orderRepository) =>
            orderRepository.GetOrdersWithDetails();
}
