using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace MSMQReceive
{
    public class StudentFormatter:IMessageFormatter
    {
        public bool CanRead(Message message)
        {
            return true;
        }

        public object Read(Message message)
        {
            Student st = new Student();
            return st;
           
        }

        public void Write(Message message, object obj)
        {
            Student st = (Student)obj;
            
        }

        public object Clone()
        {
            return new StudentFormatter();
        }
    }
}
