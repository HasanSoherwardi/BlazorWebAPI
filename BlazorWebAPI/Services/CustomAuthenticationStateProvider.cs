using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAPI.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public CustomAuthenticationStateProvider()
        {
        }

        /// <summary>
        /// This method should be called upon a successful user login, and it will store the user's JWT token in SecureStorage.
        /// Upon saving this it will also notify .NET that the authentication state has changed which will enable authenticated views
        /// </summary>
        /// <param name="token">Our JWT to store</param>
        /// <returns></returns>
        public async Task Login(string token, string username, string role)
        {
            //you can use your store/save logic  as part of this process
            await SecureStorage.SetAsync("accounttoken", token);
            await SecureStorage.SetAsync("username", username);
            await SecureStorage.SetAsync("role", role);
            if (role == "admin")
            {
                Preferences.Set("AdminLastLogin", DateTime.Now.ToString());
            }
            else
            {
                Preferences.Set("LastLogin", DateTime.Now.ToString());
            }

            //Providing the current identity ifnormation
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        /// <summary>
        /// This method should be called to log-off the user from the application, which simply removed the stored token and then
        /// notifies of the change
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            var role = await SecureStorage.GetAsync("role");
            if (role == "admin")
            {
                Preferences.Set("AdminLastLogout", DateTime.Now.ToString());
            }
            else
            {
                Preferences.Set("LastLogout", DateTime.Now.ToString());
            }
            SecureStorage.Remove("accounttoken");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        /// <summary>
        /// This is the key method that is called by .NET to accomplish our goal.  It is the method that is queried to get the current 
        /// AuthenticationState for the user.  In our base, if we have a token in secure storage, we are logged in, but we could easily
        /// do much more than this. 
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userInfo = await SecureStorage.GetAsync("accounttoken");
                var username = await SecureStorage.GetAsync("username");
                var role = await SecureStorage.GetAsync("role");
                if (userInfo != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username ?? "Unknown"),
                        new Claim(ClaimTypes.Role, role ?? "user")
                    };
                    var identity = new ClaimsIdentity(claims, "Custom authentication");
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }
            }
            catch (Exception ex)
            {
                //This should be more properly handled
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}
