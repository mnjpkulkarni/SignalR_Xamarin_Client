using System;
namespace SignalR_Client
{
    public class MessageModel
    {

        public string name { get; set; }
        public string message { get; set; }


        public MessageModel(string name, string message)
        {
            this.name = name;
            this.message = message;
        }
    }
}
