using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesChat.Models.WS
{
    public class MessagesRequest : SecurityRequest
    {
        public int IdRoom { get; set; }
    }
}
