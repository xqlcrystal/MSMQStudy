using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
namespace Sender
{
    public class Requestor
    {
        private MessageQueue requestQueue;
        private MessageQueue replyQueue;

        public Requestor(string requestQueueName,string replyQueueName)
        {
            requestQueue = new MessageQueue(requestQueueName);
            replyQueue = new MessageQueue(replyQueueName);
            replyQueue.MessageReadPropertyFilter.SetAll();
            ((XmlMessageFormatter)replyQueue.Formatter).TargetTypeNames = new string[] { "System.String,mscorlib" };

        }

        public void Send()
        {

            string content = "Hello World.";
            Send(content);
        }

        public void Send(string content)
        {
            Message requestMessage = new Message();
            requestMessage.Body = content;
            requestMessage.ResponseQueue = replyQueue;
            requestQueue.Send(requestMessage);

            Console.WriteLine("Sent request");
            Console.WriteLine("\tTime:          {0}", DateTime.Now.ToString("HH:mm:ss.ffffff"));
            Console.WriteLine("\tMessage ID:    {0}", requestMessage.Id);
            Console.WriteLine("\tCorrel:        {0}", requestMessage.CorrelationId);
            Console.WriteLine("\tReply To   :   {0}", requestMessage.ResponseQueue.Path);
            Console.WriteLine("\tContents:       {0}", requestMessage.Body.ToString());
        }

        public void ReceiveSync()
        {
            replyQueue.ReceiveCompleted += replyQueue_ReceiveCompleted;
            replyQueue.BeginReceive();
           

        }

        void replyQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            replyQueue = (MessageQueue)sender;
            Message replyMessage = replyQueue.EndReceive(e.AsyncResult);

             Console.WriteLine("Received reply");
            Console.WriteLine("\tTime:          {0}", DateTime.Now.ToString("HH:mm:ss.ffffff"));
            Console.WriteLine("\tMessage ID:    {0}", replyMessage.Id);
            Console.WriteLine("\tCorrel:        {0}", replyMessage.CorrelationId);
            Console.WriteLine("\tReply To   :   {0}", "<n/a>");
            Console.WriteLine("\tContents:       {0}", replyMessage.Body.ToString());
            replyQueue.BeginReceive();
        }

    }
}
