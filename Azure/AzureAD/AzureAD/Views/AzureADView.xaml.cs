using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using AzureAD.Services;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AzureAD.Views
{
    public partial class AzureADView : ContentPage
    {
        Label lblLoginName = new Label();
        Button btnLogin = new Button() { Text = "登入", };
        Button btnLogout = new Button() { Text = "登出", IsVisible = false };

        public static string clientId = "[在這裡填上用戶端識別碼]";
        public static string returnUri = "[在這裡填上重新導向URI]";
        public static string webAPIUri = "[在這裡填上WebAPI的URI]";

        public static string authority = "https://login.windows.net/common";
        public static string graphApiVersion = "2013-11-08";
        
        // 如果只進行App驗證，請使用下面的graphResourceUri
        private const string graphResourceUri = "https://graph.windows.net";
        // 如果要啟用WebAPI整合驗證，請使用下面的graphResourceUri
        // private const string graphResourceUri = "[在這裡填上WebAPI的應用程式識別 URI]";


        IAuthenticator iAuth = DependencyService.Get<IAuthenticator>();

        public AzureADView()
        {
            InitializeComponent();

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                    {
                        lblLoginName,
                        btnLogin,
                        btnLogout
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
            await DisplayAlert("Token", userName, "Ok", "Cancel");
            lblLoginName.Text = userName;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, webAPIUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", data.AccessToken);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            await DisplayAlert("webapi", content, "Ok");

            btnLogout.IsVisible = true;
        }
    }
}
