using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using ChatWS2.Models;

namespace ChatWS2
{
    public class CounterHub : Hub
    {
        public override Task OnConnected()
        {
            Clients.All.enterUser();
            return base.OnConnected();
        }

        public void AddGroup(int idRoom) 
        {
            Groups.Add(Context.ConnectionId, idRoom.ToString());
        }

        public void Send(int idRoom, string userName, int idUser, string message, string AccessToken) 
        {
            if (VerifyToken(AccessToken)) 
            {
                string fecha = DateTime.Now.ToString();

                using (ChatDBEntities db = new ChatDBEntities())
                {
                    var oMessage = new message();
                    oMessage.idRoom = idRoom;
                    oMessage.date_created = DateTime.Now;
                    oMessage.idUser = idUser;
                    oMessage.text = message;
                    oMessage.idState = 1;

                    db.message.Add(oMessage);
                    db.SaveChanges();
                }

                Clients.Group(idRoom.ToString()).sendChat(userName, message, fecha, idUser);
            }
            
        }

        protected bool VerifyToken(string AccessToken)
        {
            using (ChatDBEntities db = new ChatDBEntities())
            {
                var oUser = db.user.Where(d => d.access_token == AccessToken).FirstOrDefault();

                if (oUser != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}