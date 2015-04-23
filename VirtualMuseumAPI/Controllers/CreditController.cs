using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using VirtualMuseumAPI.Models;
using VirtualMuseumAPI.Helpers;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;


namespace VirtualMuseumAPI.Controllers
{
    [Authorize]
    public class CreditController : ApiController
    {
        VirtualMuseumDataContext dc;
        private ApplicationUserManager _userManager;

        public CreditController()
        {
            dc = new VirtualMuseumDataContext();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public IHttpActionResult Get()
        {
            string userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return Ok(new UserInfoViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Credits = dc.CreditsXUsers.First(u => u.UID == userid).Credits
            });
        }

        public IHttpActionResult Post(CreditModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Actions)
                {
                    case CreditActionType.Actions.ENTERMUSEUM:
                        var museum = dc.Museums.First(m => m.ID == model.ID);
                        string userid = User.Identity.GetUserId();
                        if (userid != museum.OwnerID)
                        {
                            if (!dc.MuseumUserVisits.Any(a => a.MuseumID == model.ID && a.UID == userid))
                            {
                                int creditsToAdd = dc.CreditActions.First(c => c.Name == Enum.GetName(typeof(CreditActionType),model.Actions)).Credits;
                                dc.CreditsXUsers.First(u => u.UID == userid).Credits += creditsToAdd;
                                dc.SubmitChanges();
                                var user = UserManager.FindById(userid);
                                return Ok(new UserInfoViewModel
                                {
                                    UserName = user.UserName,
                                    Email = user.Email,
                                    Credits = dc.CreditsXUsers.First(u => u.UID == userid).Credits
                                });
                            }
                            else
                            {
                                return BadRequest();
                            }
                        }
                        else
                        {
                            return BadRequest();
                        }
                    default:
                        break;
                }
            }
            return BadRequest();
        }

    }
}
