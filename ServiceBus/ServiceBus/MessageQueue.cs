using System;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    class MessageQueue
    {
        string connectionString = "Endpoint=sb://rcmmonitoringcvt.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=iQL3K84VXTcDyN045B1YzaKH7RHSEiJ0Kmqz65gJciU=";
        string queueName = "rcmmonitoringinfo";
        QueueClient client;

        public MessageQueue()
        {
            client = QueueClient.CreateFromConnectionString(connectionString, queueName);
        }

        public void ReadMessage()
        {
            client.OnMessage(message =>
            {
                Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>()));
                Console.WriteLine(String.Format("Message id: {0}", message.MessageId));
            });

            Console.ReadLine();
        }

        public void SendMessage()
        {
            var message = new BrokeredMessage("This is a test message!");

            client.Send(message);

            Console.ReadLine();
        }
    }
}