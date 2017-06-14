using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CnCNetServices
{
    public class CnCNetAPI
    {
        public const string STATE_LOGGED_IN = "stateLoggedIn";
        public const string STATE_LOGGED_OUT = "stateLoggedOut";
        public const string STATE_NO_USERNAMES = "stateNoUsernames";
        public const string STATE_HAS_USERNAMES = "stateHasUsernames";
        public const string STATE_USERNAMES_UPDATED = "stateUsernamesUpdated";
        public const string STATE_FATAL_ERROR = "stateFatalError";

        public event EventHandler<CnCNetEventUserLoginSuccess> OnUserLoginSuccess;
        public event EventHandler<CnCNetEventUserLoginError> OnUserLoginError;
        public event EventHandler<CnCNetEventUsernamesSuccess> OnUsernamesSuccess;
        public event EventHandler<CnCNetEventCreateUserSuccess> OnCreateUsernameSuccess;
        public event EventHandler<CnCNetEventCreateUserError> OnCreateUsernameError;
        public event EventHandler<CnCNetEventFatalError> OnFatalError;
        public event EventHandler<CnCNetEventGameResultSuccess> OnGameResultSuccess;
        public event EventHandler<CnCNetEventGameResultError> OnGameResultError;

        public string AccessToken = "";
        public NetworkCredential Credentials { set; get; }
        public List<string> Usernames { set; get; }

        private const string API_URL = "https://staging.cnc-comm.com/api/v1";
        private const string API_AUTH_URL = "/auth/token";
        private const string API_ACCOUNT_URL = "/user/account";
        private const string API_GAME_RESULT_URL = "/result";
        private GameResult gameResult = null;

        private static CnCNetAPI _instance;

        /// <summary>
        /// Singleton Pattern. Returns the object of this class.
        /// </summary>
        /// <returns>The object of the ClientConfiguration class.</returns>
        public static CnCNetAPI Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CnCNetAPI();
                }
                return _instance;
            }
        }

        public void Login()
        {
            try
            {
                var wc = new WebClient()
                {
                    Credentials = Credentials
                };

                string response = wc.DownloadString(API_URL + API_AUTH_URL);

                AccessToken = JObject.Parse(response)["token"].ToString();
                OnUserLoginSuccess?.Invoke(this, new CnCNetEventUserLoginSuccess(response));
            }
            catch (WebException ex)
            {
                try
                {
                    OnUserLoginError?.Invoke(this, new CnCNetEventUserLoginError("Invalid username or password", ((HttpWebResponse)ex.Response).StatusCode));
                }
                catch (Exception exx)
                {
                    OnFatalError?.Invoke(this, new CnCNetEventFatalError(HttpStatusCode.BadGateway));
                }
            }
        }

        public void GetUsernames()
        {
            try
            {
                var wc = new WebClient();
                wc.Headers.Add("Authorization", "Bearer:" + AccessToken);

                string response = wc.DownloadString(API_URL + API_ACCOUNT_URL);
                OnUsernamesSuccess?.Invoke(this, new CnCNetEventUsernamesSuccess(response));
            }
            catch (WebException ex)
            {
                try
                {
                    OnUserLoginError?.Invoke(this, new CnCNetEventUserLoginError(ex.Message, ((HttpWebResponse)ex.Response).StatusCode));
                }
                catch (Exception exx)
                {
                    OnFatalError?.Invoke(this, new CnCNetEventFatalError(HttpStatusCode.BadGateway));
                }
            }
        }

        public void AddUser(string username)
        {
            try
            {
                var wc = new WebClient();
                wc.Headers.Add("Authorization", "Bearer:" + AccessToken);

                string response = wc.UploadString(API_URL + "/player/" + username, "POST");
                OnCreateUsernameSuccess?.Invoke(this, new CnCNetEventCreateUserSuccess(response));
            }
            catch (WebException ex)
            {
                try
                {
                    OnCreateUsernameError?.Invoke(this, new CnCNetEventCreateUserError("This username already exists", ((HttpWebResponse)ex.Response).StatusCode));
                }
                catch (Exception exx)
                {
                    OnFatalError?.Invoke(this, new CnCNetEventFatalError(HttpStatusCode.BadGateway));
                }
            }
        }

        public void SendGameResult(string game, string username)
        {
            if (File.Exists("stats.dmp"))
            {
                gameResult = new GameResult("stats.dmp");

                gameResult.AddField("nick", username);
                gameResult.OnGameResultComplete += onGameResultComplete;
                gameResult.OnGameResultFailed += onGameResultFailed;

                Console.WriteLine("Request: " + API_URL + API_GAME_RESULT_URL + "/" + game + "/" + username, AccessToken);
                gameResult.UploadToServer(API_URL + API_GAME_RESULT_URL + "/" + game + "/" + username, AccessToken);
            }
            else
            {
                Console.Write("No stats file found");
            }
        }

        private void onGameResultFailed(object sender, CnCNetEventGameResultError e)
        {
            gameResult.OnGameResultComplete -= onGameResultComplete;
            gameResult.OnGameResultFailed -= onGameResultFailed;
            OnGameResultError?.Invoke(this, new CnCNetEventGameResultError(e.Response != null ? e.Response.ToString() : ""));
        }

        private void onGameResultComplete(object sender, CnCNetEventGameResultSuccess e)
        {
            gameResult.OnGameResultComplete -= onGameResultComplete;
            gameResult.OnGameResultFailed -= onGameResultFailed;
            OnGameResultSuccess?.Invoke(this, new CnCNetEventGameResultSuccess(e.Response.ToString()));
        }
    }
}