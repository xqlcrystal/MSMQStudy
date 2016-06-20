using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receiver
{
    class Program
    {
        private const string invalidQueueName = @".\Private$\InvalidQueue";
        private const string RequestQueueName = @".\Private$\RequestQueue";
        static void Main(string[] args)
        {
            Console.WriteLine("Receiver App");
            Replier reply = new Replier(RequestQueueName,invalidQueueName);
            Console.ReadLine();
        }
    }
}
