using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CnCNetEventUserLoginSuccess : EventArgs
{
    public string AccessToken { get; private set; }
    public JObject Response { get; private set; }

    public CnCNetEventUserLoginSuccess(string response)
    {
        Response = JObject.Parse(response);
        AccessToken = Response["token"].ToString();
    }
}
