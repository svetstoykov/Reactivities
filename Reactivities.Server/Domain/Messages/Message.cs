using System;
using Domain.Profiles;

namespace Domain.Messages
{
    public class Message
    {
        private Message() {}
    
        private Message(Profile sender, Profile receiver, string content, DateTime dateSent)
        {
            this.Sender = sender;
            this.Receiver = receiver;
            this.Content = content;
            this.DateSent = dateSent;
        }
    
        public int Id { get; set; }
    
        public int SenderId { get; private set; }
    
        public Profile Sender { get; private set; }
    
        public int ReceiverId { get; private set; }
    
        public Profile Receiver { get; private set; }
    
        public string Content { get; private set; }
    
        public DateTime DateSent { get; private set; }

        public static Message New(Profile sender, Profile receiver, string content, DateTime dateSent)
            => new Message(sender, receiver, content, dateSent);
    }
}