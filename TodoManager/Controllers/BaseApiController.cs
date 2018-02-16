using System.Net.Http;
using System.Web.Http;
using TodoManager.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using TodoManager.Models;

namespace TodoManager.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private ApplicationUserManager _appUserManager = null;
        private ApplicationRoleManager _appRoleManager = null;


        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _appUserManager ??
                    Request.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>();
            }
        }
        

        protected  ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _appRoleManager ??
                    Request.GetOwinContext()
                        .GetUserManager<ApplicationRoleManager>();
            }
        }




        public BaseApiController()
        {
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
                }
                return _modelFactory;
            }
        }


        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

    }
}