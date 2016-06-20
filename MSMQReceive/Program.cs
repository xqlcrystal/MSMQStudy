using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace MSMQReceive
{
    class Program
    {
        private static string path = @".\Private$\CrystalQueue";
        static void Main(string[] args)
        {
            Receive();
            Console.ReadKey();
        }

        private static void Receive()
        {
            MessageQueue mq = new MessageQueue(path);
            mq.Formatter = new XmlMessageFormatter(new string[]{typeof(Student).AssemblyQualifiedName});
            foreach (Message mes in mq.GetAllMessages())
            {

                Console.WriteLine(mes.Label + ":" + mes.Body.ToString());
            }
        }
    }
}
