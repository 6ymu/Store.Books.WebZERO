using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Store.Books.Infrastructure.Interfaces;

namespace Store.Books.Order.Services
{
    using Interfaces;
    using Domain;
    using System.Threading.Tasks;
    

    public class StoreService : IStoreService
    {
        private readonly ILogger _logger;
        private readonly IOrderRepository _orders;
        private readonly IPaymentRepository _payments;
        private readonly IBusketRepository _buskets;

        public StoreService(IPaymentRepository payment,
                            IOrderRepository order,
                            IBusketRepository busket,
                            ILogger<StoreService> logger)
        {
            _logger = logger;
            _orders = order;
            _payments = payment;
            _buskets = busket;
        }

        public async Task<Order> CreateOrUpdateOrder(int bookId, string title, int priceId, decimal price, string userName)
        {
            if(0 >= bookId)
            {
                _logger.LogWarning($"CreateOrder: bookId <= 0: {bookId}");
                throw new ArgumentOutOfRangeException($"bookId < 0: {bookId}");
            }
            if (price < 0)
            {
                _logger.LogWarning($"CreateOrder: price amount too low for bookId: {bookId}");
                throw new ArgumentOutOfRangeException($"lastPrice not found for bookId: {bookId}");
            }
            var order = await GetLastUnpayedOrder(userName);
            if (order is null)
            { 
                _logger.LogWarning($"CreateOrder: last order for user: (userName) not found, creating new.");
                await _orders.Insert(new Order
                {
                    UserName = userName,
                    Status = Domain.Enums.OrderStatusEnum.Created,
                    Created = DateTime.Now,
                    Total = price
                });            
                        
               order = await GetLastUnpayedOrder(userName);
            }
            await AddToBusket(order, bookId, title, priceId, price);
            return order;
        }
  
        public async Task<bool> AddToBusket(Order order, int bookId, string title, int priceId, decimal price)
        {
            var existing = _buskets
            .Get(p => p.Order.Id == order.Id && p.BookId == bookId, includeProperties: "Order, Book");
            if (!(existing is null))
            {
                _logger.LogWarning($"AddToBusket: bookId: (bookld) exist in busket");
                return false;
            }
            await _buskets.Insert(new Busket{Order = order, BookId = bookId, PriceId = priceId, Amount = price }); 
            return SafeSave();
          }

        public bool DeleteFromBusket(Order order, int bookId)
        {
            //var busket = ????;
            return false;
        }

        public async Task<Order> FindOrder(int orderId)
        {
            return await _orders.GetById(orderId);
        }

        public bool CompleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool SafeSave()
        {
            try
            {
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
     
        public async Task<Order> GetLastUnpayedOrder(string userName)
        {
            return (await _orders.Get(p => p.UserName == userName && p.Status == Domain.Enums.OrderStatusEnum.Created))
                .OrderByDescending(p => p.Created)
                .FirstOrDefault();
        }

    }
}