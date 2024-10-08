﻿using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service.Order.API.Core.Repositories;
using System.Text;
using System.Threading.Channels;

namespace Service.Order.API.Consumers
{
    public class OrderCreatedConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IModel _channel;
        private const string Queue = "orderservice.order-created";
        private const string Exchange = "order";
        private const string RoutingKey = "order-created";

        public OrderCreatedConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = connectionFactory.CreateConnection("orderservice.order-created");

            _channel = connection.CreateModel();

            _channel.QueueDeclare(Queue, true, false, false, null);

            _channel.ExchangeDeclare(Exchange, "direct", true, false);

            _channel.QueueBind(Queue, Exchange, RoutingKey);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var json = Encoding.UTF8.GetString(contentArray);
                var orderMessage = JsonConvert.DeserializeObject<OrderMessage>(json);

                Console.WriteLine(json);

                var @event = new Core.Entities.Order
                {
                    CustomerId = orderMessage.CodigoCliente,
                    Code = orderMessage.CodigoPedido,
                    TotalValue = orderMessage.Itens.Sum(i => i.Quantidade * i.Preco),
                    OrderItems = orderMessage.Itens.Select(i => new Core.Entities.OrderItem
                    {
                        Product = i.Produto,
                        Quantity = i.Quantidade,
                        Price = i.Preco,
                    }).ToList()
                };

                try
                {
                    await ProcessOrder(@event);
                    Console.WriteLine("Message: READ");
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    // logging
                    // Handle processing failure
                    //_channel.BasicNack(eventArgs.DeliveryTag, false, true);
                }
            };

            _channel.BasicConsume(Queue, false, consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessOrder(Core.Entities.Order order)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();

                var customer = await customerRepository.GetCustomerAsync(order.CustomerId);
                if (customer != null)
                {
                    await orderRepository.AddOrderAsync(order);
                }
            }
        }
    }

    public class OrderMessage
    {
        public string CodigoPedido { get; set; }
        public int CodigoCliente { get; set; }
        public OrderItem[] Itens { get; set; }
    }

    public class OrderItem
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
