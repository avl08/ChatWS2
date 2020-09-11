using ChatWeb.Business;
using ChatWeb.Models.ViewModels;
using Newtonsoft.Json;
using System.Web.Mvc;
using UtilitiesChat;
using UtilitiesChat.Models.WS;

namespace ChatWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login() 
        {
            UserAccessViewModel model = new UserAccessViewModel();
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccessViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AccessRequest oAR = new AccessRequest();
            oAR.Email = model.Email;
            oAR.Password =  UtilitiesChat.Tools.Encrypt.GetSHA256(model.Password);
            
            RequestUtil oRequestUtil = new RequestUtil();
            UtilitiesChat.Models.WS.Reply oReply = 
                oRequestUtil.Execute<AccessRequest>(Constants.Url.ACCESS, "Post", oAR);

            UtilitiesChat.Models.WS.UserResponse oUserResponse =
                JsonConvert.DeserializeObject<UtilitiesChat.Models.WS.UserResponse>(JsonConvert.SerializeObject(oReply.data));

            if (oReply.result == 1)
            {
                Session["User"] = oUserResponse;
                return RedirectToAction("Index", "Lobby");
            }

            ViewBag.error = "Datos incorrectos";

            return View();
        }

        [HttpGet]
        public ActionResult Register() 
        {
            ChatWeb.Models.ViewModels.RegisterViewModel model = new Models.ViewModels.RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(ChatWeb.Models.ViewModels.RegisterViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            Models.Request.User oUser = new Models.Request.User();
            oUser.Name = model.Name;
            oUser.City = model.City;
            oUser.Email = model.Email;
            oUser.Password = model.Password;

            RequestUtil oRequestUtil = new RequestUtil();
            UtilitiesChat.Models.WS.Reply oReply = oRequestUtil.Execute<Models.Request.User>(Constants.Url.REGISTER, "Post", oUser);

            return View();
        }

    }
}