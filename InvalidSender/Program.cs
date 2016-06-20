using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvalidSender
{
    class Program
    {
        private const string RequestQueueName = @".\Private$\RequestQueue";
        private const string ReplyQueueName = @".\Private$\ReplyQueue";
        static void Main(string[] args)
        {
            Console.WriteLine("Sender App");
            InvalidMessenger request = new InvalidMessenger(RequestQueueName, ReplyQueueName);
            request.Send();
            request.ReceiveSync();

            Console.ReadLine();

        }
    }
}
