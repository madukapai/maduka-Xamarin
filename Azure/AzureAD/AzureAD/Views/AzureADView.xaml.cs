using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using AzureAD.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace AzureAD.Views
{
    public partial class AzureADView : ContentPage
    {
        public static string clientId = "[在這裡填上用戶端識別碼]";
        public static string returnUri = "[在這裡填上重新導向URI]";
        public static string webAPIUri = "[在這裡填上WebAPI的URI]";
        public static string tenant = "[在這裡填入Azure AD的網域名稱，如:maduka.onmicrosoft.com]";
        public static string graphAPI = "https://graph.windows.net/{0}/users/{1}?api-version=1.6";
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
        }

        /// <summary>
        /// 點選登出的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            iAuth.Logout(authority);
            lblUserId.Text = "";
            lblUserDisplayName.Text = "";
            lblUserInfoJson.Text = "";
            btnLogout.IsEnabled = false;
        }

        /// <summary>
        /// 點選登入的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // 進行登入，並取得基本的AAD資訊
            var objAuthResult = await iAuth.Authenticate(authority, graphResourceUri, clientId, returnUri);
            var userName = objAuthResult.UserInfo.GivenName + " " + objAuthResult.UserInfo.FamilyName;
            await DisplayAlert("Token", userName, "Ok", "Cancel");
            lblUserId.Text = userName;

            // 取得 WebAPI上的資訊，並於WebAPI進行二次驗證
            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, webAPIUri);
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", objAuthResult.AccessToken);
            //var response = await client.SendAsync(request);
            //var content = await response.Content.ReadAsStringAsync();
            //await DisplayAlert("webapi", content, "Ok");

            // 取得AAD中詳細的資訊
            Models.AzureAD.UserProfile objProfile = await this.GetAADUserInfo(objAuthResult);
            lblUserDisplayName.Text = objProfile.displayName;

            btnLogout.IsEnabled = true;
        }

        /// <summary>
        /// 取得AAD上的人員詳細資料
        /// </summary>
        /// <param name="objAuth"></param>
        /// <returns></returns>
        public async Task<Models.AzureAD.UserProfile> GetAADUserInfo(AuthenticationResult objAuthResult)
        {
            // 置換GraphAPI Url
            string strGraphAPI = string.Format(graphAPI, tenant, objAuthResult.UserInfo.DisplayableId);

            // 取得登入帳號資料
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, strGraphAPI);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", objAuthResult.AccessToken);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            // 回傳的Json資訊放入Label顯示
            lblUserInfoJson.Text = content;

            // 將Json轉換成物件並回傳
            return JsonConvert.DeserializeObject<Models.AzureAD.UserProfile>(content);
        }
    }
}
