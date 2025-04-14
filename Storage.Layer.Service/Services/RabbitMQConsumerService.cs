using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using Meter.Farm.DTO;
using Storage.Layer.Service.Interfaces;
using Storage.Layer.Service.Domain;

namespace Storage.Layer.Service.Services
{
    public class RabbitMQConsumerService : BackgroundService
    {
        private readonly string _queueName;
        private readonly string _hostName;
        private readonly ILogger<RabbitMQConsumerService> _logger;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private IDataProcess _messageProcess;
        private const int WAIT_CONNECTION_RETRY = 2000;

        public RabbitMQConsumerService(ILogger<RabbitMQConsumerService> logger, MessageProcess messageProcess, IConfiguration configuration)
        {
            _logger = logger;
            _messageProcess = messageProcess;

            _hostName = configuration["RabbitMQ:HostName"] ?? "localhost";
            _queueName = configuration["RabbitMQ:QueueResponse"] ?? "";

            while (true)
            {
                try
                {
                    _factory = new ConnectionFactory() { HostName = _hostName };

                    // Criar e abrir a conexão e canal apenas uma vez
                    _connection = _factory.CreateConnection();
                    _channel = _connection.CreateModel();

                    // Declarar a fila uma vez
                    _channel.QueueDeclare(queue: _queueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    break;
                }
                catch (Exception)
                {
                    logger.LogError("Não foi possível se conectar ao sistema de mensageria. Tentando reconexão em {0}s", WAIT_CONNECTION_RETRY / 1000);
                    Thread.Sleep(WAIT_CONNECTION_RETRY);
                }
            }
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("🟢 Serviço do RABBITMQ iniciado");

            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JsonConvert.DeserializeObject<ServerPackageObject>(message);

                _logger.LogDebug("[RABBITMQ] Mensagem recebida e evento disparado.");

                _messageProcess.AddRequestPackage(data);

            };
            _channel.BasicConsume(queue: _queueName,
                             autoAck: true,
                             consumer: consumer);

            while (!stoppingToken.IsCancellationRequested) await Task.Delay(1000, stoppingToken);
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }

    }
}

