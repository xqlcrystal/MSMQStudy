using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sender
{
    class Program
    {
        private const string RequestQueueName = @".\Private$\RequestQueue";
        private const string ReplyQueueName = @".\Private$\ReplyQueue";
        static void Main(string[] args)
        {
            Console.WriteLine("Sender App");
            Requestor request = new Requestor(RequestQueueName, ReplyQueueName);


            request.ReceiveSync();

            int max = 10000;
            int i=0;
            while(i<max)
            {
                request.Send(i++.ToString());
            }
            
          

            Console.ReadLine();

        }
    }
}
