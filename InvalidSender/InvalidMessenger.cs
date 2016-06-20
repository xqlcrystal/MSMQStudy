using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
namespace InvalidSender
{
    public class InvalidMessenger
    {
        private MessageQueue requestQueue;
        private MessageQueue replyQueue;

        public InvalidMessenger(string requestQueueName, string replyQueueName)
        {
            requestQueue = new MessageQueue(requestQueueName);
            replyQueue = new MessageQueue(replyQueueName);
            replyQueue.MessageReadPropertyFilter.SetAll();
            ((XmlMessageFormatter)replyQueue.Formatter).TargetTypeNames = new string[] { "System.String,mscorlib" };

        }

        public void Send()
        {
            
            BinaryMessageFormatter formatter=new BinaryMessageFormatter();
            Message requestMessage = new Message("Hello World", formatter);
            requestMessage.ResponseQueue = replyQueue;
            requestQueue.Send(requestMessage);

            Console.WriteLine("Sent request");
            Console.WriteLine("\tType:          {0}", requestMessage.BodyType);
            Console.WriteLine("\tTime:          {0}",DateTime.Now.ToString("HH:mm:ss.ffffff"));
            Console.WriteLine("\tMessage ID:    {0}",requestMessage.Id);
            Console.WriteLine("\tCorrel:        {0}", requestMessage.CorrelationId);
            Console.WriteLine("\tReply To   :   {0}", requestMessage.ResponseQueue.Path);
            Console.WriteLine("\tContents:       {0}", requestMessage.Body.ToString());
            
        }

        public void ReceiveSync()
        {
            Message replyMessage=replyQueue.Receive();

             Console.WriteLine("Received reply");
            Console.WriteLine("\tTime:          {0}",DateTime.Now.ToString("HH:mm:ss.ffffff"));
            Console.WriteLine("\tMessage ID:    {0}",replyMessage.Id);
            Console.WriteLine("\tCorrel:        {0}", replyMessage.CorrelationId);
            Console.WriteLine("\tReply To   :   {0}", "<n/a>");
            Console.WriteLine("\tContents:       {0}", replyMessage.Body.ToString());

        }

    }
}
