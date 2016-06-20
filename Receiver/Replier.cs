using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
namespace Receiver
{
    public class Replier
    {
        private MessageQueue invalidQueue;

        public Replier(string requestQueueName,string invalidQueueName)
        {
            MessageQueue requestQueue = new MessageQueue(requestQueueName);
            invalidQueue = new MessageQueue(invalidQueueName);
            requestQueue.MessageReadPropertyFilter.SetAll();
            ((XmlMessageFormatter)requestQueue.Formatter).TargetTypeNames = new string[] { "System.String,mscorlib"};
            requestQueue.ReceiveCompleted += requestQueue_ReceiveCompleted;
            requestQueue.BeginReceive();

        }

        void requestQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            MessageQueue requestQueue = (MessageQueue)sender;
            Message requestMessage = requestQueue.EndReceive(e.AsyncResult);

            try
            {
                Console.WriteLine("Received request");
                Console.WriteLine("\tTime:          {0}", DateTime.Now.ToString("HH:mm:ss.ffffff"));
                Console.WriteLine("\tMessage ID:    {0}", requestMessage.Id);
                Console.WriteLine("\tCorrel:        {0}", requestMessage.CorrelationId);
                Console.WriteLine("\tReply To   :   {0}", "<n/a>");
                Console.WriteLine("\tContents:       {0}", requestMessage.Body.ToString());


                string contents = requestMessage.Body.ToString();
                MessageQueue replyQueue = requestMessage.ResponseQueue;
                Message replyMessage = new Message();
                replyMessage.Body = contents;
                replyMessage.CorrelationId = requestMessage.Id;
                replyQueue.Send(replyMessage);


                Console.WriteLine("Sent reply");
                Console.WriteLine("\tTime:          {0}", DateTime.Now.ToString("HH:mm:ss.ffffff"));
                Console.WriteLine("\tMessage ID:    {0}", replyMessage.Id);
                Console.WriteLine("\tCorrel:        {0}", replyMessage.CorrelationId);
                Console.WriteLine("\tReply To   :   {0}", "<n/a>");
                Console.WriteLine("\tContents:       {0}", replyMessage.Body.ToString());

            }
            catch (Exception)
            {

                Console.WriteLine("Invalid message detected!");
                Console.WriteLine("\tType:          {0}", requestMessage.BodyType);
                Console.WriteLine("\tTime:          {0}", DateTime.Now.ToString("HH:mm:ss.ffffff"));
                Console.WriteLine("\tMessage ID:    {0}", requestMessage.Id);
                Console.WriteLine("\tCorrel:        {0}", "<n/a>");
                Console.WriteLine("\tReply To   :   {0}", "<n/a>");

                requestMessage.CorrelationId = requestMessage.Id;

                invalidQueue.Send(requestMessage);

                Console.WriteLine("Send to invalid message queue!");
                Console.WriteLine("\tType:          {0}", requestMessage.BodyType);
                Console.WriteLine("\tTime:          {0}", DateTime.Now.ToString("HH:mm:ss.ffffff"));
                Console.WriteLine("\tMessage ID:    {0}", requestMessage.Id);
                Console.WriteLine("\tCorrel:        {0}", requestMessage.CorrelationId);
                Console.WriteLine("\tReply To   :   {0}", requestMessage.ResponseQueue.Path);
            }

            requestQueue.BeginReceive();
            
        }


    }
}
