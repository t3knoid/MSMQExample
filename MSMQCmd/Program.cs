using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MSMQCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageQueue messageQueue = null;
            string description = "This is a test queue.";
            string message = "This is a test message.";
            string path = @".\Private$\IDG";

            // Now, suppose you would like to check if the queue exists and if it does,
            // send a message to it. If the queue doesn't exist, you may want to create
            // a new one and then send it a message. This is exactly what the following 
            // code listing does for you.
            try
            {
                if (MessageQueue.Exists(path))
                {
                    messageQueue = new MessageQueue(path);
                    messageQueue.Label = description;
                }
                else
                {
                    MessageQueue.Create(path);
                    messageQueue = new MessageQueue(path);
                    messageQueue.Label = description;
                }
                messageQueue.Send(message);
            }
            catch
            {
                throw;
            }
            finally
            {
                messageQueue.Dispose();
            }

            //Next, you can invoke the ReadQueue method to retrieve the messages stored in the
            //message queue as shown in the code snippet below.
            List<string> lstMessages = ReadQueue(path);
        }
        /// <summary>
        /// The following code listing illustrates how you can process the messages stored in a message queue using C#.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static List<string> ReadQueue(string path)
        {
            List<string> lstMessages = new List<string>();
            using (MessageQueue messageQueue = new MessageQueue(path))
            {
                System.Messaging.Message[] messages = messageQueue.GetAllMessages();
                foreach (System.Messaging.Message message in messages)
                {
                    message.Formatter = new XmlMessageFormatter(
                        new String[] { "System.String, mscorlib" });
                    string msg = message.Body.ToString();
                    lstMessages.Add(msg);
                }
            }
            return lstMessages;
        }
        /// <summary>
        ///  The following method illustrates how you can store an instance of the LogMessage class to the message queue.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="msg"></param>
        private static void SendMessage(string queueName, LogMessage msg)
        {
            MessageQueue messageQueue = null;
            if (!MessageQueue.Exists(queueName))
                messageQueue = MessageQueue.Create(queueName);
            else
                messageQueue = new MessageQueue(queueName);
            try
            {
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(LogMessage) });
                messageQueue.Send(msg);
            }
            catch
            {
                //Write code here to do the necessary error handling.
            }
            finally
            {
                messageQueue.Close();
            }
        }
        /// <summary>
        /// The following code listing illustrates how you can read the LogMessage instance stored in the message queue.
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        private static LogMessage ReceiveMessage(string queueName)
        {
            if (!MessageQueue.Exists(queueName))
                return null;
            MessageQueue messageQueue = new MessageQueue(queueName);
            LogMessage logMessage = null;
            try
            {
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(LogMessage) });
                logMessage = (LogMessage)messageQueue.Receive().Body;
            }
            catch { }

            finally
            {
                messageQueue.Close();
            }
            return logMessage;
        }
    }
}
