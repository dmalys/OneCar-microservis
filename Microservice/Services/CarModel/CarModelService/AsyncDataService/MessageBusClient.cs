using CarModelService.AsyncDataService.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarModelService.AsyncDataService
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);//TODO:

                _channel.ConfirmSelect();

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not connect to message bus: {ex.Message}");
                throw;
            }
        }

        public async Task PublishCheckBrandExists(BrandPublishDTO brandPublishDTO)
        {
            var message = JsonSerializer.Serialize(brandPublishDTO);

            if (_connection.IsOpen)
            {
                Console.WriteLine("rabbitmq connection open sending message");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("rabbitmq connection closed, not sending message");

            }

        }

        private void SendMessage(string message)
        {
            Console.WriteLine("rabbitmq SendMessage");
            var messageBody = Encoding.UTF8.GetBytes(message);

            var basicProperties = _channel.CreateBasicProperties();
            basicProperties.Persistent = true;

            //?? wyjebac
            _channel.BasicAcks += (sender, ea) => { Console.WriteLine("ack"); };
            //_channel.BasicRejects += (sender, ea) => { Console.WriteLine(); };
            _channel.BasicNacks += (sender, ea) => { Console.WriteLine("nack"); };
            //

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: basicProperties, body: messageBody);

            Console.WriteLine($"We send message {message}");

            _channel.WaitForConfirmsOrDie();
            
        }

        public void Dispose()
        {

            Console.WriteLine("Message bus disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("==> RabbitMQ connection shutdown.");
        }
    }
}
