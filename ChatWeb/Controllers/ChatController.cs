using ChatWeb.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilitiesChat;
using UtilitiesChat.Models.WS;

namespace ChatWeb.Controllers
{
    public class ChatController : BaseController
    {
        public ActionResult Messages(int id)
        {
            GetSession();

            List<MessagesResponse> lst = new List<MessagesResponse>();

            MessagesRequest oMessagesRequest = new MessagesRequest();
            oMessagesRequest.AccessToken = oUserSession.AccessToken;
            oMessagesRequest.IdRoom = id;

            RequestUtil oRequestUtil = new RequestUtil();
            UtilitiesChat.Models.WS.Reply oReply =
                oRequestUtil.Execute<MessagesRequest>(Constants.Url.MESSAGES, "post", oMessagesRequest);

            lst =
                JsonConvert.DeserializeObject<List<MessagesResponse>>(JsonConvert.SerializeObject(oReply.data));

            lst = lst.OrderBy(d => d.DateCreated).ToList();

            ViewBag.idRoom = id;

            return View(lst);
        }
    }
}