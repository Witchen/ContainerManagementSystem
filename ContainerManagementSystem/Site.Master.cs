using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using ContainerManagementSystem.Helpers;
using ContainerManagementSystem.Models;


namespace ContainerManagementSystem
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Container Management System";
            azurelogonname.InnerText = GetLogonName();
        }

        private string GetLogonName()
        {
            return System.Web.HttpContext.Current.User.Identity.Name;
        }

        protected void logout(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Application.Remove(System.Web.HttpContext.Current.User.Identity.Name);
            foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
            {
                HttpContext.Current.Cache.Remove((string)entry.Key);
                HttpContext.Current.Cache.Remove(entry.Key.ToString());
                
            }
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Redirect("https://login.windows.net/5836aab8-6b75-4b4e-824b-7e5414068499/oauth2/logout?post_logout_redirect_uri=https://login.microsoftonline.com/");
        }
    }
}