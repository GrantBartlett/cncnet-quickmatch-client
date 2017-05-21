using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CnCNetEventGameResultSuccess : EventArgs
{
    public string Response { get; private set; }

    public CnCNetEventGameResultSuccess(string response)
    {
        Response = response;
    }
}
