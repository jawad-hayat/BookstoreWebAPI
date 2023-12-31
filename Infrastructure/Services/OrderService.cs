﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _baseketRepo;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IPaymentService _paymentService;
        public OrderService(IUnitOfWork unitOfWork, IBasketRepository baseketRepo)
        {
            //_paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _baseketRepo = baseketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basket from repo
            var basket = await _baseketRepo.GetBasketAsync(basketId);

            //get item from the product repo
            var items = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var bookItem = await _unitOfWork.Repository<Book>().GetByIdAsync(item.Id);
                var itemOrdered = new BookItemOrdered(bookItem.Id, bookItem.Name, bookItem.ImageUrl);
                var orderItem = new OrderItem(itemOrdered, bookItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            //get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //calculate subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //check to see if order exists
            var spec = new OrderByPaymentIntentWithItemSpecification(basket.PaymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (order != null)
            {
                order.ShipToAddress = shippingAddress;
                order.DeliveryMethod = deliveryMethod;
                order.Subtotal = subtotal;
                _unitOfWork.Repository<Order>().Update(order);
            }
            else
            {
                // create order
                order = new Order(items, buyerEmail, shippingAddress, deliveryMethod,
                    subtotal, basket.PaymentIntentId);
                _unitOfWork.Repository<Order>().Add(order);
            }

            // save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return order;
        }


        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecipication(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecipication(id, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }
    }
}
