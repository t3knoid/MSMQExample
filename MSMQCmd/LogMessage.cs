using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMQCmd
{
    /// <summary>
    /// You can also store objects in the message queue. As an example, suppose you 
    /// need to store a log message to the queue. The log message is stored in an
    /// instance of the LogMessage class that contains the necessary properties that
    /// pertain to the details of the log message.
    /// 
    /// You should modify the LogMessage class to incorporate other necessary 
    /// properties, i.e., message severity, etc.  
    /// </summary>
    public class LogMessage
    {
        public string MessageText { get; set; }
        public DateTime MessageTime { get; set; }
    }
}
