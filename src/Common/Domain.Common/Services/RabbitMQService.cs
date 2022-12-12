// using System.Text
// using RabbitMQ.Client;

// namespace Domain.Common.Services;

// public class RabbitMQService
// {
//     private string _hostName;
//     private int _port;
//     private string _password;
//     private string _username;

//     private ConnectionFactory _factory;

//     public RabbitMQService(string hostName, int port, string password = "", string username = "")
//     {
//         _hostName = hostName;
//         _port = port;
//         _password = password;
//         _username = username;
//         _factory = new ConnectionFactory() { HostName = _hostName, Port = _port, UserName = _username, Password = _password };
//     }

//     public void PublishEvent(Event e, string queue)
//     {
//         using (var connection = _factory.CreateConnection())
//         using (var channel = connection.CreateModel())
//         {
//             channel.QueueDeclare(
//                 queue: queue,
//                 durable: true,
//                 exclusive: false,
//                 autoDelete: false,
//                 arguments: null
//             );

//             var body = Encoding.UTF8.GetBytes(e.Message);

//             channel.BasicPublish(
//                 exchange: "",
//                 routingKey: queue,
//                 basicProperties: null,
//                 body: body
//             );
//         }
//     }


//     // Factory
//     // Connection
//     // Channel


// }

