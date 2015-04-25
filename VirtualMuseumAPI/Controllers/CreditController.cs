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

        public CreditController()
        {
            dc = new VirtualMuseumDataContext();
        }



        public IHttpActionResult Get()
        {
            string userid = User.Identity.GetUserId();
            var user = dc.AspNetUsers.First(u => u.Id == userid);
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
                        return EnterMuseumAction(model);
                    default:
                        if (model.Actions != null)
                        {
                            string userid = User.Identity.GetUserId();
                            var user = dc.AspNetUsers.First(u => u.Id == userid);
                            int creditsToAdd = dc.CreditActions.First(c => c.Name == Enum.GetName(typeof(CreditActionType.Actions), model.Actions)).Credits;
                            dc.CreditsXUsers.First(u => u.UID == user.Id).Credits += creditsToAdd;
                            dc.SubmitChanges();
                            return Ok(new UserInfoViewModel
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                CreditsAdded = true,
                                Credits = dc.CreditsXUsers.First(u => u.UID == userid).Credits
                            });
                        }
                        return BadRequest();
                }
            }
            return BadRequest();
        }

        private IHttpActionResult EnterMuseumAction(CreditModel model)
        {
            if (!dc.Museums.Any(m => m.ID == model.ID))
            {
                return NotFound();
            }
            string userid = User.Identity.GetUserId();
            var user = dc.AspNetUsers.First(u => u.Id == userid);
            return Ok(new UserInfoViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                CreditsAdded = ProcessEnterMuseumAction(user, model.ID),
                Credits = dc.CreditsXUsers.First(u => u.UID == userid).Credits
            });
        }

        private bool ProcessEnterMuseumAction(AspNetUser user, int museumID)
        {
            var museum = dc.Museums.First(m => m.ID == museumID);


            if (user.Id != museum.OwnerID)
            {
                //Increment museum visited count
                museum.Visited += 1;
                dc.SubmitChanges();

                if (!dc.MuseumUserVisits.Any(a => a.MuseumID == museumID && a.UID == user.Id))
                {
                    int creditsToAdd = dc.CreditActions.First( c => c.Name == Enum.GetName(typeof (CreditActionType.Actions), CreditActionType.Actions.ENTERMUSEUM)).Credits;
                    dc.CreditsXUsers.First(u => u.UID == user.Id).Credits += creditsToAdd;
                    dc.MuseumUserVisits.InsertOnSubmit(new MuseumUserVisit() {UID = user.Id, MuseumID = museumID});
                    dc.SubmitChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
