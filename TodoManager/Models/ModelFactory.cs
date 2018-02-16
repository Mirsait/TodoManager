using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using TodoManager.Infrastructure;

namespace TodoManager.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        private ApplicationUserManager _appUserManager;

        public ModelFactory(HttpRequestMessage httpRequestMessage, ApplicationUserManager applicationUserManager)
        {
            this._urlHelper = new UrlHelper(httpRequestMessage);
            this._appUserManager = applicationUserManager;
        }
        
        public UserReturnModel Create(ApplicationUser appUser)
        {
            UserReturnModel user = new UserReturnModel
            {
                Url = _urlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                JoinDate = appUser.JoinDate,
                Roles = _appUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = _appUserManager.GetClaimsAsync(appUser.Id).Result
            };
            return user;
        }
        
        public RoleReturnModel Create(IdentityRole appRole)
        {
            RoleReturnModel role = new RoleReturnModel
           {
               Url = _urlHelper.Link("GetRoleById", new { id = appRole.Id }),
               Id = appRole.Id,
               Name = appRole.Name
           };
            return role;
        }
        
    }


    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Level { get; set; }
        public DateTime JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }


    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }


}
