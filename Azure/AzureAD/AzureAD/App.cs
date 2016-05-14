using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using AzureAD.Services;

namespace AzureAD
{
    public class App : Application
    {
        Label lblLoginName = new Label();
        Button btnLogin = new Button() { Text = "登入", };
        Button btnLogout = new Button() { Text = "登出", IsVisible = false };

        public static string clientId = "[在這裡填上用戶端識別碼]";
        public static string returnUri = "[在這裡填上重新導向URI]";

        public static string authority = "https://login.windows.net/common";
        private const string graphResourceUri = "https://graph.windows.net";
        public static string graphApiVersion = "2013-11-08";
        private AuthenticationResult authResult = null;

        IAuthenticator iAuth = DependencyService.Get<IAuthenticator>();

        public App()
        {
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        lblLoginName,
                        btnLogin,
                        btnLogout
                    }
                }
            };

            btnLogin.Clicked += BtnLogin_Clicked;
            btnLogout.Clicked += BtnLogout_Clicked;
        }

        /// <summary>
        /// 點選登出的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogout_Clicked(object sender, EventArgs e)
        {
            iAuth.Logout(authority);
            lblLoginName.Text = "";
        }

        /// <summary>
        /// 點選登入的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var data = await iAuth.Authenticate(authority, graphResourceUri, clientId, returnUri);
            var userName = data.UserInfo.GivenName + " " + data.UserInfo.FamilyName;
            await MainPage.DisplayAlert("Token", userName, "Ok", "Cancel");
            lblLoginName.Text = userName;
            btnLogout.IsVisible = true;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
