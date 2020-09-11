using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesChat.Models.WS
{
    public class MessagesResponse
    {
        public DateTime DateCreated { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public int TypeMessage { get; set; } // 1 propietario, 2 no propietario
    }
}
