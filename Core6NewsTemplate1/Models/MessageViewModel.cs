using WebOS.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class MessageViewModel
    {
        public Message Message { get; set; }
        public MessageReply MessageReply { get; set; }
        public IEnumerable<ApplicationUser> FromApplicationUsers { get; set; }
        public IEnumerable<ApplicationUser> ToApplicationUsers { get; set; }
        public int InboxMessagesCount { get; set; }
        public IEnumerable<Message> InboxMessages { get; set; }
        public int SentMessagesCount { get; set; }
        public IEnumerable<Message> SentMessages { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<MessageReply> messageReplies { get; set; }
        public IEnumerable<MessageReply> AllMessageReplies { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ApplicationUser CurrentUser { get; set; }

    }
}
