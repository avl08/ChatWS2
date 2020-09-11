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
    public class MessagesController : BaseController
    {
        [HttpPost]
        public Reply Get([FromBody] MessagesRequest model) 
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!VerifyToken(model))
            {
                oR.message = "Método no permitido";
                return oR;
            }

            try
            {
                using (ChatDBEntities db = new ChatDBEntities())
                {
                    List<MessagesResponse> lst = (from d in db.message.ToList()
                                                  where d.idState == 1
                                                  orderby d.date_created descending
                                                  select new MessagesResponse
                                                 {
                                                     Message = d.text,
                                                     Id = d.id,
                                                     IdUser = (int)d.idUser,
                                                     UserName = d.user.name,
                                                     DateCreated = (DateTime)d.date_created,
                                                     TypeMessage = (
                                                        new Func<int>(
                                                            () => 
                                                            {
                                                                try 
                                                                {
                                                                    if (d.idUser == oUserSession.Id)
                                                                    {
                                                                        return 1;
                                                                    }
                                                                    else
                                                                    {
                                                                        return 2;
                                                                    }
                                                                } 
                                                                catch 
                                                                {
                                                                    return 2;
                                                                }
                                                            }
                                                            )()
                                                     )
                                                 }).Take(20).ToList();
                    oR.result = 1;
                    oR.data = lst;
                }
            }
            catch (Exception ex)
            {
                oR.message = "Ocurrio un error";
            }
            return oR;
        }
    }
}
