using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MSMQCommon
{
    public class Inputfile
    {
        public string File { get; set; }
    }

    public class Queue
    {
        string path = @".\Private$\IDG";
        int count = 0;
        MessageQueue messageQueue = null;
        string description = "CSV Files";

        public int Count
        {
            get
            {
                count = 0;
                MessageQueue messageQueue = new MessageQueue(path);
                var enumerator = messageQueue.GetMessageEnumerator2();
                while (enumerator.MoveNext())
                    count++;
                return count;
            }
            set
            {
                count = value;
            }
        }


        /// <summary>
        /// A default constructor
        /// </summary>
        public Queue()
        {

        }
        /// <summary>
        /// A constructor where you can specify the path
        /// </summary>
        /// <param name="p"></param>
        public Queue(string p)
        {
            path = p;
        }
        /// <summary>
        /// A constructor where you can specify the messagequeue path and description
        /// </summary>
        /// <param name="p">Path</param>
        /// <param name="d">Description</param>
        public Queue(string p, string d)
        {
            path = p;
            description = d;
        }

        public void WriteMessages(string[] messages)
        {
            foreach (string message in messages)
            {
                Inputfile inputfile = new Inputfile();
                inputfile.File = message;
                try
                {
                    WriteMessage(inputfile);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
        public void WriteMessage(Inputfile message)
        {
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
        }
        /// <summary>
        /// The following code listing illustrates how you can process the messages stored in a message queue using C#.
        /// </summary>
        /// <returns>A list of messages</returns>
        public Inputfile ReadMessage()
        {
            Inputfile inputfile = new Inputfile();
            MessageQueue messageQueue = new MessageQueue(path);
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Inputfile) });
            try
            {
                Message message = messageQueue.Receive(new TimeSpan(0, 0, 1));
                inputfile = (Inputfile)message.Body;
            }
            catch (MessageQueueException e)
            {
                // Handle Message Queuing exceptions.
                if (e.Message == "Timeout for the requested operation has expired.")
                    throw new Exception("Queue is empty") ;
                else throw(e);
            }
            catch (InvalidOperationException e)
            {
                // Handle invalid serialization format.
                throw (e);
            }
            return inputfile;
        }

        public void DeleteQueue()
        {
            // Determine whether the queue exists.
            if (MessageQueue.Exists(path))
            {
                try
                {
                    // Delete the queue.
                    MessageQueue.Delete(path);
                }
                catch (MessageQueueException e)
                {
                    if (e.MessageQueueErrorCode ==
                        MessageQueueErrorCode.AccessDenied)
                    {
                        throw new Exception("Access is denied. " +
                            "Queue might be a system queue.");
                    }

                    // Handle other sources of MessageQueueException.
                }

            }
        }
    }
}
