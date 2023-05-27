using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class TicketViewModel
    {
        public Ticket Ticket { get; set; }
        public TicketReply TicketReply { get; set; }
        public TicketCategory TicketCategory { get; set; }
        //public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
        public IEnumerable<TicketReply> TicketReplys { get; set; }
    }
}
