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
    public class LobbyController : BaseController
    {
        // GET: Lobby
        public ActionResult Index()
        {
            GetSession();
            List<ListRoomResponse> lst = new List<ListRoomResponse>();
            SecurityRequest oSecurityRequest = new SecurityRequest();
            oSecurityRequest.AccessToken = oUserSession.AccessToken;

            RequestUtil oRequestUtil = new RequestUtil();
            UtilitiesChat.Models.WS.Reply oReply =
                oRequestUtil.Execute<SecurityRequest>(Constants.Url.ROOMS, "Post", oSecurityRequest);

            lst =
                JsonConvert.DeserializeObject<List<ListRoomResponse>>(JsonConvert.SerializeObject(oReply.data));


            return View(lst);
        }
    }
}