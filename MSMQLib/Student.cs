using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSMQReceive
{    
    [Serializable()]
    public class Student
    {
        public string ID { get; set; }
        public string Sex { get; set; }
        public string Year { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", ID, Sex, Year);
        }
    }
}
