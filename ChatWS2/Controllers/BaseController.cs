using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UtilitiesChat.Models.WS;
using ChatWS2.Models;

namespace ChatWS2.Controllers
{
    public class BaseController : ApiController
    {
        public UserResponse oUserSession;
        protected bool VerifyToken(SecurityRequest model) 
        {
            using (ChatDBEntities db = new ChatDBEntities()) 
            {
                var oUser = db.user.Where(d => d.access_token == model.AccessToken).FirstOrDefault();

                if (oUser != null) 
                {
                    oUserSession = new UserResponse();
                    oUserSession.AccessToken = oUser.access_token;
                    oUserSession.City = oUser.city;
                    oUserSession.Name= oUser.name;
                    oUserSession.Id = oUser.idUser;
                    return true;
                }
            }
            return false;
        }
    }
}
