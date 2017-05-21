using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CnCNetEventCreateUserSuccess : EventArgs
{
    public string NewUsername { get; private set; }
    public JObject Response { get; private set; }

    public CnCNetEventCreateUserSuccess(string response)
    {
        Response = JObject.Parse(response);
        NewUsername = Response["username"].ToString();
    }
}
