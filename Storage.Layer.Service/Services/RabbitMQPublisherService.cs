using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Storage.Layer.Service.Services
{
    public class RabbitMQPublisherService: IPublisherService
    {
        private readonly string _hostName;
        private readonly string _exchangeName;
        private readonly string _queueRequest;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQPublisherService(IConfiguration configuration)
        {
            _hostName = configuration["RabbitMQ:HostName"] ?? "localhost";
            _exchangeName = configuration["RabbitMQ:ExchangeName"] ?? "";
            _queueRequest = configuration["RabbitMQ:QueueRequest"] ?? "";


            _factory = new ConnectionFactory() { HostName = _hostName };

            // Criar e abrir a conexão e canal apenas uma vez
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);

            // Declarar a fila uma vez
            _channel.QueueDeclare(queue: _queueRequest,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            
            _channel.QueueBind(queue: _queueRequest, exchange: _exchangeName, routingKey: _queueRequest);

        }

        public void Publish(object package){

                string json = JsonConvert.SerializeObject(package);
                var body = Encoding.UTF8.GetBytes(json);

                
                _channel.BasicPublish(exchange: _exchangeName,
                                    routingKey: _queueRequest,
                                    basicProperties: null,
                                    body: body);
        }
    }
}
