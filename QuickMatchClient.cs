using CnCNetServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cncnet_quickmatch_client
{
    public partial class QuickMatchClient : Form
    {
        private CnCNetAPI api;
        private string username = null;
        private string game = null;

        private void QuickMatchClient_Load(object sender, EventArgs e)
        {
            api = new CnCNetAPI();
        }

        public QuickMatchClient()
        {
            InitializeComponent();
        }

        public void SendResultClicked(object sender, EventArgs e)
        {
            if (CnCNetAPI.Instance != null && CnCNetAPI.Instance.AccessToken.Length > 0)
            {
                Console.Write("Sending");

                CnCNetAPI.Instance.OnGameResultError += onGameResultError;
                CnCNetAPI.Instance.OnGameResultSuccess += onGameResultSuccess;
                CnCNetAPI.Instance.SendGameResult(game, username);
            }
        }

        private void onGameResultError(object sender, CnCNetEventGameResultError e)
        {
            Console.Write("onGameResultError: " + e.Response.ToString());

            CnCNetAPI.Instance.OnGameResultSuccess -= onGameResultSuccess;
            CnCNetAPI.Instance.OnGameResultError -= onGameResultError;
            tbLog.Text = e.Response.ToString();
        }

        private void onGameResultSuccess(object sender, CnCNetEventGameResultSuccess e)
        {
            Console.Write("onGameResultSuccess: " + e.Response.ToString());

            CnCNetAPI.Instance.OnGameResultSuccess -= onGameResultSuccess;
            CnCNetAPI.Instance.OnGameResultError -= onGameResultError;
            tbLog.Text = e.Response.ToString();
        }

        public void LoginClicked(object sender, EventArgs e)
        {
            CnCNetAPI.Instance.Credentials = new NetworkCredential(tbUsername.Text, tbUserPassword.Text);
            CnCNetAPI.Instance.OnUserLoginSuccess += onUserLoginSuccess;
            CnCNetAPI.Instance.OnUserLoginError += onUserLoginError;
            CnCNetAPI.Instance.Login();
        }

        private void onUserLoginError(object sender, CnCNetEventUserLoginError e)
        {
            CnCNetAPI.Instance.OnUserLoginSuccess -= onUserLoginSuccess;
            CnCNetAPI.Instance.OnUserLoginError -= onUserLoginError;

            tbUsername.Enabled = true;
            tbUserPassword.Enabled = true;

            MessageBox.Show("Error logging in with those details");
            Debug.Print("QuickMatchClient ** onUserLoginError: " + e.ErrorMessage);
        }

        private void onUserLoginSuccess(object sender, CnCNetEventUserLoginSuccess e)
        {
            api.OnUserLoginSuccess -= onUserLoginSuccess;
            api.OnUserLoginError -= onUserLoginError;

            tbUsername.Enabled = false;
            tbUserPassword.Enabled = false;

            Debug.Print("QuickMatchClient ** onUserLoginSuccess: " + e.AccessToken);
            CnCNetAPI.Instance.AccessToken = e.AccessToken;

            onGetUsernames();
        }

        private void onGetUsernames()
        {
            if (CnCNetAPI.Instance != null)
            {
                CnCNetAPI.Instance.OnUsernamesSuccess += onUsernamesSuccess;
                CnCNetAPI.Instance.GetUsernames();
            }
        }

        private void onUsernamesSuccess(object sender, CnCNetEventUsernamesSuccess e)
        {
            CnCNetAPI.Instance.OnUsernamesSuccess -= onUsernamesSuccess;

            lbUsernames.Items.Clear();

            for(int i = 0; i < e.Usernames.Count; i ++)
            {
                var user = e.Usernames[i];
                lbUsernames.Items.Add(user);
            }
        }

        public void OnSelectedUsernamedChanged(object sender, EventArgs e)
        {
            // Get the currently selected item in the ListBox.
            string currentUsername = lbUsernames.SelectedItem.ToString();

            // Find the string in ListBox2.
            int index = lbUsernames.FindString(currentUsername);
            // If the item was not found in ListBox 2 display a message box, otherwise select it in ListBox2.
            if (index == -1)
                return;

            username = currentUsername;
        }

        public void OnSelectedGameChanged(object sender, EventArgs e)
        {
            // Get the currently selected item in the ListBox.
            string currentGame = lbGames.SelectedItem.ToString();

            // Find the string in ListBox2.
            int index = lbGames.FindString(currentGame);
            // If the item was not found in ListBox 2 display a message box, otherwise select it in ListBox2.
            if (index == -1)
                return;

            game = currentGame.ToLower();
        }
    }
}
