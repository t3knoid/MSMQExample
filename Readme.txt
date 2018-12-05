Creating a Queue

You can turn on MSMQ in your system through the "Turn Windows features on or off" option from the control panel. Once MSMQ 
has been installed in your system, creating a queue is simple. Just go to "My Computer", right click and select Manage. In the 
"Computer Management" window you can create a new queue from the "Message Queuing" node. You can also create a queue
programmatically.

Programming MSMQ in C#
To work with MSMQ, you would need to include the System.Messaging namespace. To create a queue programmatically, you need to
leverage the Create method of the MessageQueue class. The following code snippet illustrates this.

MessageQueue.Create(@".\Private$\IDG");

To create a queue and send a message to it, you can use the following code snippet.

MessageQueue.Create(@".\Private$\IDG");  
messageQueue = new MessageQueue(@".\Private$\IDG");
messageQueue.Label = "This is a test queue.";
messageQueue.Send("This is a test message.", "IDG");

Now, suppose you would like to check if the queue exists and if it does, send a message to it. If the queue doesn't exist, you
may want to create a new one and then send it a message. This is exactly what the following code listing does for you.



The following code snippet illustrates how you can create an instance of the LogMessage class, populate it with data and then invoke the SendMessage method to store the instance created in the message queue.

LogMessage msg = new LogMessage()

            {

                MessageText = "This is a test message.",

                MessageTime = DateTime.Now


            };

SendMessage(@".\Private$\IDGLog", msg);





