using System;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    class SessionMessageQueue
    {
        string connectionString = "Endpoint=sb://rcmmonitoringcvt.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=iQL3K84VXTcDyN045B1YzaKH7RHSEiJ0Kmqz65gJciU=";
        string queueName = "rcmmonitoringcritical";
        QueueClient queueClient;

        string messageId = "28c36c04-a616-4daa-af8a-1a23b755e16d"; // Guid.NewGuid().ToString();
        string sessionId = "015b64b4-420d-410c-b724-18a8d803fd84"; // Guid.NewGuid().ToString();

        public SessionMessageQueue()
        {
            queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
        }

        public void ReadMessage()
        {
            var session = queueClient.AcceptMessageSession(sessionId);
            BrokeredMessage message = null;

            while (true)
            {
                try
                {
                    message = session.Receive(TimeSpan.FromSeconds(5));

                    if (message != null)
                    {
                        Console.WriteLine(string.Format("Message received: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));

                        message.Complete();
                    }
                    else
                    {
                        //no more messages in the queue 
                        break;
                    }
                }
                catch (MessagingException e)
                {
                    if (!e.IsTransient)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                    else
                    {
                        HandleTransientErrors(e);
                    }
                }
            }

            Console.ReadLine();
        }

        private void HandleTransientErrors(MessagingException e)
        {
            throw new NotImplementedException();
        }

        public void SendMessage()
        {
            var message = new BrokeredMessage("This is a test message!")
            {
                MessageId = messageId,
                SessionId = sessionId
            };

            queueClient.Send(message);

            Console.ReadLine();
        }
    }
}