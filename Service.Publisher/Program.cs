using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection("orders.publisher");
        using var channel = connection.CreateModel();
        var routingKey = "order-created";
        var exchange = "order";

        channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct, true, false);

        while (true)
        {
            Console.WriteLine("Enter Order Code:");
            var orderId = Console.ReadLine();

            Console.WriteLine("Enter Customer Id:");
            var customerId = Console.ReadLine();

            var order = new OrderMessage
            {
                CodigoPedido = orderId,
                CodigoCliente = int.Parse(customerId),
                Itens = new[]
                {
                        new OrderItem { Produto = "lápis", Quantidade = 100, Preco = 1.10m },
                        new OrderItem { Produto = "caderno", Quantidade = 10, Preco = 1.00m }
                    }
            };

            var message = JsonSerializer.Serialize(order);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: exchange,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($" [x] Sent {message}");
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